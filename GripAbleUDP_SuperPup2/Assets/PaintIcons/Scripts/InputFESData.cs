using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class InputFESData : MonoBehaviour {
    public GameObject input;
    public GameObject disappear;
    int fesCalibPrevious = 0;

    // Start is called before the first frame update
    void Start() {
    }

    // Update is called once per frame
    void Update() {
        if (PaintGame.applyUserID == true && PaintGame.gameLevel == 2) {
            GetComponent<Image>().color = new Color(1, 1, 1, 1);
            if (PaintGame.fesCalibCounter > fesCalibPrevious) {
                fesCalibPrevious = PaintGame.fesCalibCounter;
                int.TryParse(input.GetComponent<TMP_InputField>().text, out PaintGame.fesCalib[PaintGame.fesCalibCounter-1]);
                if (PaintGame.fesCalibCounter == PaintGame.forceSteps || PaintGame.fesCalibCounter == PaintGame.forceSteps*2) { input.GetComponent<TMP_InputField>().text = "0"; }
            }
            //if (PaintGame.initFESCalib == true) {
            //    PaintGame.fesCalib[PaintGame.fesCounter] = int.Parse(input.GetComponent<TMP_InputField>().text);
            //    Debug.Log(int.Parse(input.GetComponent<TMP_InputField>().text));
            //    GetComponent<Image>().color = new Color(1, 1, 1, 1);
            //    PaintGame.initFESCalib = false;
            //}
        }
        else {
            input.GetComponent<TMP_InputField>().text = "";
            GetComponent<Image>().color = new Color(0, 0, 0, 0);
        }
    }
}
