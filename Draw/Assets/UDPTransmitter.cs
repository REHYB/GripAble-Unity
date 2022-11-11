using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

public class UDPTransmitter : MonoBehaviour {
    public string IP = "146.169.191.0";//"127.0.0.1";
    //string IP = PaintGame.computerIP;
    public int TransmitPort;
    private IPEndPoint _RemoteEndPoint;
    private UdpClient _TransmitClient;
    private double[] sendData = { 0, 0, 0, 0, 0, 0 };
    double checksum = 0;


    //private void Start() {
    //}

    ///// <summary>
    ///// Initialize objects.
    ///// </summary>
    //private void Initialize() {
    //    _RemoteEndPoint = new IPEndPoint(IPAddress.Parse(IP), TransmitPort);
    //    _TransmitClient = new UdpClient();
    //}

    //bool firstTime = false;
    //private void Update() {
    //    if (PaintGame.applyUserID == true && firstTime == false) {
    //        IP = PaintGame.computerIP;
    //        Debug.Log(IP);
    //        Debug.Log(TransmitPort);
    //        firstTime = true;
    //        Initialize();
    //    }

    //    if (PaintGame.useGripable == true) {
    //        sendData[0] = -128;
    //        sendData[1] = PaintGame.force;
    //        sendData[2] = PaintGame.angleX;
    //        sendData[3] = PaintGame.angleY;
    //        sendData[4] = PaintGame.angleZ;
    //        checksum = sendData[0] * 1 + sendData[1] * 2 + sendData[2] * 3 + sendData[3] * 4 + sendData[4] * 5;
    //        sendData[5] = checksum;//PaintGame.angleZ;
    //        Debug.Log(checksum);
    //    }
    //    else {
    //        sendData[0] = -128;
    //        sendData[1] = PaintGame.climberForce;
    //        sendData[2] = 0;// PaintGame.angleX;
    //        sendData[3] = 0;// PaintGame.angleY;
    //        sendData[4] = 0;//PaintGame.angleZ;
    //        checksum = sendData[0] * 1 + sendData[1] * 2 + sendData[2] * 3 + sendData[3] * 4 + sendData[4] * 5;
    //        sendData[5] = checksum;//PaintGame.angleZ;
    //        Debug.Log(checksum);
    //    }

    //    // Send(UDPReceiver.sharedValue); // There and Back Communication
    //    Send(sendData); //Gripable Force
    //}

    ///// <summary>
    ///// Sends a double value to target port and ip.
    ///// </summary>
    ///// <param name="val"></param>
    //public void Send(double val) {
    //    try {
    //        // Convert string message to byte array.  
    //        byte[] serverMessageAsByteArray = BitConverter.GetBytes(val);//val
    //        //Debug.Log(val);
    //        _TransmitClient.Send(serverMessageAsByteArray, serverMessageAsByteArray.Length, _RemoteEndPoint);
    //        //Debug.Log(serverMessageAsByteArray);
    //    }
    //    catch (Exception err) {
    //        Debug.Log("<color=red>" + err.Message + "</color>");
    //    }
    //}

    ///// <summary>
    ///// Sends a double array to target port and ip.
    ///// </summary>
    ///// <param name="val"></param>
    //public void Send(double[] val) {
    //    try {
    //        for (int i = 0; i < val.Length; i++) { 
    //            Send(val[i]);
    //            //Debug.Log("Item: " + i + " , Value: " + val[i]);
    //        }
    //    }
    //    catch (Exception err) {
    //        Debug.Log("<color=red>" + err.Message + "</color>");
    //    }
    //}

    ///// <summary>
    ///// Deinitialize everything on quiting the application.Or you might get error in restart.
    ///// </summary>
    //private void OnApplicationQuit() {
    //    try {
    //        _TransmitClient.Close();
    //    }
    //    catch (Exception err) {
    //        Debug.Log("<color=red>" + err.Message + "</color>");
    //    }
    //}
}