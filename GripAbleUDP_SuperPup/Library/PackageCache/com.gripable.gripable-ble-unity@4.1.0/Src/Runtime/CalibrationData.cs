using System;

namespace Gripable
{
    public struct CalibrationData
    {
        public float Min;
        public float Max;

        public static CalibrationData ROLL_LOW = new CalibrationData { Min = 345, Max = 20 };
        public static CalibrationData ROLL_MEDIUM = new CalibrationData { Min = 330, Max = 40 };
        public static CalibrationData ROLL_HIGH = new CalibrationData { Min = 315, Max = 60 };

        public static CalibrationData PITCH_LOW = new CalibrationData { Min = 355, Max = 20 };
        public static CalibrationData PITCH_MEDIUM = new CalibrationData { Min = 350, Max = 30 };
        public static CalibrationData PITCH_HIGH = new CalibrationData { Min = 345, Max = 40 };

        public static CalibrationData YAW_LOW = new CalibrationData { Min = 345, Max = 10 };
        public static CalibrationData YAW_MEDIUM = new CalibrationData { Min = 325, Max = 20 };
        public static CalibrationData YAW_HIGH = new CalibrationData { Min = 310, Max = 30 };

        public static CalibrationData GRIP_LOW = new CalibrationData { Min = 0.01f, Max = 0.5f };
        public static CalibrationData GRIP_MEDIUM = new CalibrationData { Min = 0.1f, Max = 3 };
        public static CalibrationData GRIP_HIGH = new CalibrationData { Min = 0f, Max = 10 };
    }
}
