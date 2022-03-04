using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class GripMacAddr : MonoBehaviour {
    public GameObject input;
    public GameObject disappear;

    // Start is called before the first frame update
    void Start() {
    }

    // Update is called once per frame
    void Update() {
        if (input.GetComponent<TMP_InputField>().text == "Mac Address") {
            PaintGame.macAddress = "F1:FD:7F:8C:B2:61";
            Debug.Log(PaintGame.macAddress);
        }
        else { PaintGame.macAddress = input.GetComponent<TMP_InputField>().text; }
        if (PaintGame.applyUserID == true) {
            disappear.SetActive(false);
        }

    }
}