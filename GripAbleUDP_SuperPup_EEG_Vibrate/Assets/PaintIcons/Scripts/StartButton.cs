using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StartButton : MonoBehaviour {
    public GameObject input;
    public GameObject disappear;
    public Button button;
    bool init = false;

    // Start is called before the first frame update
    void Start() {
        button.onClick.AddListener(TaskOnClick);
    }

    // Update is called once per frame
    void Update() {
        if (PaintGame.gameLevel == 1) {
            GetComponent<Image>().color = new Color(0, 0, 0, 0);
            GetComponentInChildren<TextMeshProUGUI>().text = "";
        }
        else if (PaintGame.gameLevel == 2) {
            GetComponent<Image>().color = new Color(0, 1, 0, 1);
            transform.position = new Vector2(-11.8f, -5.4f);
            GetComponentInChildren<TextMeshProUGUI>().text = "FES Set";
        }
        else if (PaintGame.gameLevel == 3) {
            GetComponent<Image>().color = new Color(0, 0, 0, 0);
            GetComponentInChildren<TextMeshProUGUI>().text = "";
        }
    }

    void TaskOnClick() { 
        if (PaintGame.gameLevel == 0) {
            PaintGame.applyUserID = true;
        }
        else if (PaintGame.gameLevel == 2) {
            PaintGame.fesCounter++;
            GetComponentInChildren<TextMeshProUGUI>().text = "";
        }
        GetComponent<Image>().color = new Color(1, 1, 1, 0);
    }
}