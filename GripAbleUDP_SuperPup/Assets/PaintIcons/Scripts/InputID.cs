using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class InputID : MonoBehaviour {
    public GameObject input;
    public GameObject disappear;

    // Start is called before the first frame update
    void Start() {
    }

    // Update is called once per frame
    void Update() {
        PaintGame.userID = input.GetComponent<TMP_InputField>().text;
        if (PaintGame.applyUserID == true) {
            disappear.SetActive(false);
        }
    }
}
