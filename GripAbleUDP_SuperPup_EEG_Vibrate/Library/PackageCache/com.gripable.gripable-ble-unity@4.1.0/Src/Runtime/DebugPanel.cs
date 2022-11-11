using System.Collections;
using System.Collections.Generic;
using Protos;
using Gripable;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DebugPanel : MonoBehaviour
{
    private readonly Color32 _green = new Color32(0, 255, 0, 90);
    private readonly Color32 _red = new Color32(255, 0, 0, 90);
    private const int _MAC_ADDRESS_CHARACTER_LIMIT = 17;

    private bool _updateValues;

    [SerializeField] private Button _closeButton;

    [SerializeField] private RectTransform _squeezeEventCounter;
    [SerializeField] private RectTransform _releaseEventCounter;
    [SerializeField] private RectTransform _flexionEventCounter;
    [SerializeField] private RectTransform _extensionEventCounter;
    [SerializeField] private RectTransform _pronationEventCounter;
    [SerializeField] private RectTransform _supinationEventCounter;
    [SerializeField] private RectTransform _ulnarEventCounter;
    [SerializeField] private RectTransform _radialEventCounter;

    [SerializeField] private Text _gripForceText;
    [SerializeField] private Text _rollText;
    [SerializeField] private Text _pitchText;
    [SerializeField] private Text _yawText;
    [SerializeField] private Button _resetRpyButton;
    [SerializeField] private Text _sensorKbps;

    [SerializeField] private RectTransform _untouchedBox;
    [SerializeField] private RectTransform _offsettingBox;
    [SerializeField] private Text _lockstateText;
    [SerializeField] private RectTransform _magstateBox;
    [SerializeField] private RectTransform _vibstateBox;
    [SerializeField] private Text _sampleRateText;
    [SerializeField] private RectTransform _chargeStateBox;
    [SerializeField] private Text _batteryText;

    [SerializeField] private Button _connectButton;
    [SerializeField] private Button _disconnectButton;
    [SerializeField] private Button _vibrateButton;
    [SerializeField] private Button _startStreamingButton;
    [SerializeField] private Button _stopStreamingButton;
    [SerializeField] private Text _streamingStateText;


    [SerializeField] private InputField _macAddressInput;
    [SerializeField] private Text _connectionStateText;
    [SerializeField] private RectTransform _cachedBox;

    private DeviceGateway _deviceGateway;

    void Start()
    {
        BuildEventSystem();
        _closeButton.onClick.AddListener(() => SetCanvasEnabled(false));
        _macAddressInput.characterLimit = _MAC_ADDRESS_CHARACTER_LIMIT;
        _macAddressInput.onValueChanged.AddListener(FormatMacAddressField);
        _connectButton.onClick.AddListener(OnConnectButtonClicked);
        _resetRpyButton.onClick.AddListener(() => _deviceGateway.ResetWristRpy());
        _disconnectButton.onClick.AddListener(() => _deviceGateway.Disconnect());
        _vibrateButton.onClick.AddListener(() => _deviceGateway.SendDeviceCommand(
            new DeviceCommand
            {
                VibrationEffect = DeviceCommand.Types.VibrationEffect.VibEffectStrongBuzz100,
                SamplingRate = DeviceCommand.Types.SamplingRate.Hz50
            }));
        _startStreamingButton.onClick.AddListener(() => _deviceGateway.StartStreamingData());
        _stopStreamingButton.onClick.AddListener(() => _deviceGateway.StopStreamingData());
    }

    private void BuildEventSystem()
    {
        if (GameObject.Find("EventSystem") == null)
        {
            var eventSystemGO = new GameObject
            {
                name = "EventSystem"
            };
            //eventSystemGO.transform.SetParent(transform);
            eventSystemGO.AddComponent<EventSystem>();
            eventSystemGO.AddComponent<StandaloneInputModule>();
        }
    }

    void Update()
    {
        if (_updateValues)
        {
            UpdateGripForce();
            UpdateGameRpy();
            UpdateDeviceStatus();
        }
        if (Input.touchCount > 4)
        {
            SetCanvasEnabled(true);
        }
    }

    private void OnConnectButtonClicked()
    {
        ClientGateway clientGateway = new ClientGateway(AndroidHelper.GetApplicationContext());
        _deviceGateway = clientGateway.GetDeviceGateway(_macAddressInput.text);
        _deviceGateway.OnConnectionStateChanged += (ConnectionState state) => OnConnectionStateChanged(state);
        _deviceGateway.OnStreamingStateChanged += (StreamingState state) => OnStreamingStateChanged(state);
        _deviceGateway.Connect();
    }

    private void OnStreamingStateChanged(StreamingState streamingState)
    {
        _streamingStateText.text = streamingState.ToString();

        if (streamingState == StreamingState.On)
        {
            _updateValues = true;
        }
    }

    private void OnConnectionStateChanged(ConnectionState connectionState)
    {
        _connectionStateText.text = connectionState.ToString();
       
        if (connectionState == ConnectionState.Connected)
        {
            _deviceGateway.OnGesture += TriggerGestureCounter;
            _macAddressInput.text = _deviceGateway.GetMacAddress();
        }
        else if (connectionState == ConnectionState.Disconnected)
        {
            _deviceGateway.OnConnectionStateChanged -= OnConnectionStateChanged;
            _deviceGateway.OnStreamingStateChanged -= OnStreamingStateChanged;
        }
    }

    private void TriggerGestureCounter(Gesture gesture)
    {
        switch (gesture.Type)
        {
            case GestureType.Squeeze:
                IncrementCounter(_squeezeEventCounter);
                break;
            case GestureType.Release:
                IncrementCounter(_releaseEventCounter);
                break;
            case GestureType.Flexion:
                IncrementCounter(_flexionEventCounter);
                break;
            case GestureType.Extension:
                IncrementCounter(_extensionEventCounter);
                break;
            case GestureType.Pronation:
                IncrementCounter(_pronationEventCounter);
                break;
            case GestureType.Supination:
                IncrementCounter(_supinationEventCounter);
                break;
            case GestureType.Ulnar:
                IncrementCounter(_ulnarEventCounter);
                break;
            case GestureType.Radial:
                IncrementCounter(_radialEventCounter);
                break;
        }
    }

    private void IncrementCounter(RectTransform counterRect)
    {
        counterRect.GetComponent<Image>().color = _green;
        var counterText = counterRect.GetComponentInChildren<Text>();
        int count = int.Parse(counterText.text);
        count++;
        counterText.text = count.ToString();
    }

    private void UpdateGripForce()
    {
        if (_deviceGateway.GetSensorData().Grip != null)
            _gripForceText.text = _deviceGateway.GetSensorData().Grip.Force + " Kg";
    }

    private void UpdateGameRpy()
    {
        WristRpyData gameRpyData = _deviceGateway.GetGameRpyData();
        _rollText.text = "R: " + Mathf.Floor(gameRpyData.Roll);
        _pitchText.text = "P: " + Mathf.Floor(gameRpyData.Pitch);
        _yawText.text = "Y: " + Mathf.Floor(gameRpyData.Yaw);
        _sensorKbps.text = _deviceGateway.GetSensorKbps().ToString();
    }

    private void UpdateDeviceStatus()
    {
        DeviceStatus deviceStatus = _deviceGateway.GetDeviceStatus();
        _untouchedBox.GetComponent<Image>().color = deviceStatus.Untouched ? _green : _red;
        _untouchedBox.GetComponentInChildren<Text>().text = deviceStatus.Untouched.ToString();
        _offsettingBox.GetComponent<Image>().color = deviceStatus.CalculatingOffset ? _green : _red;
        _offsettingBox.GetComponentInChildren<Text>().text = deviceStatus.CalculatingOffset.ToString();
        _lockstateText.text = deviceStatus.LockedState.ToString();
        _magstateBox.GetComponent<Image>().color = deviceStatus.RequiresMagCalibration ? _green : _red;
        _magstateBox.GetComponentInChildren<Text>().text = deviceStatus.RequiresMagCalibration.ToString();
        _vibstateBox.GetComponent<Image>().color = deviceStatus.Vibrating ? _green : _red;
        _vibstateBox.GetComponentInChildren<Text>().text = deviceStatus.Vibrating.ToString();
        _sampleRateText.text = deviceStatus.SamplingRate.ToString();
        _chargeStateBox.GetComponent<Image>().color = deviceStatus.Charging ? _green : _red;
        _chargeStateBox.GetComponentInChildren<Text>().text = deviceStatus.Charging.ToString();
        _batteryText.text = deviceStatus.BatteryLevel.ToString();

        _cachedBox.GetComponent<Image>().color = _deviceGateway.IsCached() ? _green : _red;   
        _cachedBox.GetComponentInChildren<Text>().text = _deviceGateway.IsCached().ToString();
    }

    private void SetCanvasEnabled(bool canvasEnabled)
    {
        GetComponent<Canvas>().enabled = canvasEnabled;
        GetComponent<CanvasScaler>().enabled = canvasEnabled;
    }

    private void FormatMacAddressField(string macAddress)
    {
        string rawMac = macAddress.Replace(":", "");
        string newMac = "";
        for (int i = 0; i < rawMac.Length && i < _MAC_ADDRESS_CHARACTER_LIMIT; i++)
        {
            if (i > 0 && i % 2 == 0)
                newMac += ":";
            newMac += rawMac[i];
        }
        _macAddressInput.text = newMac.ToUpper();
        _macAddressInput.caretPosition = _macAddressInput.text.Length;
    }
}
