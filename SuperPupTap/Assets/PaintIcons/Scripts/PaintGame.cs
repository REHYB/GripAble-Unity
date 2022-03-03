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
    public static float climberPositionMin = -4.1f; // -5.5f ground
    public static float climberPositionMax = 5.0f; // maximum voluntary contraction scalar
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
    public static float appleHeight = -4.1f;
    public static Color appleColor = Color.white;
    public static int stimCounter = 2;
    public static int forceCounter = 1;
    public static Color idButtonColor = Color.white;
    public static bool idButtoninteractable = true;
    public static float holdTime = 6f;
    public static float angleYaw = 0f;
    public static float anglePitch = 0f;
    public static float angleRoll = 0f;
    public static float climberForceScale = 80f;
    public static bool repStarted = false;
    public static bool restStarted = false;
    public static float repStart = 0f;
    public static float repTimer = 30f;
    public static float appleHeightPrevious = -4.1f;
    public static int bonesCaught = 0;
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
    public static float challengeTap = 1.0f;  // how fast puppy rises on taps
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
    //public static float[] appleHeightVector = new float[] { m2+m1*0/6, m2+m1*1/6, m2+m1*2/6, m2+m1*3/6, m2+m1*4/6, m2+m1*5/6, m2+m1*6/6 };
    public static float[] appleHeightVector = new float[] { m2+m1*2/6, m2+m1*2/6, m2+m1*2/6, m2+m1*4/6, m2+m1*4/6, m2+m1*4/6, m2+m1*6/6, m2+m1*6/6, m2+m1*6/6 };
    public static int[] appleRewardVector = new int[] { 0, 1, 2, 0, 1, 2, 0, 1, 2 };
    public static int reward = 1;
    public static int challenge = 1;
    public static bool success = false;
    public static float stage1 = 200f; //bronze
    public static float stage2 = 500f;//silver
    public static float stage3 = 2000f;//gold
    public static int programStage = 0;
    public static int rewardBonePrev;
    public static int challengeBonePrev;
    public static int targetHitPrev;
    static bool challengeChange = false;
    //public static int[] rewardBoneArray = new int[10000]; //1-6
    //public static int[] challengeBoneArray = new int[10000]; //1-6
    public static int[] targetHitArray = new int[10000];
    public static int repCount = 0;
    public static int rewardLevels = 3;
    public static int challengeLevels = 3;
    public static int rounds = 50;
    public static int options = rewardLevels * challengeLevels;
    public static int shuffleLength = options * rounds; 
    public static int[] challengeBoneArray = new int[shuffleLength];
    public static int[] rewardBoneArray = new int[shuffleLength];
    public static int[] boneArray = new int[shuffleLength];
    public static int tempGO;
    public static bool decoyBone = false;
    public static bool tapEnabled = true;

    void Start() {
        //rewardBoneArray[0] = 4; rewardBoneArray[1] = 2; rewardBoneArray[2] = 6;
        //challengeBoneArray[0] = 4; challengeBoneArray[1] = 2; challengeBoneArray[2] = 6;
        Shuffle();
    }

    public void Shuffle() {
        for (int i = 0; i < rounds; i++) { 
            for (int j = 0; j < options; j++) {
                boneArray[i * options + j] = j;
            }
            //boneArray[0-8] = { 0 1 2 3 4 5 6 7 8 }
            //boneArray[9-17] = { 0 1 2 3 4 5 6 7 8 }
            for (int k = 0; k < options; k++) {
                //int rnd = 0-8; //int rnd = 9-17
                int rnd = UnityEngine.Random.Range(i * options + k, i * options + options);
                //rnd = 4, tempGo = 4 //rnd = 12, tempGo = 3
                tempGO = boneArray[rnd];
                //boneArray[4] = 0, boneArray[12] = 0
                boneArray[rnd] = boneArray[i * options + k];
                //boneArray[0] = 4, boneArray[9] = 3
                boneArray[i * options + k] = tempGO;
                //if (i == 0) { Debug.Log(boneArray[i * options + k]); }
                //Debug.Log(boneArray[i * options + k]);
            }
        }

        //for (int i = 0; i < challengeBoneArray.Length; i++) {
        //    int rnd = UnityEngine.Random.Range(i, challengeBoneArray.Length);
        //    tempGO = challengeBoneArray[rnd];
        //    challengeBoneArray[rnd] = challengeBoneArray[i];
        //    challengeBoneArray[i] = tempGO;
        //}
        //for (int i = 0; i < rewardBoneArray.Length; i++) {
        //    int rnd = UnityEngine.Random.Range(i, rewardBoneArray.Length);
        //    tempGO = rewardBoneArray[rnd];
        //    rewardBoneArray[rnd] = rewardBoneArray[i];
        //    rewardBoneArray[i] = tempGO;
        //}
    }

    void Update() {
        // Get Tap Data and Set Puppy Height
        if (Time.time > timePrev + timeUpdate) {
            climberPosition = climberPosition - fallSpeed;
            if (tapDetected == true) {
                climberPosition = climberPosition + challengeTap;
                tapDetected = false;
            }
            if (climberPosition > climberPositionMax) {
                climberPosition = climberPositionMax;
            }
            else if (climberPosition < climberPositionMin) {
                climberPosition = climberPositionMin;
            }
            timePrev = Time.time;
        }

        // Select bone height
        if (applyUserID == true && Time.time > (previousRespawn + respawnGap)) {
            previousRespawn = Time.time;
            //training
            if (trials < 3) {
                reward = appleRewardVector[trials]; // 1 to 6 bones
                appleHeight = appleHeightVector[trials*3];
                Instantiate(apple, new Vector3(14.5f, appleHeight, 0), Quaternion.identity);
                programStage = 1;
            }

            //calibration
            else if (trials >= 3 && trials < 19) {
                if (trials % 2 == 0) {
                    tapEnabled = true;
                    reward = appleRewardVector[0]; // 1 to 6 bones
                    decoyBone = false;
                    Instantiate(apple, new Vector3(14.5f, appleHeightVector[8], 0), Quaternion.identity);
                    programStage = 2;
                }
                else {
                    // disable button
                    tapEnabled = false;
                    //decoyBone = true;
                    //Instantiate(apple, new Vector3(14.5f, appleHeightVector[0], 0), Quaternion.identity);
                }
            }

            //game
            else {
                programStage = 4;
                if (trials % 2 == 0) {
                    tapEnabled = true;
                    //staircase algorithm is evil for data analysis
                    appleHeight = appleHeightVector[boneArray[trials-20]+1]; 
                    reward = rewardBoneArray[trials-20]+1;  
                    challengeBoneArray[repCount] = challenge;
                    decoyBone = false;
                    Instantiate(apple, new Vector3(14.5f, appleHeight, 0), Quaternion.identity);
                    //Instantiate(apple, new Vector3(14.5f, appleHeightVector[0], 0), Quaternion.identity);
                    repCount++;
                }
                else {
                    tapEnabled = false;
                    //decoyBone = true;
                    //Instantiate(apple, new Vector3(14.5f, appleHeightVector[0], 0), Quaternion.identity);
                }
            }
            trials++;
        }
    }
}