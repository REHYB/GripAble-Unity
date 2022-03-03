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
    float TimeBeforeApply = 0;
    void Update() {
        if (PaintGame.applyUserID == true && createNewFile == true) {
            SaveFileHeader();
            createNewFile = false;
        }
        if (PaintGame.applyUserID == true && createNewFile == false) {
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
        //Simple Data Recording
        destination = Application.persistentDataPath + "/"
            + PaintGame.userID + "_" + increment + raw + txtEnding;
        while (File.Exists(destination)) {
            increment++;
            destination = Application.persistentDataPath + "/"
                + PaintGame.userID + "_" + increment + raw + txtEnding;
        }

        ////Raw Data Recording
        writer = new StreamWriter(destination, true);
        writer.WriteLine("TimeElapsed" + "," + "GameLevel" + "," + "Order" + "," + "Set" + "," + "Reps" + "," + "FESmA" + "," + "Force" + "," + "DogHeight" + "," + "BoneHeight" + "," +  "BonesCaught" + "," + "AngleYaw" + "," + "AnglePitch" + "," + "AngleRoll" + "," + "fesCalib0_1" + "," + "fesCalib0_2" + "," + "fesCalib0_3" + "," + "fesCalib0_T" + "," + "fesCalib1_1" + "," + "fesCalib1_2" + "," + "fesCalib1_3" + "," + "fesCalib1_T" + "," + "fesCalib2_1" + "," + "fesCalib2_2" + "," + "fesCalib2_3" + "," + "fesCalib2_T" + "," + "fesCalib3_1" + "," + "fesCalib3_2" + "," + "fesCalib3_3" + "," + "fesCalib3_T" + "," + "fesCalib4_1" + "," + "fesCalib4_2" + "," + "fesCalib4_3" + "," + "fesCalib4_T" + "scaleChallenge" + "," + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss.fff"));
        writer.Close();
    }

    public void SaveRawData() {
        writer = new StreamWriter(destination, true);
        writer.WriteLine(Time.time + "," + PaintGame.gameLevel + "," + PaintGame.order[PaintGame.set] + "," + PaintGame.set + "," + PaintGame.reps + "," + PaintGame.FESmA + "," + PaintGame.force + "," + PaintGame.climberPosition + "," + PaintGame.targetPosition + "," + PaintGame.bonesCaught + "," + PaintGame.yaw + "," + PaintGame.pitch + "," + PaintGame.roll + "," + PaintGame.fesCalib[0] + "," + PaintGame.fesCalib[5] + "," + PaintGame.fesCalib[10] + "," + PaintGame.fesCalib2[0] + "," + PaintGame.fesCalib[1] + "," + PaintGame.fesCalib[6] + "," + PaintGame.fesCalib[11] + "," + PaintGame.fesCalib2[1] + "," + PaintGame.fesCalib[2] + "," + PaintGame.fesCalib[7] + "," + PaintGame.fesCalib[12] + "," + PaintGame.fesCalib2[2] + "," + PaintGame.fesCalib[3] + "," + PaintGame.fesCalib[8] + "," + PaintGame.fesCalib[13] + "," + PaintGame.fesCalib2[3] + "," + PaintGame.fesCalib[4] + "," + PaintGame.fesCalib[9] + "," + PaintGame.fesCalib[14] + "," + PaintGame.fesCalib2[4] + "," + PaintGame.scaleChallenge);
        //                   "Time" + "," +                                                                                         "GameLevel" + ","                  + "Order" + ","                    + "Set" + ","           + "Reps" + ","     + "FESmA"       + ","    + "Force"      + ","     + "DogHeight" + ","             + "BoneHeight" + ","       +       "BonesCaught" + ","       + "AngleYaw" + "," + "AnglePitch" + "," + "AngleRoll" + "," + "fesCalib0_1" + "," + "fesCalib0_2" + "," + "fesCalib0_3" + "," + "fesCalib=_T" +  "fesCalib1_1" + "," + "fesCalib1_2" + "," + "fesCalib1_3" + "," + "fesCalib1_T" + "," + "fesCalib2_1" + "," + "fesCalib2_2" + "," + "fesCalib2_3" + "," + "fesCalib2_T" + "," + "," + "fesCalib3_1" + "," + "fesCalib3_2" + "," + "fesCalib3_3" + "," + "fesCalib3_T" + "," + "fesCalib4_1" + "," + "fesCalib4_2" + "," + "fesCalib4_3" + "," + "fesCalib4_T" + "," + "fesCalib5_1" + "," + "fesCalib5_2" + "," + "fesCalib5_3" + "," + "fesCalib5_T" + "scaleChallenge" + "," + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss.fff"));
        writer.Close();
    }
}