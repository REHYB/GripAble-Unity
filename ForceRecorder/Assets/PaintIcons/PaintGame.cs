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
    public static float climberPositionMin = -6.5f; // -5.5f ground
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
    public static string macAddress = "F1:FD:7F:8C:B2:61";//:Aaron1: CA:49:AB:EF:4A:17 Leeza1: D5:B2:37:4A:C8:5E Leeza2: F9:05:C0:6D:B2:C2 Satoshi1: C9:D9:D6:CA:33:CB Satoshi2: D6:7E:B6:F8:F4:8D Satoshi3: F1:FD:7F:8C:B2:61
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
    bool initialize = false;
    public static int repCounter = 0;
    public static float appleHeight = (0 * 5) + -2.69f;
    public static Color appleColor = Color.white;

    bool timerStarted = false;
    float startTime = 0;
    public static int stimCounter = 2;
    public static int forceCounter = 1;
    public static Color idButtonColor = Color.white;
    public static bool idButtoninteractable = true;
    public static float holdTime = 6f;
    public static float angleYaw = 0f;

    void Start() {
        GripablePlugin.Player.SetDevice(macAddress); //CA:49:AB:EF:4A:17
        GripablePlugin.Player.Connect();
    }

    void Update() {
        //instruction = " update ";
        if (GripablePlugin.Player.IsInitialized()) {
            if (initialize == false) {
                instruction = " initializing - press gripable latch down halfway ";
            }
            force = GripablePlugin.Player.GetGripForce();
            angleYaw = GripablePlugin.Player.GetYaw();// + GripablePlugin.Player.GetRoll() + GripablePlugin.Player.GetPitch();
            initialize = true;
            climberForce = force;
            //instruction = " recording force ";
            //instruction = " yaw ";
            //instruction = " Ready! ";
        }

        climberPosition = (force * 8) - 4.8f;
        //Physical Setup - Open this App, Place Myo with LED on dorsal side of Arm, Place FES, Place Gripable in Hand, Turn on Python App
        //No need to record location of each sEMG sensor

        //Objective: Detect voluntary muscle activation in stroke patients; Develop EMG estimator for grip force
        if (counter == 0) { instruction = " Enter User ID "; }
        if (counter == 1) { instruction = " Place Myo on forearm near elbow "; }
        if (counter == 2) { instruction = " Center Myo's blue light on dorsal side of arm "; }
        if (counter == 3) { instruction = " Sync Myo armband by holding wrist extended "; }
        if (counter == 4) { instruction = " Place last big FES electrode just distal to Myo "; }
        if (counter == 5) { instruction = " Place first big FES electrode just distal to other electrode "; }
        if (counter == 6) { instruction = " Place GripAble in hand and fasten strap "; }
        if (counter == 7) { instruction = " Start Python program to record EMG "; }
        if (counter == 8) { instruction = " Start FES App "; }
        if (counter == 9) { instruction = " Select first Big electrode pad as Anode "; }
        if (counter == 10) { instruction = " Select last Big electrode pad as Cathode "; }
        if (counter == 11) { instruction = " Set Stimulation Parameters - 35 Hz, 300us "; }
        if (counter == 12) { instruction = " Verify that Myo is Synced and Python is Recording "; }
        if (counter == 13) { instruction = " Setup Complete, Well Done! "; }

        //Level 0 - Calibration: Relax, (Stim 1 -> consecutive electrodes) x3, (Stim 2 -> ...), up to discomfort level (1-5) 
        if (counter == 14 && repCounter < 5) { instruction = " Start of FES Calbiration: Keep your hand as Relaxed as possible "; }
        if (counter >= 15 && counter <=17 && repCounter < 5) {
            if (stimCounter <= 12) {
                if (counter == 15) { instruction = " Set Stimulation to " + stimCounter + " mA "; timerStarted = false; }
                if (counter == 16) { instruction = " Click Test, skip if painful "; }
                if (counter == 17) {
                    if (timerStarted == false) {
                        startTime = Time.time;
                        timerStarted = true;
                    }
                    if (timerStarted == true && (Time.time - startTime < (holdTime/2))) { idButtoninteractable = false; }
                    else { idButtoninteractable = true; counter = 15; stimCounter = stimCounter + 2; }
                }
            }
            else { repCounter++; stimCounter = 2; } // change counter# to match start
        }

        //Level 1 - Grip Without FES: Relax, (Stim 1 -> consecutive electrodes) x3, (Stim 2 -> ...), up to discomfort level (1-5) 
        if (counter == 15 && repCounter >= 5 && repCounter < 10) {
            instruction = " Start of Grip Tests ";
            Instantiate(apple, new Vector3(0.82f, appleHeight, 0), Quaternion.identity);
        }

        if ((climberPosition + 4.8 > (appleHeight + 2.69 - 0.25)) && ((climberPosition + 4.8) < (appleHeight + 2.69 + 0.25))) {
            appleColor = Color.yellow;
        }
        else { appleColor = Color.white; }

        if (counter >= 16 && counter <= 18 && repCounter >= 5 && repCounter < 10) {
            if (counter == 16) {
                instruction = " Relax ";
                appleHeight = (0 * 8) + -2.69f;
                timerStarted = false;
            }
            if (counter == 17) { instruction = " Maintain Grip on Apple "; }
            if (counter == 18) {
                appleHeight = (0.1f * (forceCounter) * 8) + -2.69f;
                if (timerStarted == false) {
                    startTime = Time.time;
                    timerStarted = true;
                }
                if (timerStarted == true && (Time.time - startTime >= holdTime)) {
                    counter = 16; forceCounter++;
                    if (forceCounter == 5) {
                        forceCounter = 1;
                        repCounter++;
                    }
                }
                if (timerStarted == true && (Time.time - startTime < holdTime)) { idButtoninteractable = false; }
                else { idButtoninteractable = true; }
            }
        }

        //Level 2 - During FES: (Stim 1 -> Grip 1, Relax, Grip 2, Relax, Grip 3, Relax) x10, (Stim 2 -> ...), (Stim 3 -> ...)
        if (counter == 16 && repCounter >= 10 && repCounter < 15) {
            instruction = " Start of FES + Grip Tests ";
            stimCounter = 2;
        }
        if (counter >= 17 && counter <= 23 && repCounter >= 10 && repCounter < 15) {
            if (counter == 17) {
                instruction = " Relax ";
                appleHeight = (0 * 8) + -2.69f;
                timerStarted = false;
            }
            if (counter == 18) { instruction = " Set Stimulation to " + stimCounter + " mA "; }
            if (counter == 19) {
                instruction = " Maintain Grip on Apple before, during and after Stimulation ";
            }
            if (counter == 20) {
                appleHeight = (0.1f * (forceCounter) * 8) + -2.69f;
                if (timerStarted == false) {
                    startTime = Time.time;
                    timerStarted = true;
                }
                if (timerStarted == true && (Time.time - startTime < (holdTime / 2))) { idButtoninteractable = false; }
                else { idButtoninteractable = true; counter++; timerStarted = false; }
            }
            if (counter == 21) { 
                instruction = " Click Test, Maintain Grip on Apple, skip if painful ";
            }
            if (counter == 22) {
                if (timerStarted == false) {
                    startTime = Time.time;
                    timerStarted = true;
                }
                if (timerStarted == true && (Time.time - startTime < (holdTime / 2))) { idButtoninteractable = false; }
                else { idButtoninteractable = true; counter++; }
            }
            if (counter == 23) {
                stimCounter = stimCounter + 2; counter = 17;
                if (stimCounter > 12) { forceCounter++; stimCounter = 2; }
                if (forceCounter == 5) { repCounter++; forceCounter = 1; }
            }
        }

        if (repCounter >= 15) { instruction = " Please close the program. Study Complete, Thank you! "; }
    }
}
