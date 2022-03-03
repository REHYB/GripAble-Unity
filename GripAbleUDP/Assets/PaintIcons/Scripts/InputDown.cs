using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class InputDown : MonoBehaviour
{
    public Button yourButton;
    public GameObject input;

    // Start is called before the first frame update
    void Start() {
        yourButton.onClick.AddListener(TaskOnClick);
        yourButton.GetComponent<Image>().color = new Color(0f, 0f, 1f, 0f);
        yourButton.GetComponentInChildren<TextMeshProUGUI>().text = "";
        yourButton.GetComponent<Button>().interactable = false;
    }

    void Update()  {
        if (PaintGame.applyUserID == true && PaintGame.protocol == 2) {
            yourButton.GetComponent<Image>().color = new Color(0.5424528f, 0.6796547f, 1f, 1);
            yourButton.GetComponentInChildren<TextMeshProUGUI>().text = "DOWN";
            yourButton.GetComponent<Button>().interactable = true;
        }
    }

    void TaskOnClick() {
        PaintGame.appleHeight = PaintGame.appleHeight - 0.25f;
    }
}
