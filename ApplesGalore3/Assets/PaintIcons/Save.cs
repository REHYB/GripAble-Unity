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
            + PaintGame.userID + "_" + increment + simple  + txtEnding;
        while (File.Exists(destination)) {
            increment++;
            destination = Application.persistentDataPath + "/"
                + PaintGame.userID + "_" + increment + simple + txtEnding; }
        writer = new StreamWriter(destination, true);
        writer.WriteLine("TimeElapsed," + "Reward," + "Challenge,"
            + "No(0)/YesTimeout(1)/YesGrabbed(2)," + "MVC," + "ApplesTotal,"
            + "NumReps," + "UserID," + PaintGame.userID + "," + "Date(MDY)," + DateTime.Now);
        writer.Close();

        destinationRaw = Application.persistentDataPath + "/"
            + PaintGame.userID + "_" + increment + raw + txtEnding;
        writer = new StreamWriter(destinationRaw, true);
        writer.WriteLine("Time.time," + "Reward_1_6_1_15," + "Challenge_6_11,"
            + "No(0)/YesTimeout(1)/YesGrabbed(2)," + "MVC," + "ApplesTotal,"
            + "NumReps," + "ProgramState," + "Force," + "ClimberPosition"
            + "ClimberPosMin," + "ClimberPosMax," + "SelectAngle,"
            + "SecondsStart," + "MacAddress," + "mvcCal[0]," + "mvcCal[1],"
            + "mvcCal[2]," + "mvcCal[3]," + "mvcCal[4]," + "UserID," + PaintGame.userID + "," + "Date(MDY)," + DateTime.Now);
        writer.Close();
    }

    public static void SaveSimpleData() {
        writer = new StreamWriter(destination, true);
        writer.WriteLine(Time.time + "," + PaintGame.rewardApples + "," + PaintGame.challengeForce
            + "," + PaintGame.noYesSuccess + "," + PaintGame.mvc + "," + PaintGame.score + ","
            + PaintGame.reps);
        writer.Close();
    }

    public void SaveRawData() {
        writer = new StreamWriter(destinationRaw, true);
        writer.WriteLine(Time.time + "," + PaintGame.rewardApples + "," + PaintGame.challengeForce
            + "," + PaintGame.noYesSuccess + "," + PaintGame.mvc + "," + PaintGame.score + ","
            + PaintGame.reps + "," + PaintGame.programState + "," 
            + PaintGame.force + "," + PaintGame.climberPosition + "," + PaintGame.climberPosMin + "," + PaintGame.climberPosMax + ","
            + PaintGame.selectAngle + "," + PaintGame.secondsStart + "," + PaintGame.macAddress + "," + PaintGame.mvcCal[0] + "," + PaintGame.mvcCal[1] + "," + PaintGame.mvcCal[2] + "," + PaintGame.mvcCal[3] + "," + PaintGame.mvcCal[4]);
        writer.Close();
    }
}

