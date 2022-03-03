using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class ComputerIP : MonoBehaviour {
    public GameObject input;
    public GameObject disappear;

    // Start is called before the first frame update
    void Start() {
    }

    // Update is called once per frame
    void Update() {
        if (input.GetComponent<TMP_InputField>().text == "Computer IP Address") {
            PaintGame.computerIP = "127.0.0.1";
            //Debug.Log(PaintGame.computerIP);
        }
        else { PaintGame.computerIP = input.GetComponent<TMP_InputField>().text; }
        if (PaintGame.applyUserID == true) {
            disappear.SetActive(false);
        }

    }
}
