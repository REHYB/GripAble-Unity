using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Gripable;
using System;
using TMPro;

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
    public GameObject disappear;

    public static float climberPositionMin = -4.7f; // -5.5f ground
    public static float climberPositionMax = 3.0f; // maximum voluntary contraction scalar
    public static float climberPosition = climberPositionMin;
    public static float boneHeight = 0;
    public static float[] boneHeightArray = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
    public static int score = 0;
    public static string instruction = "SuperPup";
    public static string instruction2 = "";
    public static int reps = 0;
    public static float climberForce = 0;
    public static string macAddress = "DB:1C:18:62:82:9C"; //"E1:39:99:37:A0:CA"; //Verity?: "E1:39:99:37:A0:CA";//Aaron masking tape: "CF:C8:03:D7:2E:FC"; //Chiara "D9:EC:A0:72:0B:2C"; //CF:C8:03:D7:2E:FC";//:Aaron Blue1: DB:1C:18:62:82:9C Aaron1: CA:49:AB:EF:4A:17 Leeza1: D5:B2:37:4A:C8:5E Leeza2-thisOne: F9:05:C0:6D:B2:C2 Satoshi1: C9:D9:D6:CA:33:CB Satoshi2: D6:7E:B6:F8:F4:8D Satoshi3 CHIARA: F1:FD:7F:8C:B2:61 Juliette1: CF:C8:03:D7:2E:FC (Blue device at Imperial) //FF:30:2F:47:20:6E
    public static int maxReps = 36;
    public static bool applyUserID = false;
    public static string userID = "notAvailable";
    public static float FESmA = 0f;
    public static float FESmAmax = 50f;
    public static string computerIP = "146.169.191.0";//"127.0.0.1";
    public static float force = 0;
    public static float force1 = 0;
    public static float force2 = 0;
    public static float angleX = 0;
    public static float angleY = 0;
    public static float angleZ = 0;
    public static bool initialize = false;
    public static float climberForceScale = 80f;
    public static float climberForcebias = 0;
    public static int forceStepCounter = 0;
    float timePrev = 0;
    float timeUpdate = 0.1f; // how fast puppy position updates
    float respawnGap = 0.75f;
    public static bool boneDestroy = false;
    public static float stage1 = 50f; //bronze
    public static float stage2 = 150f;//silver
    public static float stage3 = 500f;//gold
    public static float bonesCaught = 0;
    public static bool useGripable = true; // change before sending, also player.cs, also can do mac address 
    public static bool StimOn = false;
    public static float targetPosition = 0;
    public static bool newtarget = false;
    public static bool initGame = false;
    public static float timeStart = 0;
    public static int gameLevel = 0;
    public static int repRepeat = 1;
    public static float eegSignalColor = 0.5f;
    public static int fesCounter = 0;
    public static int fesCounterPrev = 0;
    public static double FESmAEntry = 0;
    public static double[] fesCalib = { 0, 0, 0, 0, 0 };
    float rest = 5; //changed from 2
    float hold = 3;
    int rep = 0;
    int repMax = 5;
    int set = 0;
    int[] order = { 4, 5, 6, 6, 5, 4, 7, 8, 9 };
    float error = 1.5f;
    int subRep = 0;

    void Start() {
    }

    void Update() {
        if (useGripable == true) {
            //Setup
            if (GripablePlugin.Player.IsInitialized() == false && applyUserID == true) {
                GripablePlugin.Player.SetDevice(macAddress); //CA:49:AB:EF:4A:17
                GripablePlugin.Player.Connect();
                instruction = " connecting gripable... ";
            }

            // Get Gripable Data
            else if (GripablePlugin.Player.IsInitialized() && applyUserID == true) {
                if (initialize == false) {
                    instruction = " gripable disconnected - connect through gripable's app first ";
                }
                force = GripablePlugin.Player.GetGripForce();
                angleX = GripablePlugin.Player.GetPitch();
                angleY = GripablePlugin.Player.GetRoll();
                angleZ = GripablePlugin.Player.GetYaw();
                instruction = force.ToString();
                initialize = true;
                climberForce = force;
                climberPosition = (force * (8 + 5) * 8 * 10 / 12.5f) - 8f;
            }
        }

        // Get Data from mouse input instead
        else if (useGripable == false && applyUserID == true) {
            Vector3 mousePos = Input.mousePosition;
            climberForce = (mousePos.y - 700) / 100;
            climberPosition = climberForce;
        }

        //Set new game level
        if (applyUserID == true && initGame == false) {
            timeStart = Time.time;
            initGame = true;
            gameLevel++;
            if (gameLevel == 2) { instruction2 = "Stay relaxed - Adjust mA"; }
            if (gameLevel == 3) { instruction2 = "Play!"; }
        }

        //Game 1: Max Grip Force - Calibration
        //bones at top, superpup decays to max, 5s grip, 3s rest, 3 reps
        if (applyUserID == true && initGame == true && gameLevel == 1) { 
            boneHeight = (float)UDPReceiver.target;
            eegSignalColor = (float)UDPReceiver.eeg;
        }
        if (applyUserID == true && initGame == true && gameLevel == 1 && Time.time > timePrev + respawnGap ) {
            instruction2 = "Let's Play! Power Up with your Mind";
            Instantiate(apple, new Vector3(14.5f, boneHeight, 0), Quaternion.identity);
            timePrev = Time.time;
        }
    }

}