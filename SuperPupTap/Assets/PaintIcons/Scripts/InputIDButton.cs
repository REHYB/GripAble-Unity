using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;



public class InputIDButton : MonoBehaviour
{
    public Button yourButton;
    public GameObject input;

    // Start is called before the first frame update
    void Start() {
        yourButton.onClick.AddListener(TaskOnClick);
        yourButton.GetComponent<Image>().color = Color.green;
        yourButton.GetComponent<Button>().interactable = true;
    }

    void Update()  {
    }

    void TaskOnClick() {
        if (yourButton.GetComponent<Object>().name == "BStart") {
            PaintGame.applyUserID = true;
            input.SetActive(false);
        }
        else if (yourButton.GetComponent<Object>().name == "BTapMe") {
            PaintGame.tapDetected = true;
        }
    }
}
