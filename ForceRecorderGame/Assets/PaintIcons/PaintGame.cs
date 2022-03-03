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
    public Texture2D cursor;

    public static int applesToGrab = 0;
    public static float climberPositionMin = -5f; // -5.5f ground
    public static float climberPositionMax = 1f; // maximum voluntary contraction scalar
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
    public static string instruction = "";
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
    public static string macAddress = "CF:C8:03:D7:2E:FC";//:Aaron Blue1: DB:1C:18:62:82:9C Aaron1: CA:49:AB:EF:4A:17 Leeza1: D5:B2:37:4A:C8:5E Leeza2: F9:05:C0:6D:B2:C2 Satoshi1: C9:D9:D6:CA:33:CB Satoshi2: D6:7E:B6:F8:F4:8D Satoshi3: F1:FD:7F:8C:B2:61 Juliette1: FF:30:2F:47:20:6E
    public static int maxReps = 36;
    public static int maxCalibReps = 10000; // if you change this you need to change Save.cs
    public static float[] mvcCal = new float[maxCalibReps];
    public static string noFailYes = "3";
    public static bool applyUserID = false;
    public static string userID = "notAvailable";
    public static float force = 0;
    public static float forceMin = 0.01f;
    public static float delayTime = 3f;
    public static int counter = 0;
    public static int waitInc = 3;
    public static bool initialize = false;
    public static int repCounter = 0;
    public static Color appleColor = Color.white;
    public static int stimCounter = 2;
    public static int forceCounter = 1;
    public static Color idButtonColor = Color.white;
    public static bool idButtoninteractable = true;
    public static float holdTime = 6f;
    public static float angleYaw = 0f;
    public static float anglePitch = 0f;
    public static float angleRoll = 0f;
    float previousRespawn = 0f;
    public static float climberForceScale = 80f;
    public static bool repStarted = false;
    public static bool restStarted = false;
    public static float repStart = 0f;
    public static float repTimer = 30f;
    public static float appleHeightPrevious = -4.8f;
    public static int bonesCaught = 0;
    public static int stage = 0;
    public static float forceStep = 1.25f;
    public static int forceStepCounter = 0;
    public static float appleHeight = -4.5f;
    public static float forceBias = 0f;
    public static bool init = false;
    public static float timeBias = 0f;
    public static bool init2 = false;
    public static float appleHeight1 = -4.5f;
    public static float appleHeight2 = 1.5f;
    public static bool timeGrip = false;
    public static float climberForceBiased = 0f;

    void Start() {
        GripablePlugin.Player.SetDevice(macAddress); //CA:49:AB:EF:4A:17
        GripablePlugin.Player.Connect();

    }

    void Update() {
        instruction = " update ";
        if (GripablePlugin.Player.IsInitialized()) {
            instruction = " initializing ";
            force = GripablePlugin.Player.GetGripForce();
            climberForce = force ;
            instruction = " force ";
            selectAngle = GripablePlugin.Player.GetRoll();// + GripablePlugin.Player.GetRoll() + GripablePlugin.Player.GetPitch();
            instruction = " yaw ";
            instruction = " ready ";
            if (init == false) { forceBias = GripablePlugin.Player.GetGripForce(); init = true; }
        }

        if (applyUserID == true && Time.time > previousRespawn + 0.5f) {
            if (init2 == false) { timeBias = Time.time; init2 = true; }

            if ((Time.time - timeBias) < 3) { timeGrip = false; }
            else if ((Time.time - timeBias) < 6) { timeGrip = true; }
            else { timeBias = Time.time; timeGrip = false; }

            if (timeGrip == false) { Instantiate(apple, new Vector3(14.5f, appleHeight1, 0), Quaternion.identity); }
            else if (timeGrip == true) { Instantiate(apple, new Vector3(14.5f, appleHeight2, 0), Quaternion.identity); }

            previousRespawn = Time.time;
        }
        climberForceBiased = force - forceBias;
        climberPosition = ((force- forceBias) * climberForceScale) - 4.5f;
    }
}