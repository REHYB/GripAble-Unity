using UnityEngine;
using Protos;
using Google.Protobuf;
using System;
using System.Collections.Generic;


namespace Gripable
{
    public class DeviceGateway : AndroidJavaProxy
    {
        private const int GESTURE_DURATION = 600000;
        private const int TIMEOUT_DURATION = 250;

        private AndroidJavaObject _javaDeviceGateway;
        public Action<ConnectionState> OnConnectionStateChanged;
        public Action<StreamingState> OnStreamingStateChanged;
        public Action<Gesture> OnGesture;

        public DeviceGateway(AndroidJavaObject javaDeviceGateway)
            : base("com.gripable.gripablebleandroid.unity.IUnityDeviceGateway")
        {
            _javaDeviceGateway = javaDeviceGateway;
            _javaDeviceGateway.Call("setUnityDeviceGateway", this);
        }

        public bool Connect()
        {
            return _javaDeviceGateway.Call<bool>("connect");
        }

        public void Disconnect()
        {
            _javaDeviceGateway.Call("disconnect");
        }

        public string GetMacAddress()
        {
            return _javaDeviceGateway.Call<string>("getMacAddress");
        }

        public bool IsCached()
        {
            return _javaDeviceGateway.Call<bool>("isCached");
        }

        public void StartStreamingData()
        {
            _javaDeviceGateway.Call("startStreamingData");
        }

        public void StopStreamingData()
        {
            _javaDeviceGateway.Call("stopStreamingData");
        }

        public float GetSensorKbps()
        {
            return _javaDeviceGateway.Call<float>("getSensorKbps");
        }

        public SensorData GetSensorData()
        {
            sbyte[] sensorDataBytes = _javaDeviceGateway.Call<sbyte[]>("getSensorDataBytes");
            return SensorData.Parser.ParseFrom((byte[])(Array)sensorDataBytes);
        }

        public WristRpyData GetWristRpyData()
        {
            sbyte[] wristRpyData = _javaDeviceGateway.Call<sbyte[]>("getWristRpyDataBytes");
            return WristRpyData.Parser.ParseFrom((byte[])(Array)wristRpyData);
        }

        public WristRpyData GetGameRpyData()
        {
            sbyte[] gameRpyData = _javaDeviceGateway.Call<sbyte[]>("getGameRpyDataBytes");
            return WristRpyData.Parser.ParseFrom((byte[])(Array)gameRpyData);
        }

        public DeviceStatus GetDeviceStatus()
        {
            sbyte[] deviceStatus = _javaDeviceGateway.Call<sbyte[]>("getDeviceStatusBytes");
            return DeviceStatus.Parser.ParseFrom((byte[])(Array)deviceStatus);
        }

        public void ResetWristRpy()
        {
            _javaDeviceGateway.Call("resetWristRpy");
        }

        public void SetHand(int hand)
        {
            _javaDeviceGateway.Call("setHand", hand);
        }

        public void SetGestureConfig(TranslationGestureConfig translationGestureConfig)
        {
            _javaDeviceGateway.Call("setGestureConfig", (sbyte[])(Array)translationGestureConfig.ToByteArray());
        }

        public void SendDeviceCommand(DeviceCommand deviceCommand)
        {
            _javaDeviceGateway.Call("sendDeviceCommand", (sbyte[])(Array)deviceCommand.ToByteArray());
        }

        public void Destroy()
        {
            _javaDeviceGateway.Call("destroy");
        }

        public void onConnectionState(int connectionState)
        {
            AsyncDispatcher.RunOnMainThread(() => OnConnectionStateChanged?.Invoke((ConnectionState)connectionState));
        }

        public void onStreamingState(int streamingState)
        {
            AsyncDispatcher.RunOnMainThread(() => OnStreamingStateChanged?.Invoke((StreamingState)streamingState));
        }

        public void onGesture(sbyte[] gestureBytes)
        {
            var gesture = Gesture.Parser.ParseFrom((byte[])(Array)gestureBytes);
            AsyncDispatcher.RunOnMainThread(() => OnGesture?.Invoke(gesture));
        }

        public void SetGestureConfigs(Calibration[] calibrations, float scaleFactor)
        {
            foreach (Calibration calibration in calibrations)
            {
                switch (calibration.Type)
                {
                    case MovementType.Grip:
                        SetGestureConfig(CreateTranslationGestureConfig(GestureType.Squeeze, calibration, scaleFactor));
                        SetGestureConfig(CreateTranslationGestureConfig(GestureType.Release, calibration, scaleFactor));
                        break;
                    case MovementType.Roll:
                        SetGestureConfig(CreateTranslationGestureConfig(GestureType.Supination, calibration, scaleFactor));
                        SetGestureConfig(CreateTranslationGestureConfig(GestureType.Pronation, calibration, scaleFactor));
                        break;
                    case MovementType.Pitch:
                        SetGestureConfig(CreateTranslationGestureConfig(GestureType.Ulnar, calibration, scaleFactor));
                        SetGestureConfig(CreateTranslationGestureConfig(GestureType.Radial, calibration, scaleFactor));
                        break;
                    case MovementType.Yaw:
                        SetGestureConfig(CreateTranslationGestureConfig(GestureType.Flexion, calibration, scaleFactor));
                        SetGestureConfig(CreateTranslationGestureConfig(GestureType.Extension, calibration, scaleFactor));
                        break;
                }
            }
        }

        private TranslationGestureConfig CreateTranslationGestureConfig(GestureType gestureType, Calibration calibration, float scaleFactor)
        {
            return new TranslationGestureConfig
            {
                Type = gestureType,
                Translation = GestureUtility.GetTranslation(calibration.Min, calibration.Max, gestureType, scaleFactor),
                GestureDuration = GESTURE_DURATION,
                TimeoutDuration = TIMEOUT_DURATION
            };
        }

        public AndroidJavaObject GetDeviceInstance()
        {
            return _javaDeviceGateway.Call<AndroidJavaObject>("getDevice");
        }
    }
}
