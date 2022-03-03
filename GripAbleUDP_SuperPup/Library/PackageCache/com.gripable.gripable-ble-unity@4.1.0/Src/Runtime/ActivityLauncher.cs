using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Protos;


namespace Gripable
{
    public sealed class ActivityLauncher
    {
        private static ActivityLauncher _instance;
        private static ActivityLauncher Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ActivityLauncher();
                }
                return _instance;
            }
        }

        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static ActivityLauncher()
        { 
            var DummyData = new ActivityConfig
            {
                AutoConnect = true,
                DemoUser = false,
                Gender = Gender.Other,
                Hand = Hand.Left,
                LaunchedFromPackageName = "testPackage",
                //MacAddress = "00:00:00:00:00:00",
                PatientUid = "dummyData",
                ShowTutorial = true,
                Username = "testUser",
                GripCalibration = new Calibration { Max = CalibrationData.GRIP_MEDIUM.Max, Min = CalibrationData.GRIP_MEDIUM.Min, Type = MovementType.Grip },
                RollCalibration = new Calibration { Max = CalibrationData.ROLL_MEDIUM.Max, Min = CalibrationData.ROLL_MEDIUM.Min, Type = MovementType.Roll },
                PitchCalibration = new Calibration { Max = CalibrationData.PITCH_MEDIUM.Max, Min = CalibrationData.PITCH_MEDIUM.Min, Type = MovementType.Pitch },
                YawCalibration = new Calibration { Max = CalibrationData.YAW_MEDIUM.Max, Min = CalibrationData.YAW_MEDIUM.Min, Type = MovementType.Yaw },
                ScaleFactor = 0.3,
                DailyGoals = new DailyProgress
                {
                    Grip = new MovementProgress
                    { 
                        Repetitions = 100,
                        Seconds = 60,
                        Type = MovementType.Grip
                    },
                    Pitch = new MovementProgress
                    {
                        Repetitions = 24,
                        Seconds = 74,
                        Type = MovementType.Pitch 
                    },
                    Yaw = new MovementProgress
                    {
                        Repetitions = 56,
                        Seconds = 204,
                        Type = MovementType.Yaw
                    },
                    Roll = new MovementProgress
                    {
                        Repetitions = 13,
                        Seconds = 122,
                        Type = MovementType.Roll
                    }
                },
                DailyReport = new DailyProgress
                {
                    Grip = new MovementProgress
                    {
                        Repetitions = 12,
                        Seconds = 45,
                        Type = MovementType.Grip
                    },
                    Pitch = new MovementProgress
                    {
                        Repetitions = 4,
                        Seconds = 13,
                        Type = MovementType.Pitch
                    },
                    Yaw = new MovementProgress
                    {
                        Repetitions = 95,
                        Seconds = 77,
                        Type = MovementType.Yaw
                    },
                    Roll = new MovementProgress
                    {
                        Repetitions = 100,
                        Seconds = 177,
                        Type = MovementType.Roll
                    }
                },
            };

            if (Application.platform.Equals(RuntimePlatform.Android))
            {
                byte[] launchDataBytes = AndroidHelper.GetExtraByteArrayDataFromLaunchIntent("activityConfig");
                if (launchDataBytes != null)
                {
                    Instance._activityConfig = ActivityConfig.Parser.ParseFrom(launchDataBytes);
                }
                else
                {
                    Instance._activityConfig = DummyData;
                }
            }
            else
            {
                DummyData.MacAddress = "00:00:00:00:00:00";
                Instance._activityConfig = DummyData;
            }

        }

        private ActivityConfig _activityConfig;
        public static ActivityConfig Config
        {
            get
            {
                return Instance._activityConfig;
            }
        }

        public static void Exit()
        {
            if (Application.platform.Equals(RuntimePlatform.Android) && !string.IsNullOrEmpty(_instance._activityConfig.LaunchedFromPackageName))
            {
                GripablePlugin.Player.GetDevice().Destroy();
                bool hasLaunched = AndroidHelper.LaunchApp(_instance._activityConfig.LaunchedFromPackageName);
                if (hasLaunched)
                {
                    AndroidHelper.HideCurrentActivityFromRecents(true);
                    //Application.Quit();
                }
            }
            else
            {
                AndroidHelper.HideCurrentActivityFromRecents(true);
                Application.Quit();
            }
        }
    }
}

