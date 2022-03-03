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
    //COPY AND PASTE THE FOLLOWING FORCE CALIBRATION INTO Gripable BLE Unity > Src > Runtime> Player.cs >
    //public bool Connect() {
    //    if (IsInitialized()) {
    //        GripablePlugin.Player.SetCalibration(
    //        new Calibration { Min = 0, Max = 70, Type = MovementType.Grip },
    //        new Calibration { Min = 315, Max = 60, Type = MovementType.Roll },
    //        new Calibration { Min = 345, Max = 40, Type = MovementType.Pitch },
    //        new Calibration { Min = 310, Max = 30, Type = MovementType.Yaw }, 0.4f);
    //        return _deviceGateway.Connect();
    //Find MAC Address – open gripable app - ||| - support – terms & privacy – double tap bottom right corner

    public Transform climber;
    public Transform apple;
    public Texture2D cursor;

    public static int applesToGrab = 0;
    public static float climberPosMin = -6.76f; // -5.5f ground
    public static float climberPosMax = -2.35f; // maximum voluntary contraction scalar
    public static float climberMaxX = 2; // maximum voluntary contraction scalar
    public static float climberPosition = climberPosMin;
    public static float challengeHeight = 0;
    public static bool climberPositionLocked = true;
    public static int programState = 0; //0 = settings; 1 = calibration relax; 2 = calibration grip; 3 = grip task; 4 = complete
    public static bool destroyApples = false;
    public static bool yesSelected = false;
    public static bool noSelected = false;
    public static bool pushSelected = false;
    public static int score = 0;
    public static bool instantiateApples = true;
    public static float waitTime = 2.5f; //!! CHANGE
    public static float waitTimeGrip = 5f; //!! CHANGE
    public static float secondsStart = 0;
    public static string instruction = "";
    public static int reps = 0;
    public static float mvc = 0.3f; // maximum voluntary contraction scalar // normalize to user
    public static Color climberColor = Color.grey;
    public static Color yesButtonColor = Color.grey;
    public static Color noButtonColor = Color.grey;
    public static float climberForce = 0;
    public static float selectAngle = 0;
    public static float selectAngleMax = 0.2f;
    public static Vector2 glovePosition = new Vector2(0.8f, -6.2f);
    public static Color gloveColor = new Color(1, 1, 1, 0);
    public static string macAddress = "C7:37:68:67:F6:7E";//AY: "CF:C8:03:D7:2E:FC";//"C7:37:68:67:F6:7E"; //"F9:05:C0:6D:B2:C2";// Aaron1-B-I-G8: CF:C8:03:D7:2E:FC Aaron-B-V: CA:49:AB:EF:4A:17 Aaron-W-I: FF:30:2F:47:20:6E Leeza1: D5:B2:37:4A:C8:5E Leeza2-ThisOne: F9:05:C0:6D:B2:C2, Leeza-4S: D5:B2:37:4A:C8:5E, Leeza-1AY: C0:50:3E:B8:FC:61, Leeza-U3: E4:ED:DB:D1:B6:56, Leeza-G5: C7:37:68:67:F6:7E

    public static int maxReps = 45; //36
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
    bool useGripable = true;
    bool init = false;
    public static int challengeForce = 0;
    public static int rewardApples = 0;
    public static int noYesSuccess = 0; // 0 = No selected, 1 = Yes selected (Timeout), 2 = Yes selected (Grabbed)
    static bool challengeChange = false;
    public static int[] noYesSuccessArray = new int[maxCalibReps+maxReps];
    public static int[] challengeForceArray = new int[6 * 1000];// maxCalibReps + maxReps];
    public static int[] rewardApplesArray = new int[6 * 1000];// maxCalibReps + maxReps];
    public static int tempGO;

    void Start() {
        Shuffle();
        if (useGripable == true) {
            GripablePlugin.Player.SetDevice(macAddress); //CA:49:AB:EF:4A:17
            GripablePlugin.Player.Connect();
            climberForce = GripablePlugin.Player.GetGripForce();
            selectAngle = GripablePlugin.Player.GetRoll();// + GripablePlugin.Player.GetRoll() + GripablePlugin.Player.GetPitch();
            instruction = " GripAble Ready ";
        }
    }

    public void Shuffle() {
        for (int i = 0; i < 1000; i++) {
            for (int j = 0; j < 6; j++) {
                challengeForceArray[i * 6 + j] = j;
                rewardApplesArray[i * 6 + j] = j;
            }
        }
        for (int i = 0; i < challengeForceArray.Length; i++) {
            int rnd = UnityEngine.Random.Range(i, challengeForceArray.Length);
            tempGO = challengeForceArray[rnd];
            challengeForceArray[rnd] = challengeForceArray[i];
            challengeForceArray[i] = tempGO;
        }
        for (int i = 0; i < rewardApplesArray.Length; i++) {
            int rnd = UnityEngine.Random.Range(i, rewardApplesArray.Length);
            tempGO = rewardApplesArray[rnd];
            rewardApplesArray[rnd] = rewardApplesArray[i];
            rewardApplesArray[i] = tempGO;
        }
    }

    Vector2 mousePosition = new Vector2(0.0f, 0.0f);
    Vector2 objPosition = new Vector2(0.0f, 0.0f);
    void Update() {
        if (useGripable == true) {
            if (GripablePlugin.Player.IsInitialized()) {
                climberForce = GripablePlugin.Player.GetGripForce()/mvc;
                climberPosition = (climberForce - 0.5f) * (-climberPosMin + climberPosMax) * 2 + climberPosMin;
                selectAngle = 2f*GripablePlugin.Player.GetRoll()+3.75f;// + GripablePlugin.Player.GetRoll() + GripablePlugin.Player.GetPitch();
                //Cursor.SetCursor(cursor, new Vector2(selectAngle, -5.8f), CursorMode.Auto);
                glovePosition = new Vector2(selectAngle - 3.74f, -8.53f);
                if (climberPosition < climberPosMin) { climberPosition = climberPosMin; }
            }
        }
        else {
            climberForce = (Input.mousePosition.y+15) / (mvc*(349f+15f));
            selectAngle = (Input.mousePosition.x) / 180;
            climberPosition = (climberForce-0.5f) * (-climberPosMin + climberPosMax) * 2 + climberPosMin;
            Cursor.SetCursor(cursor, new Vector2(climberForce, -5.8f), CursorMode.Auto);
            if (climberPosition < climberPosMin) { climberPosition = climberPosMin; }
        }

        //score = (int)(selectAngle*100);
        //mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); // change climberForce to mousepostion below for Gripable-less game
        if (reps >= (maxReps + maxCalibReps)) { //  36 game reps complete
            instruction = "NICE WORK!";
        }

        else if (programState == 0) {
            Cursor.visible = true;
            yesButtonColor = Color.gray;
            noButtonColor = Color.gray;
            climberColor = new Color (1,1,0,0);
        }

        //Calibration
        else if (programState == 1) {
            if (init == false) {
                climberColor = new Color(1, 1, 0, 1);
                Cursor.visible = false;
                yesButtonColor = Color.gray;
                noButtonColor = Color.gray;
                secondsStart = Time.time;
                instruction = "RELAX HAND";
                init = true;
            }

            if (reps == maxCalibReps) {
                init = false;
                Array.Sort(mvcCal);
                mvc = mvcCal[(int)Mathf.Floor(maxCalibReps / 2f)];
                if (mvc < 0.01) {
                    mvc = 0.01f;
                }
                programState = 3;
            }

            else if (Time.time > secondsStart + waitTime) {
                init = false;
                programState = 2;
            }
        }

        else if (programState == 2) {
            if (init == false) {
                Cursor.visible = false;
                yesButtonColor = Color.gray;
                noButtonColor = Color.gray;
                secondsStart = Time.time;
                instruction = "GRIP HARD";
                init = true;
            }
            //Cursor.visible = false;

            else if (Time.time > secondsStart + waitTimeGrip) {
                init = false;
                mvc = mvc * (maxCalibReps - 1) / maxCalibReps + mvcCal[reps] / maxCalibReps; 
                reps++;
                programState = 1;
            }

            else {
                if (climberForce > mvcCal[reps]) {
                    mvcCal[reps] = climberForce;
                }
            }
            //GripablePlugin.Player.ResetWristRpy();
        }

        //Game phase
        else if (programState == 3) {
            if (init == false) {
                Cursor.visible = false;
                yesButtonColor = Color.gray;
                noButtonColor = Color.gray;
                secondsStart = Time.time;
                challengeForce = 0;
                rewardApples = 0;
                instruction = "RELAX HAND";
                init = true;
            }

            else if (Time.time > secondsStart + waitTime) {
                init = false;
                if (useGripable == true) {
                    GripablePlugin.Player.ResetWristRpy();
                }
                programState = 4;
            }
        }

        else if (programState == 4) {
            if (init == false) {
                Cursor.visible = true;
                if (useGripable == true) {
                    gloveColor = new Color(1, 1, 1, 1);
                }
                yesButtonColor = Color.gray;
                noButtonColor = Color.gray;
                secondsStart = Time.time;
                instruction = "-> CENTRE GLOVE <-";
                init = true;
            }

            else if (selectAngle > 3.5 && selectAngle < 4) {
                init = false;
                programState = 5;
            }
        }

        else if (programState == 5) {
            if (init == false) {
                Cursor.visible = true;
                yesButtonColor = Color.green;
                noButtonColor = Color.green;
                secondsStart = Time.time;
                //set trunk height (6 to 11) and set apple number (1 to 6)
                //noYesSuccessArray[reps-1] = noYesSuccess;
                //if (reps == maxCalibReps) { rewardApples = 1; challengeForce = 6; }
                //else if (reps == (maxCalibReps+1)) { rewardApples = 3; challengeForce = 8; }
                //else if (reps == (maxCalibReps+2)) { rewardApples = 5; challengeForce = 10; }
                //else if (noYesSuccessArray[reps-3] == 0 && challengeChange == false ) {
                //    challengeForce = challengeForceArray[reps-3] - 1;
                //    rewardApples = rewardApplesArray[reps - 3];
                //    challengeChange = true;
                //    Debug.Log("1: " + noYesSuccessArray[reps - 3] + ", c: " + challengeForce);
                //}
                //else if (noYesSuccessArray[reps-3] == 0 && challengeChange == true) {
                //    rewardApples = rewardApplesArray[reps - 3] + 1;
                //    challengeForce = challengeForceArray[reps - 3];
                //    challengeChange = false;
                //    Debug.Log("2: " + noYesSuccessArray[reps - 3] + ", a: " + rewardApples);
                //}
                //else if (challengeChange == false) {
                //    challengeForce = challengeForceArray[reps - 3] + 1;
                //    rewardApples = rewardApplesArray[reps - 3];
                //    challengeChange = true;
                //    Debug.Log("3: " + noYesSuccessArray[reps - 3] + ", c: " + challengeForce);
                //}
                //else if (challengeChange == true) {
                //    rewardApples = rewardApplesArray[reps - 3] - 1;
                //    challengeForce = challengeForceArray[reps - 3];
                //    challengeChange = false;
                //    Debug.Log("4: " + noYesSuccessArray[reps - 3] + ", a: " + rewardApples);
                //}

                //if (challengeForce < 6) { challengeForce = 6; }
                //else if (challengeForce > 11) { challengeForce = 11; }

                //if (rewardApples < 1) { rewardApples = 1; }
                //else if (rewardApples > 6) { rewardApples = 6; }
                //challengeForceArray[reps] = challengeForce;
                //rewardApplesArray[reps] = rewardApples;

                challengeForce = challengeForceArray[reps]+6;
                rewardApples = rewardApplesArray[reps]+1;
                instruction = "<- YES OR NO? ->";
                init = true;
            }

            else if (selectAngle < 2.05) {
                init = false;
                if (useGripable == true) {
                    gloveColor = new Color(1, 1, 1, 0);
                }
                programState = 6;
            }

            else if (selectAngle > 5.45) {
                init = false;
                if (useGripable == true) {
                    gloveColor = new Color(1, 1, 1, 0);
                }
                noYesSuccess = 0;
                reps++;
                Save.SaveSimpleData();
                programState = 3;
            }
        }

        else if (programState == 6) {
            if (init == false) {
                Cursor.visible = true;
                yesButtonColor = Color.gray;
                noButtonColor = Color.gray;
                secondsStart = Time.time;
                instruction = "GRIP HARD";
                init = true;
            }

            if (climberForce > (challengeForce / 10f)) {
                if (rewardApples == 1) { score = score + 1; }
                else if (rewardApples == 2) { score = score + 3; }
                else if (rewardApples == 3) { score = score + 6; }
                else if (rewardApples == 4) { score = score + 9; }
                else if (rewardApples == 5) { score = score + 12; }
                else if (rewardApples == 6) { score = score + 15; }

                init = false;
                noYesSuccess = 2;
                reps++;
                Save.SaveSimpleData();
                programState = 3;
            }

            else if (Time.time > secondsStart + waitTimeGrip) {
                init = false;
                noYesSuccess = 1;
                score = score - 1;
                reps++;
                Save.SaveSimpleData();
                programState = 3;
            }
        }
    }
}
