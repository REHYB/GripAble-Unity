using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;

//starting code: https://answers.unity.com/questions/1300019/how-do-you-save-write-and-load-from-a-file.html
//better starting code for ease of use: https://support.unity3d.com/hc/en-us/articles/115000341143-How-do-I-read-and-write-data-from-a-text-file-
public class Save : MonoBehaviour {

    bool createNewFile = true;
    bool writerClosed = false;
    bool firstSave = true;
    float TimeBeforeApply = 0;
    int buffer = 0;
    float gameTime = 0;

    //public static string username = "P";
    private void Start() {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 200;
        SaveFileHeader();


    }

    void Update() {
        gameTime = DateTime.Now.Millisecond;
        SaveRawData();
    }

    public static StreamWriter writer;
    public static string destination = "";
    public static string destinationRaw = "";
    string simple = "_SimpleData";
    string raw = "_RawData";
    string txtEnding = ".txt";
    public static int increment = 2;

    public void SaveFileHeader() {
        ////Raw Data Recording
        destinationRaw = "hello.txt";
        writer = new StreamWriter(destinationRaw, true);
        writer.WriteLine("TimeElapsed");
        writer.Close();
    }

    public void SaveRawData() {
        writer = new StreamWriter(destinationRaw, true);
        writer.WriteLine(gameTime);
        writer.Close();
    }
}


