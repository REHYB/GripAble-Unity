using System.Collections;
using System.Collections.Generic;
using Protos;
using Gripable;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AppPanel : MonoBehaviour
{
    private readonly Color32 _green = new Color32(0, 255, 0, 90);
    private const int _MAC_ADDRESS_CHARACTER_LIMIT = 17;

    private bool _updateValues;

    [SerializeField] private Button _closeButton;

    [SerializeField] private Text _autoConnectText;
    [SerializeField] private Text _demoUserText;
    [SerializeField] private Text _genderText;
    [SerializeField] private Text _handText;
    [SerializeField] private Text _launchedFromPackageNameText;

    [SerializeField] private Text _macAddressText;
    [SerializeField] private Text _userIdText;
    [SerializeField] private Text _userNameText;

    [SerializeField] private Text _gripCalibrationMaxText;
    [SerializeField] private Text _gripCalibrationMinText;
    [SerializeField] private Text _pitchCalibrationMaxText;
    [SerializeField] private Text _pitchCalibrationMinText;
    [SerializeField] private Text _rollCalibrationMaxText;
    [SerializeField] private Text _rollCalibrationMinText;
    [SerializeField] private Text _yawCalibrationMaxText;
    [SerializeField] private Text _yawCalibrationMinText;

    [SerializeField] private Text _scaleFactorText;


    void Start()
    {
        BuildEventSystem();
        _closeButton.onClick.AddListener(() => SetCanvasEnabled(false));

        _autoConnectText.text = ActivityLauncher.Config.AutoConnect.ToString();
        _demoUserText.text = ActivityLauncher.Config.DemoUser.ToString();
        _genderText.text = ActivityLauncher.Config.Gender.ToString();
        _handText.text = ActivityLauncher.Config.Hand.ToString();
        _launchedFromPackageNameText.text = ActivityLauncher.Config.LaunchedFromPackageName.ToString();

        _macAddressText.text = ActivityLauncher.Config.MacAddress;
        _userIdText.text = ActivityLauncher.Config.PatientUid;
        _userNameText.text = ActivityLauncher.Config.Username;

        _gripCalibrationMaxText.text = ActivityLauncher.Config.GripCalibration.Max.ToString();
        _gripCalibrationMinText.text = ActivityLauncher.Config.GripCalibration.Min.ToString();
        _pitchCalibrationMaxText.text = ActivityLauncher.Config.PitchCalibration.Max.ToString();
        _pitchCalibrationMinText.text = ActivityLauncher.Config.PitchCalibration.Min.ToString();
        _rollCalibrationMaxText.text = ActivityLauncher.Config.RollCalibration.Max.ToString();
        _rollCalibrationMinText.text = ActivityLauncher.Config.RollCalibration.Min.ToString();
        _yawCalibrationMaxText.text = ActivityLauncher.Config.YawCalibration.Max.ToString();
        _yawCalibrationMinText.text = ActivityLauncher.Config.YawCalibration.Min.ToString();
        _scaleFactorText.text = ActivityLauncher.Config.ScaleFactor.ToString();

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
        if (Input.touchCount > 5)
        {
            SetCanvasEnabled(true);
        }
    }

    private void SetCanvasEnabled(bool canvasEnabled)
    {
        GetComponent<Canvas>().enabled = canvasEnabled;
        GetComponent<CanvasScaler>().enabled = canvasEnabled;
    }
}
