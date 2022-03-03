using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class InputFESData : MonoBehaviour {
    public GameObject input;
    public GameObject disappear;
    bool initFESCalib = false;

    // Start is called before the first frame update
    void Start() {
    }

    // Update is called once per frame
    void Update() {
        if (PaintGame.applyUserID == true && PaintGame.gameLevel == 2) {
            if (initFESCalib == false) {
                input.GetComponent<TMP_InputField>().text = "0";
                GetComponent<Image>().color = new Color(1, 1, 1, 1);
                initFESCalib = true;
            }
            PaintGame.FESmAEntry = double.Parse(input.GetComponent<TMP_InputField>().text);
        }
        else {
            input.GetComponent<TMP_InputField>().text = "";
            GetComponent<Image>().color = new Color(0, 0, 0, 0);
        }
    }
}
