using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gripable;
using System;



public class PaintGame : MonoBehaviour {
    //Program State = RelaxCal - check that mouse is low and centred - relax instruction
    //Program State = SqueezeCal - check that mouse is low and centred - relax instruction
    //Program State = RelaxSelect - check that mouse is low and centred - relax instruction
    //Program State = Select - check that mouse is right or left - select Yes or No
    //Program State = No - Explode apples and wait N seconds - go to Relax1 
    //Program State = RelaxYes - check that mouse is low and centred - relax instruction
    //Program State = Yes - check that mouse is high
    //Program State = Success - Explode apples and wait N seconds - go to Relax1

    //COPY AND PASTE THE FOLLOWING FORCE CALIBRATION INTO Gripable BLE > Plugins > Src > Runtime> Player.cs > Connect

    //GripablePlugin.Player.SetCalibration(
    //new Calibration { Min = 0, Max = 70, Type = MovementType.Grip},
    //new Calibration { Min = 315, Max = 60, Type = MovementType.Roll },
    //new Calibration { Min = 345, Max = 40, Type = MovementType.Pitch },
    //new Calibration { Min = 310, Max = 30, Type = MovementType.Yaw }, 0.4f);

    //Find MAC Address – open gripable app - ||| - support – terms & privacy – double tap bottom right corner

    public Transform climber;
    public Transform apple;
    public Transform wall;
    public Transform apple8;
    public Texture2D cursor;

    public static int applesToGrab = 0;
    public static float climberPositionMin = -4.7f; // -5.5f ground
    public static float climberPositionMax = 3.0f; // maximum voluntary contraction scalar
    public static float climberMaxX = 2; // maximum voluntary contraction scalar
    public static float climberPosition = climberPositionMin;
    public static float challengeHeight = 0;
    public static bool climberPositionLocked = true;
    public static string programState = "Settings";//"RelaxCal"
    public static bool destroyApples = false;
    public static bool yesSelected = false;
    public static bool noSelected = false;
    public static bool pushSelected = false;
    public static int score = 0;
    public static bool instantiateApples = true;
    public static float waitTime = 5f;
    public static float secondsStart = 0;
    public static string instruction = "SuperPup";
    public static int reps = 0;
    public static float mvc = 1f; // maximum voluntary contraction scalar // normalize to user
    public static Color climberColor = Color.grey;
    public static Color yesButtonColor = Color.grey;
    public static Color noButtonColor = Color.grey;
    public static float climberForce = 0;
    public static float selectAngle = 0;
    public static float selectAngleMax = 0.2f;
    public static Vector2 glovePosition = new Vector2(0.8f, -6.2f);
    public static Color gloveColor = new Color(1, 1, 1, 0);
    public static string macAddress = "CF:C8:03:D7:2E:FC";//:Aaron Blue1: DB:1C:18:62:82:9C Aaron1: CA:49:AB:EF:4A:17 Leeza1: D5:B2:37:4A:C8:5E Leeza2-thisOne: F9:05:C0:6D:B2:C2 Satoshi1: C9:D9:D6:CA:33:CB Satoshi2: D6:7E:B6:F8:F4:8D Satoshi3: F1:FD:7F:8C:B2:61 Juliette1: CF:C8:03:D7:2E:FC (Blue device at Imperial) //FF:30:2F:47:20:6E
    public static int maxReps = 36;
    public static int maxCalibReps = 10000; // if you change this you need to change Save.cs
    public static float[] mvcCal = new float[maxCalibReps];
    public static string noFailYes = "3";
    public static bool applyUserID = false;
    public static string userID = "notAvailable";
    public static string computerIP = "0.0.0.0";
    public static float force = 0;
    public static float forceMin = 0.01f;
    public static float delayTime = 3f;
    public static int counter = 0;
    public static int waitInc = 3;
    public static bool initialize = false;
    public static int repCounter = 0;
    public static float appleHeight = -4.8f;
    public static Color appleColor = Color.white;

    public static int stimCounter = 2;
    public static int forceCounter = 1;
    public static Color idButtonColor = Color.white;
    public static bool idButtoninteractable = true;
    public static float holdTime = 6f;
    public static float angleYaw = 0f;
    public static float anglePitch = 0f;
    public static float angleRoll = 0f;
    public static float climberForceScale = 70f;
    public static bool repStarted = false;
    public static bool restStarted = false;
    public static float repStart = 0f;
    public static float repTimer = 30f;
    public static float appleHeightPrevious = -4.8f;
    public static float bonesCaught = 0;
    public static int stage = 0;
    public static float forceStep = 1.25f;
    public static int forceStepCounter = 0;
    public static bool inputTypeSelected = false;
    public static bool protocolSelected = false;
    public static int inputType = 0;
    public static int protocol = 0;
    public static bool tapDetected = false;
    float timePrev = 0;
    float timeUpdate = 0.1f; // how fast puppy position updates
    public static float challengeTap = 0.9f;  // how fast puppy rises on taps
    float fallSpeed = 0.5f; // how fast puppy falls
    float respawnGap = 3;
    float previousRespawn = -3;
    public static int bonesCreated = 0;
    public static bool wallContact = false;
    public static bool boneDestroy = false;
    public static int trials = 0;
    public static int tagDestroy = 0;
    static float m1 = climberPositionMax - climberPositionMin;
    static float m2 = climberPositionMin + 0.7f;
    public static float[] appleHeightVector = new float[] { m2+m1*0/6, m2+m1*1/6, m2+m1*2/6, m2+m1*3/6, m2+m1*4/6, m2+m1*5/6, m2+m1*6/6 };
    public static int reward = 1;
    public static bool success = false;
    public static float stage1 = 50f; //bronze
    public static float stage2 = 150f;//silver
    public static float stage3 = 500f;//gold
    public static int instantiateTarget = 0;
    public static float bias = 0;
    public static bool init = false;

    void Start() {
    }

    void Update() {
        if (GripablePlugin.Player.IsInitialized() == false) {
            GripablePlugin.Player.SetDevice(macAddress); //CA:49:AB:EF:4A:17
            GripablePlugin.Player.Connect();
            instruction = " connecting gripable... ";
        }
        //instruction = " update ";

        // Get Gripable Data
        else if (GripablePlugin.Player.IsInitialized()) {
            if (initialize == false) {
                instruction = " gripable disconnected - connect through gripable's app first ";
            }
            force = GripablePlugin.Player.GetGripForce();
            if (init == false) { bias = force; init = true; }
            instruction = force.ToString();
            angleYaw = GripablePlugin.Player.GetYaw();
            anglePitch = GripablePlugin.Player.GetPitch();
            angleRoll = GripablePlugin.Player.GetRoll();
            initialize = true;
            climberForce = force - bias;
            climberPosition = (climberForce * (float)UDPReceiver.sharedValue2) - 7.5f;
        }
        if (applyUserID == true && instantiateTarget == 0) {
            Vector3 position = new Vector3(0f, -7.5f, 0f);
            Instantiate(apple, position, Quaternion.identity);
            instantiateTarget = 1;
        }
    }
}