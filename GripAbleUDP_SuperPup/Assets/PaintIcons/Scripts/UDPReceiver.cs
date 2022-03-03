using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

public class UDPReceiver : MonoBehaviour {
    public static double sharedValue = 0;
    public static double sharedValue2 = 0;
    public int Port;
    private UdpClient _ReceiveClient;
    private Thread _ReceiveThread;


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
                sharedValue = values[0];
                sharedValue2 = values[1];
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
