using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

public class UDPTransmitter : MonoBehaviour {
    //public string IP = "127.0.0.1";
    string IP = PaintGame.computerIP;
    public int TransmitPort;
    private IPEndPoint _RemoteEndPoint;
    private UdpClient _TransmitClient;

    private void Start() {
        
    }

    /// <summary>
    /// Initialize objects.
    /// </summary>
    private void Initialize() {
        _RemoteEndPoint = new IPEndPoint(IPAddress.Parse(IP), TransmitPort);
        _TransmitClient = new UdpClient();
    }

    bool firstTime = false;
    private void Update() {
        if (PaintGame.applyUserID == true && firstTime == false) {
            IP = PaintGame.computerIP;
            firstTime = true;
            Initialize();
        }
        // Send(UDPReceiver.sharedValue); // There and Back Communication
        Send(PaintGame.climberForce); //Gripable Force
    }

    /// <summary>
    /// Sends a double value to target port and ip.
    /// </summary>
    /// <param name="val"></param>
    public void Send(double val) {
        try {
            // Convert string message to byte array.  
            byte[] serverMessageAsByteArray = BitConverter.GetBytes(val);//val

            _TransmitClient.Send(serverMessageAsByteArray, serverMessageAsByteArray.Length, _RemoteEndPoint);
            //Debug.Log(val);
        }
        catch (Exception err) {
            Debug.Log("<color=red>" + err.Message + "</color>");
        }
    }

    /// <summary>
    /// Sends a double array to target port and ip.
    /// </summary>
    /// <param name="val"></param>
    public void Send(double[] val) {
        try {
            for (int i = 0; i < val.Length; i++) { 
                Send(val[i]);
            }
        }
        catch (Exception err) {
            Debug.Log("<color=red>" + err.Message + "</color>");
        }
    }

    /// <summary>
    /// Deinitialize everything on quiting the application.Or you might get error in restart.
    /// </summary>
    private void OnApplicationQuit() {
        try {
            _TransmitClient.Close();
        }
        catch (Exception err) {
            Debug.Log("<color=red>" + err.Message + "</color>");
        }
    }
}