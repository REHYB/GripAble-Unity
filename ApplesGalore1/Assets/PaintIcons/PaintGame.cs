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
    //COPY AND PASTE THE FOLLOWING FORCE CALIBRATION INTO Player.cs > Connect
      /* GripablePlugin.Player.SetCalibration(
       new Calibration { Min = 0, Max = 10, Type = MovementType.Grip},
       new Calibration { Min = 315, Max = 60, Type = MovementType.Roll },
       new Calibration { Min = 345, Max = 40, Type = MovementType.Pitch },
       new Calibration { Min = 310, Max = 30, Type = MovementType.Yaw },
       0.4f); */
    //Find MAC Address – open gripable app - ||| - support – terms & privacy – double tap bottom right corner

    public Transform climber;
    public Transform apple;
    public Texture2D cursor;

    public static int applesToGrab = 0;
    public static float climberPositionMin = -5.5f; // -5.5f ground
    public static float climberPositionMax = 2f; // maximum voluntary contraction scalar
    public static float climberPosition = climberPositionMin;
    public static int challengeHeight = 0;
    public static bool climberPositionLocked = true;
    public static string programState = "RelaxCal";
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
    public static float mvc = 1; // maximum voluntary contraction scalar // normalize to user
    public static Color climberColor = Color.grey;
    public static Color yesButtonColor = Color.grey;
    public static Color noButtonColor = Color.grey;
    public static float climberForce = 0;
    public static float[] mvcCal = {0, 0, 0, 0, 0};
    public static float selectAngle = 0;
    public static Vector2 glovePosition = new Vector2 (0.8f, -6.2f);
    public static Color gloveColor = new Color(1, 1, 1, 0);
    public static string macAddress = "F2:78:BD:C2:0B:9D";

    void Start() {
        GripablePlugin.Player.SetDevice(macAddress); //CA:49:AB:EF:4A:17
        GripablePlugin.Player.Connect();
    }

        //Vector2 mousePosition = new Vector2(0.0f, 0.0f);
        Vector2 objPosition = new Vector2(0.0f, 0.0f);

    void Update() {
        //    macAddress = "CA:49:AB:EF:4A:17";
        //    if (PlayerPrefs.GetString(macAddress) != "" && GripablePlugin.Player.IsInitialized() == false) {
        //        GripablePlugin.Player.SetDevice(macAddress); //CA:49:AB:EF:4A:17
        //        GripablePlugin.Player.Connect();
        //    }
        //    else if (PlayerPrefs.GetString(macAddress) !=)
        //        PlayerPrefs.Save(); }
        //        PlayerPrefs.SetString(macAddress, macAddress);
        //    }

        instruction = " update ";
        if (GripablePlugin.Player.IsInitialized()) {
            instruction = " initialized ";
            climberForce = GripablePlugin.Player.GetGripForce() * mvc * (climberPositionMax - climberPositionMin) + climberPositionMin;
            instruction = " force ";
            selectAngle = GripablePlugin.Player.GetYaw();// + GripablePlugin.Player.GetRoll() + GripablePlugin.Player.GetPitch();
            instruction = " yaw ";
        }
        //score = (int)(selectAngle*100);
        //mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); // change climberForce to mousepostion below for Gripable-less game
        Cursor.SetCursor(cursor, new Vector2(climberForce, -5.8f), CursorMode.Auto);
        if (reps>40) { //  36 game reps complete
            instruction = "Nice Work!";
        }

        else if (programState.Equals("RelaxCal")) {
            //Cursor.visible = false;
            gloveColor = new Color(1, 1, 1, 0);
            climberColor = Color.white;
            secondsStart = secondsStart + Time.deltaTime;
            instruction = "Relax Hand";
            GripablePlugin.Player.ResetWristRpy();
            if (climberForce > climberPositionMax) { climberPosition = climberPositionMax; }
            else if (climberForce < climberPositionMin) { climberPosition = climberPositionMin; }
            else { climberPosition = climberForce; }
            if (secondsStart > (waitTime) && climberForce < -4.5) {
                if (reps < 5) {
                    programState = "SqueezeCal";
                }
                else {
                    Array.Sort(mvcCal);
                    mvc = 1/mvcCal[2];
                    programState = "RelaxSelect";
                }
                secondsStart = 0;
                //save relax value
            }
        }

        else if (programState.Equals("SqueezeCal")) {
            //Cursor.visible = false;
            gloveColor = new Color(1, 1, 1, 0);
            climberColor = Color.white;
            secondsStart = secondsStart + Time.deltaTime;
            instruction = "Squeeze Tight";
            mvcCal[reps] = Mathf.Max(mvcCal[reps], GripablePlugin.Player.GetGripForce());
            if (climberForce > climberPositionMax) { climberPosition = climberPositionMax; }
            else if (climberForce < climberPositionMin) { climberPosition = climberPositionMin; }
            else { climberPosition = climberForce; }
            if (secondsStart > (waitTime) && climberForce > -4.5) {
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
            if (selectAngle > -0.2 && selectAngle < 0.2) {
                programState = "GenerateApples";
                secondsStart = 0;
            }
        }

        else if (programState.Equals("GenerateApples")) {
            //Cursor.visible = true;
            glovePosition = new Vector2(0.8f + selectAngle * -5, -6.2f);
            gloveColor = new Color(1, 1, 1, 1);
            climberColor = Color.grey;
            applesToGrab = UnityEngine.Random.Range(1, 5);
            challengeHeight = UnityEngine.Random.Range(-1, 3);
            destroyApples = false;
            for (int i = 0; i < applesToGrab; i++) {
                objPosition = new Vector2(2f * (0.5f + i - (float)applesToGrab / 2), challengeHeight);
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
            if (climberForce > climberPositionMax) { climberPosition = climberPositionMax; }
            else if (climberForce < climberPositionMin) { climberPosition = climberPositionMin; }
            else { climberPosition = climberForce; }
            if (climberForce < -4.5) {
                programState = "Yes";
                secondsStart = 0;
            }
        }

        else if (programState.Equals("Yes")) {
            secondsStart = secondsStart + Time.deltaTime;
            if ((secondsStart> waitTime) || (climberPosition >= (challengeHeight - 2))) {
                if (climberPosition >= (challengeHeight - 2)) {
                    score = score + applesToGrab;
                }
                destroyApples = true;
                climberPosition = climberPositionMin;
                programState = "RelaxSelect";
                reps++;
            }
            else if (destroyApples == false) {
                instruction = "Squeeze Tight";
                if (climberForce > climberPositionMax) { climberPosition = climberPositionMax; }
                else if (climberForce < climberPositionMin) { climberPosition = climberPositionMin; }
                else { climberPosition = climberForce; }
            }
        }
    }

    IEnumerator ExecuteAfterTime(float time) {
        instruction = "Relax Hand";
        secondsStart = secondsStart + Time.deltaTime;
        climberPosition = climberPositionMin;
        destroyApples = true;
        yield return new WaitForSeconds(time);
        noSelected = false;
        climberPositionLocked = true;
        instantiateApples = true;
        programState = "RelaxSelect";
        reps++;
        StopCoroutine("ExecuteAfterTime");
    }
}