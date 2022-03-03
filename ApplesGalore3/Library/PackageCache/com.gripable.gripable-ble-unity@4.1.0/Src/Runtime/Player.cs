using System;
using System.Collections;
using System.Collections.Generic;
using Protos;
using UnityEngine;

namespace Gripable
{
    public class Player
    {
        //test comment
        private DeviceGateway _deviceGateway;

        public event Action<ConnectionState> OnConnectionStateChanged;
        public event Action<StreamingState> OnStreamingStateChanged;
        public event Action<Gesture> OnGesture;

        // calibrations
        private Calibration _gripCalibration;
        private Calibration _rollCalibration;
        private Calibration _pitchCalibration;
        private Calibration _yawCalibration;
        private float _scaleFactor;
        private float _neutralGrip;

        private bool _lowPassFilterOn = true;
        private LowPassFilter _lowPassFilterRoll;
        private LowPassFilter _lowPassFilterPitch;
        private LowPassFilter _lowPassFilterYaw;

        public void SetDevice(string macAddress)
        {
            var clientGateway = new ClientGateway(AndroidHelper.GetApplicationContext());
            _deviceGateway = clientGateway.GetDeviceGateway(macAddress);
            _deviceGateway.OnConnectionStateChanged += (ConnectionState state) => OnConnectionStateChanged?.Invoke(state);
            _deviceGateway.OnStreamingStateChanged += (StreamingState state) => OnStreamingStateChanged?.Invoke(state);
            _deviceGateway.OnGesture += (Gesture gesture) => OnGesture?.Invoke(gesture);
            _lowPassFilterRoll = new LowPassFilter();
            _lowPassFilterPitch = new LowPassFilter();
            _lowPassFilterYaw = new LowPassFilter();
        }

        public DeviceGateway GetDevice()
        {
            return _deviceGateway;
        }

        public void SetHand(Hand hand)
        {
            _deviceGateway.SetHand((int)hand);
        }

        public void SetCalibration(Calibration gripCalibration, Calibration rollCalibration, Calibration pitchCalibration, Calibration yawCalibration, float scaleFactor)
        {
            if (gripCalibration == null)
                Debug.LogError("ERROR: Null Grip Calibration received");
            if (rollCalibration == null)
                Debug.LogError("ERROR: Null Roll Calibration received");
            if (pitchCalibration == null)
                Debug.LogError("ERROR: Null Pitch Calibration received");
            if (yawCalibration == null)
                Debug.LogError("ERROR: Null Yaw Calibration received");
            _gripCalibration = gripCalibration;
            _rollCalibration = rollCalibration;
            _pitchCalibration = pitchCalibration;
            _yawCalibration = yawCalibration;
            SetNormalizeScaleFactor(scaleFactor);
            SetGestureConfigs(scaleFactor);
        }

        private void SetGestureConfigs(float scaleFactor)
        {
            Calibration[] calibrations = new Calibration[4];
            calibrations[0] = _gripCalibration;
            calibrations[1] = _rollCalibration;
            calibrations[2] = _pitchCalibration;
            calibrations[3] = _yawCalibration;
            _deviceGateway.SetGestureConfigs(calibrations, scaleFactor);

        }

        public void SetNormalizeScaleFactor(float scaleFactor)
        {
            _scaleFactor = scaleFactor;
        }

        public void SetGestureScaleFactor(float scaleFactor)
        {
            SetGestureConfigs(scaleFactor);
        }

        public float GetGripForce()
        {
            if (IsInitialized())
            {
                float force = _deviceGateway.GetSensorData().Grip.Force;
                //float normalizedForce = (force - _gripCalibration.Min) / ((_gripCalibration.Max * _scaleFactor) - _gripCalibration.Min);
                //return Mathf.Clamp(normalizedForce, 0, 1);
                return CalculateNormalizedGripForce(force, _gripCalibration.Min, _gripCalibration.Neutral, _gripCalibration.Max, _scaleFactor);
            }
            else
            {
                return new SensorData().Grip.Force;
            }
        }

        public float CalculateNormalizedGripForce(float force, float calibrationMin, float neutral, float calibrationMax, float scaleFactor)
        {

            float scaledMax = neutral + ((calibrationMax - neutral) * scaleFactor);
            float scaledMin = neutral - ((neutral - calibrationMin) * scaleFactor);
            float normalized = (force - scaledMin) / (scaledMax - scaledMin);

            if (normalized.Equals(float.NaN))
                return 0;
            else
                return Mathf.Clamp(normalized, 0, 1); ;
        }

        public float GetRoll()
        {
            if (IsInitialized())
            {
                return NormalizeRpy(_deviceGateway.GetGameRpyData().Roll, _rollCalibration, _lowPassFilterRoll);
            }
            else
            {
                return new WristRpyData().Roll;
            }
        }

        public float GetPitch()
        {
            if (IsInitialized())
            {
                return NormalizeRpy(_deviceGateway.GetGameRpyData().Pitch, _pitchCalibration, _lowPassFilterPitch);
            }
            else
            {
                return new WristRpyData().Pitch;
            }
        }

        public float GetYaw()
        {
            if (IsInitialized())
            {
                return NormalizeRpy(_deviceGateway.GetGameRpyData().Yaw, _yawCalibration, _lowPassFilterYaw);
            }
            else
            {
                return new WristRpyData().Yaw;
            }
        }

        public float CalculateNormalizedWristRpy(float degrees, float calibrationMin, float calibrationMax, float scaleFactor)
        {
            float normalized;
            if (degrees > 180f)
            {
                degrees -= 360f;
                if (calibrationMin == 0 && degrees < 0) normalized = -1f;
                else normalized = -degrees / ((calibrationMin - 360) * scaleFactor);
            }
            else
            {
                normalized = degrees / (calibrationMax * scaleFactor);
            }

            if (normalized.Equals(float.NaN))
                return 0;
            else
                return Mathf.Clamp(normalized, -1, 1);
        }

        private float NormalizeRpy(float degrees, Calibration calibration, LowPassFilter lowPassFilter)
        {
            float normalized = CalculateNormalizedWristRpy(degrees, calibration.Min, calibration.Max, _scaleFactor);

            if (_lowPassFilterOn)
                normalized = lowPassFilter.FilterInput(normalized);

            return Mathf.Clamp(normalized, -1, 1);
        }

        public void ResetWristRpy()
        {
            if (IsInitialized())
            {
                _deviceGateway.ResetWristRpy();
            }
            else
            {
                Debug.LogError("ResetWristRpy: Gripable Device is not set");
            }
        }


        // Connection methods

        public bool Connect()
        {
            if (IsInitialized())
            {
                GripablePlugin.Player.SetCalibration(
                new Calibration { Min = 0, Max = 70, Type = MovementType.Grip },
                new Calibration { Min = 315, Max = 60, Type = MovementType.Roll },
                new Calibration { Min = 345, Max = 40, Type = MovementType.Pitch },
                new Calibration { Min = 310, Max = 30, Type = MovementType.Yaw }, 0.4f);
                return _deviceGateway.Connect();
            }
            else
            {
                return false;
            }
        }

        public AndroidJavaObject GetNativeDeviceObject()
        {
            if (IsInitialized())
            {
                return _deviceGateway.GetDeviceInstance();
            }
            else
            {
                return null;
            }
        }

        public void Disconnect()
        {
            if (IsInitialized())
            {
                _deviceGateway.Disconnect();
            }
            else
            {
                Debug.LogError("Disconnect: Gripable Device is not set");
            }
        }

        public void SendRumbleCommand(DeviceCommand.Types.VibrationEffect vibrationEffect, DeviceCommand.Types.SamplingRate samplingRate = DeviceCommand.Types.SamplingRate.Hz50)
        {
            if (IsInitialized())
            {
                _deviceGateway.SendDeviceCommand(new DeviceCommand {
                    SamplingRate = samplingRate,
                    VibrationEffect = vibrationEffect
                });
            }
            else
            {
                Debug.LogError("SendRumbleCommand: Gripable Device is not set");
            }
        }

        public bool IsInitialized()
        {
            return _deviceGateway != null;
        }

        public void ActivateLowPassFilter(bool enabled)
        {
            _lowPassFilterOn = enabled;
        }

        //private float NormalizeRpy(float degrees, Calibration calibration, LowPassFilter lowPassFilter)
        //{
        //    float normalized;
        //    if (degrees > 180.0f)
        //    {
        //        degrees -= 360;
        //        normalized = -degrees / ((calibration.Min - 360) * _scaleFactor);
        //    }
        //    else
        //    {
        //        normalized = degrees / (calibration.Max * _scaleFactor);
        //    }

        //    if (_lowPassFilterOn)
        //        normalized = lowPassFilter.FilterInput(normalized);

        //    return Mathf.Clamp(normalized, -1, 1);
        //}
    }
}
