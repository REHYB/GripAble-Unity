using System.Collections;
using System.Collections.Generic;
using Protos;
using Gripable;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerPanel : MonoBehaviour
{
    private readonly Color32 _green = new Color32(0, 255, 0, 90);
    private const int _MAC_ADDRESS_CHARACTER_LIMIT = 17;

    private bool _updateValues;
    private Canvas _canvas;

    [SerializeField] private Button _closeButton;

    [SerializeField] private InputField _macAddressInput;
    [SerializeField] private Text _connectionStateText;
    [SerializeField] private Button _connectButton;
    [SerializeField] private Button _disconnectButton;

    [SerializeField] private RectTransform _squeezeEventCounter;
    [SerializeField] private RectTransform _releaseEventCounter;
    [SerializeField] private RectTransform _flexionEventCounter;
    [SerializeField] private RectTransform _extensionEventCounter;
    [SerializeField] private RectTransform _pronationEventCounter;
    [SerializeField] private RectTransform _supinationEventCounter;
    [SerializeField] private RectTransform _ulnarEventCounter;
    [SerializeField] private RectTransform _radialEventCounter;

    [SerializeField] private Text _streamingStateText;
    [SerializeField] private Text _gripForceText;

    [SerializeField] private Text _wristRollText;
    [SerializeField] private Text _wristPitchText;
    [SerializeField] private Text _wristYawText;

    [SerializeField] private Text _rollText;
    [SerializeField] private Text _pitchText;
    [SerializeField] private Text _yawText;

    [SerializeField] private Button _resetRpyButton;
    [SerializeField] private Button _vibrateButton;

    [SerializeField] private InputField _normalizeScaleFactorInput;
    [SerializeField] private Button _normalizeScaleFactorButton;
    [SerializeField] private InputField _gestureScaleFactorInput;
    [SerializeField] private Button _gestureScaleFactorButton;
    [SerializeField] private Button _lowButton;
    [SerializeField] private Button _mediumButton;
    [SerializeField] private Button _highButton;

    [SerializeField] private Button _leftButton;
    [SerializeField] private Button _rightButton;

    [SerializeField] private bool _dontDestroyOnLoad = true;

    void Start()
    {
        BuildEventSystem();
        _closeButton.onClick.AddListener(() => SetCanvasEnabled(false));
        _macAddressInput.characterLimit = _MAC_ADDRESS_CHARACTER_LIMIT;
        _macAddressInput.onValueChanged.AddListener(FormatMacAddressField);
        GripablePlugin.Player.OnConnectionStateChanged += OnConnectionStateChanged;
        GripablePlugin.Player.OnStreamingStateChanged += OnStreamingStateChanged;
        GripablePlugin.Player.OnGesture += TriggerGestureCounter;
        _connectButton.onClick.AddListener(OnConnectButtonClicked);
        _resetRpyButton.onClick.AddListener(GripablePlugin.Player.ResetWristRpy);
        _disconnectButton.onClick.AddListener(GripablePlugin.Player.Disconnect);
        _vibrateButton.onClick.AddListener(() => GripablePlugin.Player.SendRumbleCommand(DeviceCommand.Types.VibrationEffect.VibEffectBuzz100));
        _normalizeScaleFactorButton.onClick.AddListener(SetNormalizeScaleFactor);
        _gestureScaleFactorButton.onClick.AddListener(SetGestureScaleFactor);
        _lowButton.onClick.AddListener(SetLowCalibration);
        _mediumButton.onClick.AddListener(SetMediumCalibration);
        _highButton.onClick.AddListener(SetHighCalibration);
        _leftButton.onClick.AddListener(() => GripablePlugin.Player.SetHand(Hand.Left));
        _rightButton.onClick.AddListener(() => GripablePlugin.Player.SetHand(Hand.Right));
        _canvas = GetComponent<Canvas>();
        if (_dontDestroyOnLoad)
            DontDestroyOnLoad(gameObject);
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
        if (_canvas.enabled && _updateValues)
        {
            UpdateGripForce();
            UpdateGameRpy();
            UpdateWristRpy();
        }
        if (!_canvas.enabled && Input.touchCount > 3)
        {
            SetCanvasEnabled(true);
        }
    }

    private void OnConnectButtonClicked()
    {
        GripablePlugin.Player.SetDevice(_macAddressInput.text);
        GripablePlugin.Player.Connect();
    }

    private void OnConnectionStateChanged(ConnectionState connectionState)
    {
        _connectionStateText.text = connectionState.ToString();
        if (connectionState == ConnectionState.Connected)
        {
            SetCanvasEnabled(false);
        }
    }

    //private bool _calibrationSet;

    private void OnStreamingStateChanged(StreamingState streamingState)
    {
            Debug.Log("OnStreamingStateChangedOn");
        _streamingStateText.text = streamingState.ToString();

        if (streamingState == StreamingState.On)//!_calibrationSet && streamingState == StreamingState.On)
        {
            if (ActivityLauncher.Config != null)
            {
                ActivityConfig config = ActivityLauncher.Config;
                _normalizeScaleFactorInput.text = config.ScaleFactor.ToString();
                //GripablePlugin.Player.SetHand(config.Hand);
                //GripablePlugin.Player.SetCalibration(config.GripCalibration, config.RollCalibration, config.PitchCalibration, config.YawCalibration, (float)config.ScaleFactor);
            }

            _updateValues = true;
            //_calibrationSet = true;
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
        _gripForceText.text = GripablePlugin.Player.GetGripForce().ToString();
    }

    private void UpdateGameRpy()
    {
        _rollText.text = GripablePlugin.Player.GetRoll().ToString();
        _pitchText.text = GripablePlugin.Player.GetPitch().ToString();
        _yawText.text = GripablePlugin.Player.GetYaw().ToString();
    }

    private void UpdateWristRpy()
    {
        WristRpyData wristRpy = GripablePlugin.Player.GetDevice().GetWristRpyData();
        _wristRollText.text = wristRpy.Roll.ToString();
        _wristPitchText.text = wristRpy.Pitch.ToString();
        _wristYawText.text = wristRpy.Yaw.ToString();
    }

    private void SetCanvasEnabled(bool canvasEnabled)
    {
        _canvas.enabled = canvasEnabled;
        GetComponent<CanvasScaler>().enabled = canvasEnabled;
    }

    private void SetNormalizeScaleFactor()
    {
        GripablePlugin.Player.SetNormalizeScaleFactor(float.Parse(_normalizeScaleFactorInput.text));
    }

    private void SetGestureScaleFactor()
    {
        GripablePlugin.Player.SetGestureScaleFactor(float.Parse(_gestureScaleFactorInput.text));
    }

    private void SetLowCalibration()
    {
        GripablePlugin.Player.SetCalibration(
            new Calibration { Min = CalibrationData.GRIP_LOW.Min, Max = CalibrationData.GRIP_LOW.Max, Type = MovementType.Grip },
            new Calibration { Min = CalibrationData.ROLL_LOW.Min, Max = CalibrationData.ROLL_LOW.Max, Type = MovementType.Roll },
            new Calibration { Min = CalibrationData.PITCH_LOW.Min, Max = CalibrationData.PITCH_LOW.Max, Type = MovementType.Pitch },
            new Calibration { Min = CalibrationData.YAW_LOW.Min, Max = CalibrationData.YAW_LOW.Max, Type = MovementType.Yaw },
            float.Parse(_gestureScaleFactorInput.text));
    }

    private void SetMediumCalibration()
    {
        GripablePlugin.Player.SetCalibration(
            new Calibration { Min = CalibrationData.GRIP_MEDIUM.Min, Max = CalibrationData.GRIP_MEDIUM.Max, Type = MovementType.Grip },
            new Calibration { Min = CalibrationData.ROLL_MEDIUM.Min, Max = CalibrationData.ROLL_MEDIUM.Max, Type = MovementType.Roll },
            new Calibration { Min = CalibrationData.PITCH_MEDIUM.Min, Max = CalibrationData.PITCH_MEDIUM.Max, Type = MovementType.Pitch },
            new Calibration { Min = CalibrationData.YAW_MEDIUM.Min, Max = CalibrationData.YAW_MEDIUM.Max, Type = MovementType.Yaw },
            float.Parse(_gestureScaleFactorInput.text));
    }

    private void SetHighCalibration()
    {
        GripablePlugin.Player.SetCalibration(
            new Calibration { Min = CalibrationData.GRIP_HIGH.Min, Max = CalibrationData.GRIP_HIGH.Max, Type = MovementType.Grip },
            new Calibration { Min = CalibrationData.ROLL_HIGH.Min, Max = CalibrationData.ROLL_HIGH.Max, Type = MovementType.Roll },
            new Calibration { Min = CalibrationData.PITCH_HIGH.Min, Max = CalibrationData.PITCH_HIGH.Max, Type = MovementType.Pitch },
            new Calibration { Min = CalibrationData.YAW_HIGH.Min, Max = CalibrationData.YAW_HIGH.Max, Type = MovementType.Yaw },
            float.Parse(_gestureScaleFactorInput.text));
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
