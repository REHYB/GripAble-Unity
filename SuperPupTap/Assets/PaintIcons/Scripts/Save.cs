using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;

//starting code: https://answers.unity.com/questions/1300019/how-do-you-save-write-and-load-from-a-file.html
//better starting code for ease of use: https://support.unity3d.com/hc/en-us/articles/115000341143-How-do-I-read-and-write-data-from-a-text-file-
public class Save : MonoBehaviour {
    //public static string username = "P";
    private void Start() {
    }

    bool newFileCreated = false;
    void Update() {
        if (PaintGame.applyUserID == true && newFileCreated == false) {
            SaveFileHeader();
            newFileCreated = true;
        }
        if (PaintGame.applyUserID == true && newFileCreated == true) {
            //    SaveSimpleData(); //only write when asked
            SaveRawData();
        }
    }

    public static StreamWriter writer;
    public static string destination = "";
    public static string destinationRaw = "";
    string simple = "_SimpleData";
    string raw = "_RawData";
    string txtEnding = ".txt";
    public static int increment = 1;
    public void SaveFileHeader() {
        destination = Application.persistentDataPath + "/"
            + PaintGame.userID + "_" + increment + simple + txtEnding;
        while (File.Exists(destination)) {
            increment++;
            destination = Application.persistentDataPath + "/"
                + PaintGame.userID + "_" + increment + simple + txtEnding;
        }
        writer = new StreamWriter(destination, true);
        writer.WriteLine("TimeElapsed," + "TrialNumber," + "Reward," + "Challenge,"
            + "Miss(0)/Hit(1)," + "TotalScore," + "ProgramState," + "RiseSpeed," + "DecoyFlag,"
            + "UserID," + PaintGame.userID + "," + "Date(MDY)," + DateTime.Now);
        writer.Close();

        destinationRaw = Application.persistentDataPath + "/"
            + PaintGame.userID + "_" + increment + raw + txtEnding;
        writer = new StreamWriter(destinationRaw, true);
        writer.WriteLine("Time.time," + "TotalScore," + "climberPosition," + "climberMin," + PaintGame.climberPositionMin + "," + "climberMax," + PaintGame.climberPositionMax + "," + "UserID," + PaintGame.userID + "," + "Date(MDY)," + DateTime.Now);
        writer.Close();
    }


    public static void SaveSimpleData(int trialNum, int rewardVal, int challengeVal, int hit, int decoyBoneFlag) {
        writer = new StreamWriter(destination, true);
        writer.WriteLine(Time.time + "," + trialNum + "," + rewardVal + "," + challengeVal + "," + hit + "," + PaintGame.bonesCaught + "," + PaintGame.programStage + "," + PaintGame.challengeTap, "," + decoyBoneFlag);
        writer.Close();
    } 

    public void SaveRawData() {
        writer = new StreamWriter(destinationRaw, true);
        writer.WriteLine(Time.time + "," + PaintGame.bonesCaught + "," + PaintGame.climberPosition );
        writer.Close();
    }
}

