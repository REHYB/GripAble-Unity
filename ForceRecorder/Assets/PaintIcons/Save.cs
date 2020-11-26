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
        writer.WriteLine("Time" + "," + "Force" + "," + "repCounter" + "," + "counter" + "," + "forceCounter" + "," + "stimCounter" + "," + "angleYaw" + "," + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss.fff"));
        writer.Close();
    }

    public void SaveRawData() {
        writer = new StreamWriter(destination, true);
        writer.WriteLine(DateTime.Now.Hour*3600+DateTime.Now.Minute*60+DateTime.Now.Second + "." + DateTime.Now.Millisecond + "," + PaintGame.force + "," + PaintGame.repCounter + "," + PaintGame.counter + "," + PaintGame.forceCounter + "," + PaintGame.stimCounter + "," + PaintGame.angleYaw);
        writer.Close();
    }
}