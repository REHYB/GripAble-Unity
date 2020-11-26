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

    bool createNewFile = true;
    void Update() {
        if (PaintGame.applyUserID == true && createNewFile == true) {
            SaveFileHeader();
            createNewFile = false;
        }
        if (PaintGame.applyUserID == true && createNewFile == false) {
            SaveSimpleData();
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
            + "No(0)/YesTimeout(1)/YesGrabbed(2)," + "ApplesTotal,"
            + "NumReps," + "Date(MDY)," + DateTime.Now);
        writer.Close();

        destinationRaw = Application.persistentDataPath + "/"
            + PaintGame.userID + "_" + increment + raw + txtEnding;
        writer = new StreamWriter(destinationRaw, true);
        writer.WriteLine("TimeElapsed," + "Reward," + "Challenge,"
            + "No(0)/YesTimeout(1)/YesGrabbed(2)," + "ApplesTotal,"
            + "NumReps," + "maxReps," + "maxCalibReps,"
            + "programState,"
            + "climberForce," + "climberPosition," + "climberPositionMin," + "climberPositionMax,"
            + "secondsStart(timeSinceYesSelected)," + "waitTime,"
            + "selectAngle(yaw)," + "selectAngleMax,"
            + "macAddress," + "mvc,"
            + "mvcCal[0]," + "mvcCal[1]," + "mvcCal[2]," + "mvcCal[3]," + "mvcCal[4],"
            + "instructions," + "Date(MDY)," + DateTime.Now);
        writer.Close();
    }

    public static void SaveSimpleData() {
        writer = new StreamWriter(destination, true);
        writer.WriteLine(Time.time + "," + PaintGame.applesToGrab + "," + (PaintGame.challengeHeight+2)
            + "," + PaintGame.noFailYes + "," + PaintGame.score + ","
            + PaintGame.reps);
        writer.Close();
    }

    public void SaveRawData() {
        writer = new StreamWriter(destinationRaw, true);
        writer.WriteLine(Time.time + "," + PaintGame.applesToGrab + "," + (PaintGame.challengeHeight + 2) + ","
            + PaintGame.noFailYes + "," + PaintGame.score + ","
            + PaintGame.reps + "," + PaintGame.maxReps + "," + PaintGame.maxCalibReps + ","
            + PaintGame.programState + "," 
            + PaintGame.climberForce + "," + PaintGame.climberPosition + "," + PaintGame.climberPositionMin + "," + PaintGame.climberPositionMax + ","
            + PaintGame.secondsStart + "," + PaintGame.waitTime + ","
            + PaintGame.selectAngle + "," + PaintGame.selectAngleMax + ","
            + PaintGame.macAddress + "," + PaintGame.mvc + ","
            + PaintGame.mvcCal[0] + "," + PaintGame.mvcCal[1] + "," + PaintGame.mvcCal[2] + "," + PaintGame.mvcCal[3] + "," + PaintGame.mvcCal[4] + ","
            + PaintGame.instruction);
        writer.Close();
    }
}

