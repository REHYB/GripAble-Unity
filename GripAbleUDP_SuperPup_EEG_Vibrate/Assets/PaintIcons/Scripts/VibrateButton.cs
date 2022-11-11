using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Gripable;

public class VibrateButton : MonoBehaviour {
    public GameObject input;
    public GameObject disappear;
    public Button button;
    bool init = false;
    bool VibrateOn = false;
    float timePrev = Time.time;
    float vibrateDelay = 5;

    // Start is called before the first frame update
    void Start() {
        button.onClick.AddListener(TaskOnClick);
    }

    // Update is called once per frame
    void Update() {
        if ( VibrateOn == true && ( Time.time > (timePrev + vibrateDelay) ) ) {
            GripablePlugin.Player.SendRumbleCommand(Protos.DeviceCommand.Types.VibrationEffect.VibEffectStrongClick100, Protos.DeviceCommand.Types.SamplingRate.Hz25);
            timePrev = Time.time;
        }
        //if (PaintGame.gameLevel == 1) {
        //    GetComponent<Image>().color = new Color(0, 0, 0, 0);
        //    GetComponentInChildren<TextMeshProUGUI>().text = "";
        //}
        //else if (PaintGame.gameLevel == 2) {
        //    GetComponent<Image>().color = new Color(0, 1, 0, 1);
        //    transform.position = new Vector2(-11.8f, -5.4f);
        //    GetComponentInChildren<TextMeshProUGUI>().text = "FES Set";
        //}
        //else if (PaintGame.gameLevel == 3) {
        //    GetComponent<Image>().color = new Color(0, 0, 0, 0);
        //    GetComponentInChildren<TextMeshProUGUI>().text = "";
        //}
    }

    void TaskOnClick() {
        VibrateOn = !VibrateOn;
        timePrev = Time.time - vibrateDelay;

        //if (PaintGame.gameLevel == 0) {
        //    PaintGame.applyUserID = true;
        //}
        //else if (PaintGame.gameLevel == 2) {
        //    PaintGame.fesCounter++;
        //    GetComponentInChildren<TextMeshProUGUI>().text = "";
        //}
        //GetComponent<Image>().color = new Color(1, 1, 1, 0);
    }
}
