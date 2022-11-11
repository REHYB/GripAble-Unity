using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

public class UDPReceiver : MonoBehaviour {
    public static double sharedValue = 0;
    public static double target = 0;
    public static double eeg = 0;
    public int Port;
    private UdpClient _ReceiveClient;
    private Thread _ReceiveThread;

    public static string sharedValue2 = "";
    public static string[] sharedValue3;
    public static double[] sharedValue4 = { 0, 0, 0, 0 };
    public static double[] sharedValue5 = { 0, 0, 0, 0 };
    public static double[] stylus_x = { 0, 0, 0, 0 };
    public static double[] stylus_y = { 0, 0, 0, 0 };
    public static int stylus_point = -1;

    void Start() {
        Initialize();
    }

    /// <summary>
    /// Initialize objects.
    /// </summary>
    public void Initialize() {
        // Receive
        _ReceiveThread = new Thread(
            new ThreadStart(ReceiveData));
        _ReceiveThread.IsBackground = true;
        _ReceiveThread.Start();
    }

    /// <summary>
    /// Receive data with pooling.
    /// </summary>
    private void ReceiveData() {
        _ReceiveClient = new UdpClient(Port);

        while (true) {
            try {
                IPEndPoint anyIP = new IPEndPoint(IPAddress.Any, 0);
                byte[] data = _ReceiveClient.Receive(ref anyIP);

                double[] values = new double[data.Length / 8];
                Buffer.BlockCopy(data, 0, values, 0, values.Length * 8);
                sharedValue2 = System.Text.Encoding.UTF8.GetString(data);
                sharedValue3 = sharedValue2.Split(new char[] { '\t', '<', '>' });
                //Debug.Log(sharedValue2);
                stylus_point = -1;
                for (int i = 0; i < sharedValue3.Length; i++) {
                    if (sharedValue3[i] == "touchpoint") {
                        stylus_point = stylus_point + 1;
                        i = i + 4;
                        stylus_x[stylus_point] = float.Parse(sharedValue3[i-1]);
                        stylus_y[stylus_point] = float.Parse(sharedValue3[i]);
                        //Debug.Log(" Point: " + stylus_point + ", X: " + stylus_x[stylus_point] + ", Y: " + stylus_y[stylus_point]);
                    }
                }
            } 
            catch (Exception err) {
                Debug.Log("<color=red>" + err.Message + "</color>");
            }
        }
    }

    /// <summary>
    /// Deinitialize everything on quiting the application.Or you might get error in restart.
    /// </summary>
    private void OnApplicationQuit() {
        try {
            _ReceiveThread.Abort();
            _ReceiveThread = null;
            _ReceiveClient.Close();
        }
        catch (Exception err) {
            Debug.Log("<color=red>" + err.Message + "</color>");
        }
    }
}
