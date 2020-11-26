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
        Button btn = yourButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    void Update()  {
        if (PaintGame.idButtoninteractable == false) {
            yourButton.GetComponent<Image>().color = Color.white;
            yourButton.GetComponent<Button>().interactable = false;
        }
        else {
            yourButton.GetComponent<Image>().color = Color.yellow;
            yourButton.GetComponent<Button>().interactable = true;
        }
    }

    void TaskOnClick() {
        PaintGame.applyUserID = true;
        PaintGame.counter++;
    }
}
