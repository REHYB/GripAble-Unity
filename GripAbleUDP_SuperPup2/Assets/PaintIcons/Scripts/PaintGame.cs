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
    public static string macAddress = "CF:C8:03:D7:2E:FC"; //"F1:FD:7F:8C:B2:61";//:Aaron Blue1: DB:1C:18:62:82:9C Aaron1: CA:49:AB:EF:4A:17 Leeza1: D5:B2:37:4A:C8:5E Leeza2-thisOne: F9:05:C0:6D:B2:C2 Satoshi1: C9:D9:D6:CA:33:CB Satoshi2: D6:7E:B6:F8:F4:8D Satoshi3 CHIARA: F1:FD:7F:8C:B2:61 Juliette1: CF:C8:03:D7:2E:FC (Blue device at Imperial) //FF:30:2F:47:20:6E
    public static int maxReps = 36;
    public static bool applyUserID = false;
    public static string userID = "notAvailable";
    public static float FESmA = 0f;
    public static float FESmAmax = 50f;
    public static string computerIP = "192.168.178.20";
    public static float force = 0;
    public static float force1 = 0;
    public static float force2 = 0;
    public static bool initialize = false;
    public static float climberForceScale = 80f;
    public static float climberForcebias = 0;
    public static int forceStepCounter = 0;
    float timePrev = 0;
    float timeUpdate = 0.1f; // how fast puppy position updates
    float respawnGap = 0.5f;
    public static bool boneDestroy = false;
    public static float stage1 = 500f; //bronze
    public static float stage2 = 1000f;//silver
    public static float stage3 = 1500f;//gold
    public static float bonesCaught = 0;
    public static bool useGripable = true; // change before sending, also player.cs
    public static bool StimOn = false;
    public static float targetPosition = 0;
    public static bool newtarget = false;
    public static bool initGame = false;
    public static float timeStart = 0;
    public static int gameLevel = 0;
    public static int repRepeat = 1;
    public static Color gripSignalColor = new Color(0, 0, 0, 0);
    public static int fesCounter = 0;
    public static int fesCounterPrev = 0;
    float rest = 5f; // changed from 2f
    float hold = 3f;
    int rep = 0;
    int repMax = 5;
    public static int set = 0;
    //can we detect m wave v wave separated and together?
    //becasue 30min max - we do: (1) FES CALIB, (2) ALL FES, (3) NO FES, (4) CUE FES, (5) ADAPT FES, 4,6,3,5 MVC
    int orderSelect = 0; 
    int[] orderSort0 = { 2, 3, 4, 5, 3, 5, 2, 4, 6 }; //6 is an end game glicth stopper
    int[] orderSort1 = { 3, 2, 4, 5, 2, 3, 5, 4, 6 };
    int[] orderSort2 = { 5, 2, 3, 4, 3, 4, 5, 2, 6 };
    int[] orderSort3 = { 4, 5, 2, 3, 5, 4, 2, 3, 6 };
    public static int[] order = { 2, 3, 4, 5, 3, 5, 2, 4, 6 };//2 first
    float error = 1.5f;
    int subRep = 0;
    float mvcTime = 1; //mvc wait time ; use 5
    public static int calibrationReps = 3;
    public static int[] fesCalib = new int[5 * 3];
    public static float[] fesCalib2 = new float[5];
    int fesSetReps = 1;
    public static float scaleGrip = 2.5f;
    public static bool initFESCalib = false;
    public static int fesCalibCounter = 0;
    public static bool fesStop = false;
    public static float scaleChallenge = 1f;
    public static float boneHeightNow = 0f;
    public static float roll = 0f;
    public static float pitch = 0f;
    public static float yaw = 0f;
    public static float appleStartX = 22.7f;
    public static int forceSteps = 5;
    float forceScaling = 11f;
    //Training:
    int[] training1 = { 2, 2, 2, 2, 2, 3, 3, 3, 3, 3, 1, 1, 1, 1, 1, 4, 4, 4, 4, 4 };
    int[] training2 = { 3, 3, 3, 3, 3, 1, 1, 1, 1, 1, 4, 4, 4, 4, 4, 2, 2, 2, 2, 2 };

    //Testing:
    int[] test1 = { 3, 2, 1};
    int[] test2 = { 1, 2, 3 };

    void Start() {
        orderSelect = UnityEngine.Random.Range(0, 4);
        if (orderSelect == 0) { order = orderSort0; }
        else if (orderSelect == 1) { order = orderSort1; }
        else if (orderSelect == 2) { order = orderSort2; }
        else if (orderSelect == 3) { order = orderSort3; }
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
                yaw = GripablePlugin.Player.GetYaw();
                pitch = GripablePlugin.Player.GetPitch();
                roll = GripablePlugin.Player.GetRoll();
                instruction = force.ToString();
                initialize = true;
                climberForce = force;
                climberPosition = (force * (8 + 5) * 8 * 10 / forceScaling) - 8f;
                if (climberPosition < -8) { climberPosition = -8; }
                else if (climberPosition > 5) { climberPosition = 5; }
            }
        }

        // Get Data from mouse input instead
        else if (useGripable == false && applyUserID == true) {
            Vector3 mousePos = Input.mousePosition;
            climberForce = (mousePos.y - 700) / 100;
            climberPosition = climberForce;
            if (climberPosition < -8) { climberPosition = -8; }
            else if (climberPosition > 5) { climberPosition = 5; }
        }

        //Set new game level
        if (applyUserID == true && initGame == false) {
            timeStart = Time.time;
            initGame = true;
            gameLevel++;
        }

        //Game 12: Max Grip Force - Calibration - DONE
        //bones at top, superpup decays to max, 5s grip, 3s rest, 3 reps
        //if (applyUserID == true && initGame == true && gameLevel == 12 && instruction2.Contains("Grip Your Hardest")) { gripSignalColor = new Color(1 - (climberPosition + 8) / (5 + 8), 1, 1 - (climberPosition + 8) / (5 + 8), 1); }
        //else if (gameLevel == 1 && instruction2.Contains("Relax")) { gripSignalColor = new Color(1, 0.5f, 0.5f, 1); }
        //if (applyUserID == true && initGame == true && gameLevel == 1 && Time.time > timePrev + respawnGap) {
        //    if ((Time.time - timeStart > mvcTime && Time.time - timeStart < mvcTime*2) || (Time.time - timeStart > mvcTime*3 && Time.time - timeStart < mvcTime*4) || (Time.time - timeStart > mvcTime*5 && Time.time - timeStart < mvcTime*6)) { boneHeight = 5; instruction2 = "Grip Your Hardest"; }
        //    else { boneHeight = -8f; instruction2 = "Relax"; }
        //    //Instantiate(apple, new Vector3(14.5f, boneHeight, 0), Quaternion.identity);
        //    timePrev = Time.time;

        //}

        //Game 1: Synch Start
        if (applyUserID == true && initGame == true && gameLevel == 1) {
            instruction2 = "Relax";
            if (fesCounter > fesCounterPrev) { initGame = false; fesCounter = 0; fesCounterPrev = 0; }
        }

        //Game 2: FES Calibration, show bone at 2.5N, user stays relaxed - DONE
        if (applyUserID == true && initGame == true && gameLevel == 2 && Time.time > timePrev + respawnGap) {
            instruction2 = "Relax";
            boneHeight = -8f + (5 + 8) * scaleGrip * (fesCounter) / forceScaling;
            if (fesCounter < forceSteps) {Instantiate(apple, new Vector3(appleStartX, boneHeight, 0), Quaternion.identity);}
            timePrev = Time.time;
            if (fesCounter > fesCounterPrev) {
                fesCounterPrev = fesCounter;
                if (fesCounterPrev == forceSteps && fesSetReps >= calibrationReps ) {
                    fesCalib2[0] = (fesCalib[0] + fesCalib[5] + fesCalib[10]) / 3;
                    fesCalib2[1] = (fesCalib[1] + fesCalib[6] + fesCalib[11]) / 3;
                    fesCalib2[2] = (fesCalib[2] + fesCalib[7] + fesCalib[12]) / 3;
                    fesCalib2[3] = (fesCalib[3] + fesCalib[8] + fesCalib[13]) / 3;
                    fesCalib2[4] = (fesCalib[4] + fesCalib[9] + fesCalib[14]) / 3;
                    initGame = false;
                }
                else if (fesCounterPrev == forceSteps && fesSetReps < calibrationReps) { fesSetReps++; fesCounter = 0; fesCounterPrev = 0; }
            }
        }

        //Game 3: Synch Start
        if (applyUserID == true && initGame == true && gameLevel == 3) {
            if (fesCounter > fesCounterPrev) {
                initGame = false; fesCounter = 0; fesCounterPrev = 0;
                //copied from gamelevel 4
                if (order[set] == 2 && set < 4) { FESmA = fesCalib2[training1[rep]]; instruction2 = "Relax"; }
                else if (order[set] == 3 && set < 4) { FESmA = 0; instruction2 = "Drücken"; }
                else if (order[set] == 4 && set < 4) { FESmA = fesCalib2[1]; instruction2 = "Drücken"; }
                else if (order[set] == 5 && set < 4) { FESmA = scaleChallenge * (fesCalib2[training1[rep]] - fesCalib2[0]) + fesCalib2[0]; instruction2 = "Drücken"; }
                else if (order[set] == 2 && set >= 4) { FESmA = fesCalib2[training2[rep]]; instruction2 = "Relax"; }
                else if (order[set] == 3 && set >= 4) { FESmA = 0; instruction2 = "Drücken"; }
                else if (order[set] == 4 && set >= 4) { FESmA = fesCalib2[1]; instruction2 = "Drücken"; }
                else if (order[set] == 5 && set >= 4) { FESmA = scaleChallenge * (fesCalib2[training2[rep]] - fesCalib2[0]) + fesCalib2[0]; instruction2 = "Drücken"; }
            }
        }

        //Order 1 || 2 || 3 3 7 || 4 3 7 || 5 3 7 || 6 3 7 || 6 3 7 || 5 3 7 || 4 3 7 || 6 8 || 5 8 || 4 8
        // Game Level 4: Test and Train
        if (initGame == true && gameLevel == 4 && rep >= (training1.Length)) { //training1 and training2 same length
            set++; rep = 0;
            if (set >= order.Length-1) { gameLevel = 5; initGame = false; } // end game
            else { gameLevel = 2; initGame = false; }

            if (order[set] == 3) { fesStop = true; }
            else { fesStop = false; }

            if (order[set] == 5) { scaleChallenge = 1f; }
        }
        else if (initGame == true && gameLevel == 4 && (Time.time - timeStart) >= (hold + rest)) {
            timeStart = Time.time; rep++;
            timePrev = Time.time;
            if (rep < (training1.Length)) { 
                Instantiate(apple, new Vector3(appleStartX, -8f, 0), Quaternion.identity);
                //copied from gamelevel 4
                if (order[set] == 2 && set < 4) { FESmA = fesCalib2[training1[rep]]; instruction2 = "Relax"; }
                else if (order[set] == 3 && set < 4) { FESmA = 0; instruction2 = "Drücken"; }
                else if (order[set] == 4 && set < 4) { FESmA = fesCalib2[1]; instruction2 = "Drücken"; }
                else if (order[set] == 5 && set < 4) { FESmA = scaleChallenge * (fesCalib2[training1[rep]] - fesCalib2[0]) + fesCalib2[0]; instruction2 = "Drücken"; }
                else if (order[set] == 2 && set >= 4) { FESmA = fesCalib2[training2[rep]]; instruction2 = "Relax"; }
                else if (order[set] == 3 && set >= 4) { FESmA = 0; instruction2 = "Drücken"; }
                else if (order[set] == 4 && set >= 4) { FESmA = fesCalib2[1]; instruction2 = "Drücken"; }
                else if (order[set] == 5 && set >= 4) { FESmA = scaleChallenge * (fesCalib2[training2[rep]] - fesCalib2[0]) + fesCalib2[0]; instruction2 = "Drücken"; }
            }
        }

        //Game Level 4: Train: (2) ALL FES, (3) NO FES, (4) CUE FES, (5) ADAPT FES
        else if (initGame == true && gameLevel == 4 && Time.time >= timePrev + respawnGap) {
            if (Time.time - timeStart < rest) {
                boneHeight = -8f;
            }
            else if (Time.time - timeStart < hold + rest) {
                if (set < 4) { boneHeight = -8f + (5 + 8) * scaleGrip * training1[rep] / forceScaling; }
                else if (set >= 4) { boneHeight = -8f + (5 + 8) * scaleGrip * training2[rep] / forceScaling; }

                if (order[set] == 2 && set < 4) { FESmA = fesCalib2[training1[rep]]; instruction2 = "Relax"; }
                else if (order[set] == 3 && set < 4) { FESmA = 0; instruction2 = "Drücken"; }
                else if (order[set] == 4 && set < 4) { FESmA = fesCalib2[1]; instruction2 = "Drücken"; }
                else if (order[set] == 5 && set < 4) { FESmA = scaleChallenge*(fesCalib2[training1[rep]] - fesCalib2[0]) + fesCalib2[0]; instruction2 = "Drücken"; }
                else if (order[set] == 2 && set >= 4) { FESmA = fesCalib2[training2[rep]]; instruction2 = "Relax"; }
                else if (order[set] == 3 && set >= 4) { FESmA = 0; instruction2 = "Drücken"; }
                else if (order[set] == 4 && set >= 4) { FESmA = fesCalib2[1]; instruction2 = "Drücken"; }
                else if (order[set] == 5 && set >= 4) { FESmA = scaleChallenge * (fesCalib2[training2[rep]] - fesCalib2[0]) + fesCalib2[0]; instruction2 = "Drücken"; }
            }
            Instantiate(apple, new Vector3(appleStartX, boneHeight, 0), Quaternion.identity);
            timePrev = Time.time;
        }
        //to add - subreps x5 for training (or hardcode), training2 and test2, remember the goal - no FES, FES, smart FES

        if (applyUserID == true && initGame == true && gameLevel == 6 ) {
            instruction2 = "Super, Danke!";
        }
    }
}