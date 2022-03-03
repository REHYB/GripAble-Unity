using System.Collections;
using System.Collections.Generic;
using Gripable;
using Protos;
using UnityEngine;

public class ApplicationPauseHandler : MonoBehaviour
{
    private ConnectionState _connectionState;
    private StreamingState _streamingState;

    void Awake()
    {
        GripablePlugin.Player.OnStreamingStateChanged += OnStreamingStateChanged;
        GripablePlugin.Player.OnConnectionStateChanged += OnConnectionStateChanged;
    }

    void OnApplicationPause(bool pause)
    {
        if (pause && _streamingState == StreamingState.On)
            GripablePlugin.Player.GetDevice().StopStreamingData();

        else if (!pause && _connectionState == ConnectionState.Connected)
            GripablePlugin.Player.GetDevice().StartStreamingData();
    }

    private void OnConnectionStateChanged(ConnectionState connectionState)
    {
        _connectionState = connectionState;
    }

    private void OnStreamingStateChanged(StreamingState streamingState)
    {
        _streamingState = streamingState;
    }
}
