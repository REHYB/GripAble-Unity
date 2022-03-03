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
        //Button btn = yourButton.GetComponent<Button>();
        yourButton.onClick.AddListener(TaskOnClick);
        if (yourButton.GetComponent<Object>().name == "BTapMe") {
            yourButton.GetComponent<Image>().color = new Color(0f, 1f, 0f, 0f);
            yourButton.GetComponentInChildren<TextMeshProUGUI>().text = "";
        }
    }

    void Update()  {
        if ((yourButton.GetComponent<Object>().name == "BGripable" || yourButton.GetComponent<Object>().name == "BTap") && PaintGame.applyUserID == false) {
            yourButton.GetComponent<Image>().color = new Color(1f, 0.602824f, 0.4784313f);
            yourButton.GetComponent<Button>().interactable = true;
        }
        else if ((yourButton.GetComponent<Object>().name == "BAaron" || yourButton.GetComponent<Object>().name == "BVerity" || yourButton.GetComponent<Object>().name == "BLeeza" || yourButton.GetComponent<Object>().name == "BChiara") && PaintGame.inputTypeSelected == true && PaintGame.applyUserID == false) {
            yourButton.GetComponent<Image>().color = new Color(1f, 0.9942086f, 0.4858491f);
            yourButton.GetComponent<Button>().interactable = true;
        }
        else if (yourButton.GetComponent<Object>().name == "BStart" && PaintGame.inputTypeSelected == true && PaintGame.protocolSelected == true && PaintGame.applyUserID == false) {
            yourButton.GetComponent<Image>().color = new Color(0.5330188f, 1f, 0.5330188f);
            yourButton.GetComponent<Button>().interactable = true;
        }
        else if (PaintGame.applyUserID == true) {
            if (yourButton.GetComponent<Object>().name == "BTapMe" && PaintGame.inputType == 2) {
                yourButton.GetComponentInChildren<TextMeshProUGUI>().text = "TAP \n FAST";
                yourButton.GetComponent<Image>().color = Color.green;
                yourButton.GetComponent<Button>().interactable = true;
            }
            else {
                input.SetActive(false);
            }
        }
        else {
            if (yourButton.GetComponent<Object>().name != "BTapMe") { 
            yourButton.GetComponent<Image>().color = Color.white;
            yourButton.GetComponent<Button>().interactable = false;
            }
        }
    }

    void TaskOnClick() {
        if (yourButton.GetComponent<Object>().name == "BGripable" || yourButton.GetComponent<Object>().name == "BTap") {
            PaintGame.inputTypeSelected = true;
            if (yourButton.GetComponent<Object>().name == "BGripable") { PaintGame.inputType = 1; }
            else if (yourButton.GetComponent<Object>().name == "BTap") { PaintGame.inputType = 2; }
        }
        else if (yourButton.GetComponent<Object>().name == "BAaron" || yourButton.GetComponent<Object>().name == "BVerity" || yourButton.GetComponent<Object>().name == "BLeeza" || yourButton.GetComponent<Object>().name == "BChiara") {
            PaintGame.protocolSelected = true;
            if (yourButton.GetComponent<Object>().name == "BAaron") { PaintGame.protocol = 1; }
            else if (yourButton.GetComponent<Object>().name == "BVerity") { PaintGame.protocol = 2; }
            else if (yourButton.GetComponent<Object>().name == "BLeeza") { PaintGame.protocol = 3; }
            else if (yourButton.GetComponent<Object>().name == "BChiara") { PaintGame.protocol = 4; }
        }
        else if (yourButton.GetComponent<Object>().name == "BStart") {
            PaintGame.applyUserID = true;
            input.SetActive(false);
        }
        else if (yourButton.GetComponent<Object>().name == "BTapMe") {
            PaintGame.tapDetected = true;
        }
    }
}
