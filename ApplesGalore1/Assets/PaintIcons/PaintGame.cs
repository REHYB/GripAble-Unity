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
    public static Vector2 glovePosition = new Vector2 (0.8f, -6.2f);
    public static Color gloveColor = new Color(1, 1, 1, 0);
    public static string macAddress = "F9:05:C0:6D:B2:C2";//:Aaron1: CA:49:AB:EF:4A:17 Leeza1: D5:B2:37:4A:C8:5E Leeza2: F9:05:C0:6D:B2:C2
    public static int maxReps = 36;
    public static int maxCalibReps = 5; // if you change this you need to change Save.cs
    public static float[] mvcCal = new float[maxCalibReps];
    public static string noFailYes = "3";
    public static bool applyUserID = false;
    public static string userID = "notAvailable";
    public static float force = 0;
    public static float forceMin = 0.01f;
    int applesToGrabIncrement = 0;
    int challengeHeightIncrement = 5;
    int[] applesToGrabArray = { 4, 6, 3, 5, 1, 2, 2, 1, 5, 6, 3, 4, 1, 3, 2, 4, 5, 6, 2, 3, 1, 4, 5, 6, 5, 1, 6, 4, 3, 2, 2, 6, 5, 4, 1, 3 };
    float[] challengeHeightArray = { 2, 4, 6, 1, 3, 5, 1, 5, 3, 6, 2, 4, 1, 4, 3, 6, 5, 2, 4, 5, 2, 1, 6, 3, 2, 4, 5, 3, 1, 6, 2, 1, 4, 5, 6, 3 };

    void Start() {
        GripablePlugin.Player.SetDevice(macAddress); //CA:49:AB:EF:4A:17
        GripablePlugin.Player.Connect();
    }

        //Vector2 mousePosition = new Vector2(0.0f, 0.0f);
        Vector2 objPosition = new Vector2(0.0f, 0.0f);

    void Update() {
        instruction = " update ";
        if (GripablePlugin.Player.IsInitialized()) {
            instruction = " initializing ";
            force = GripablePlugin.Player.GetGripForce();
            climberForce = force * (1/mvc) * (climberPositionMax - climberPositionMin) + climberPositionMin;
            instruction = " force ";
            selectAngle = GripablePlugin.Player.GetYaw();// + GripablePlugin.Player.GetRoll() + GripablePlugin.Player.GetPitch();
            instruction = " yaw ";
            instruction = " ready ";
        }
        //score = (int)(selectAngle*100);
        //mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); // change climberForce to mousepostion below for Gripable-less game
        Cursor.SetCursor(cursor, new Vector2(climberForce, -5.8f), CursorMode.Auto);
        if (reps>=(maxReps + maxCalibReps)) { //  36 game reps complete
            instruction = "Nice Work!";
        }

        else if (programState.Equals("RelaxCal")) {
            //Cursor.visible = false;
            gloveColor = new Color(1, 1, 1, 0);
            climberColor = Color.white;
            secondsStart = secondsStart + Time.deltaTime;
            instruction = "Relax Hand";
            GripablePlugin.Player.ResetWristRpy();
            if (climberForce > climberPositionMax + climberMaxX) { climberPosition = climberPositionMax + climberMaxX; }
            else { climberPosition = climberForce; }
            if (secondsStart > (waitTime) && force < forceMin) {
                if (reps < maxCalibReps) {
                    programState = "SqueezeCal";
                }
                else {
                    Array.Sort(mvcCal);
                    mvc = mvcCal[(int)Mathf.Floor(maxCalibReps/2f)];
                    programState = "RelaxSelect";
                }
                secondsStart = 0;
            }
        }

        else if (programState.Equals("SqueezeCal")) {
            //Cursor.visible = false;
            gloveColor = new Color(1, 1, 1, 0);
            climberColor = Color.white;
            secondsStart = secondsStart + Time.deltaTime;
            instruction = "Squeeze Tight";
            mvcCal[reps] = Mathf.Max(mvcCal[reps], GripablePlugin.Player.GetGripForce());
            if (climberForce > climberPositionMax + climberMaxX) { climberPosition = climberPositionMax + climberMaxX; }
            else { climberPosition = climberForce; }
            if (secondsStart > (waitTime) && force > 0.01) {
                programState = "RelaxCal";
                secondsStart = 0;
                reps++;
                //save relax value
            }
        }

        else if (programState.Equals("RelaxSelect")) {
            //Cursor.visible = true;
            glovePosition = new Vector2(0.8f + selectAngle * -5, -6.2f);
            gloveColor = new Color(1, 1, 1, 1);
            climberColor = Color.grey;
            instruction = "Centre Glove";
            if (selectAngle > -selectAngleMax && selectAngle < selectAngleMax) {
                programState = "GenerateApples";
                secondsStart = 0;
            }
        }

        else if (programState.Equals("GenerateApples")) {
            //Cursor.visible = true;
            glovePosition = new Vector2(0.8f + selectAngle * -5, -6.2f);
            gloveColor = new Color(1, 1, 1, 1);
            climberColor = Color.grey;
            applesToGrab = applesToGrabArray[applesToGrabIncrement];//(1,5)
            challengeHeight = ((challengeHeightArray[applesToGrabIncrement]+5)*0.1f) * (climberPositionMax- climberPositionMin)+ climberPositionMin;
            applesToGrabIncrement++;
            destroyApples = false;
            for (int i = 0; i < applesToGrab; i++) {
                objPosition = new Vector2(1f * (0.5f + i - (float)applesToGrab / 2), challengeHeight + 2.4f);
                Instantiate(apple, objPosition, apple.rotation);
            }
            programState = "Select";
        }

        else if (programState.Equals("Select")) {
            //Cursor.visible = true;
            glovePosition = new Vector2(0.8f + selectAngle * -5, -6.2f);
            gloveColor = new Color(1, 1, 1, 1);
            climberColor = Color.grey;
            yesButtonColor = new Color(0, 0.85f + (Mathf.Sin(Time.time * 2f) * .15f), 0, 1);
            noButtonColor = yesButtonColor;
            instruction = "Grab Apples?";
            if (selectAngle < -0.8) {
                programState = "No";
            }
            else if (selectAngle > 0.8) {
                programState = "RelaxYes";
            }
        }

        else if (programState.Equals("No")) {
            StartCoroutine("ExecuteAfterTime", 0); // 0 = waitTime
        }

        else if (programState.Equals("RelaxYes")) {
            //Cursor.visible = false;
            gloveColor = new Color(1, 1, 1, 0);
            climberColor = Color.white;
            yesButtonColor = Color.grey;
            noButtonColor = yesButtonColor;
            instruction = "Relax Hand";
            if (climberForce > climberPositionMax+ climberMaxX) { climberPosition = climberPositionMax+ climberMaxX; }
            else { climberPosition = climberForce; }
            if (force < forceMin) {
                programState = "Yes";
                secondsStart = 0;
            }
        }

        else if (programState.Equals("Yes")) {
            secondsStart = secondsStart + Time.deltaTime;
            if (secondsStart > waitTime){
                destroyApples = true;
                climberPosition = climberPositionMin;
                noFailYes = "1"; //YesTimeout
                programState = "RelaxSelect";
                reps++;
            }
            else if (climberPosition >= challengeHeight) {
                score = score + applesToGrab;
                destroyApples = true;
                climberPosition = climberPositionMin;
                noFailYes = "2"; //YesGrabbed
                programState = "RelaxSelect";
                reps++;
            }
            else if (destroyApples == false) {
                instruction = "Squeeze Tight";
                if (climberForce > climberPositionMax + climberMaxX) { climberPosition = climberPositionMax + climberMaxX; }
                else { climberPosition = climberForce; }
            }
        }
    }

    IEnumerator ExecuteAfterTime(float time) {
        yesButtonColor = Color.grey;
        noButtonColor = yesButtonColor;
        instruction = "Relax Hand";
        secondsStart = secondsStart + Time.deltaTime;
        climberPosition = climberPositionMin;
        destroyApples = true;
        yield return new WaitForSeconds(time);
        noSelected = false;
        climberPositionLocked = true;
        instantiateApples = true;
        noFailYes = "0"; //No
        Save.SaveSimpleData();
        programState = "RelaxSelect";
        reps++;
        StopCoroutine("ExecuteAfterTime");
    }
}

