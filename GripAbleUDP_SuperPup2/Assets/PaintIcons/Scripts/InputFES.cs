using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class InputFES : MonoBehaviour {
    public GameObject input;
    public GameObject disappear;
    public Button button;

    // Start is called before the first frame update
    void Start() {
        GetComponentInChildren<TextMeshProUGUI>().text = PaintGame.FESmA.ToString("F0");
        button.GetComponent<Image>().color = new Color(1 - PaintGame.FESmA / PaintGame.FESmAmax, 1f, 1 - PaintGame.FESmA / PaintGame.FESmAmax, 1);
    }

    // Update is called once per frame
    void Update() {
        if (PaintGame.applyUserID == true && PaintGame.gameLevel > 3 && PaintGame.fesStop == false) {
            GetComponentInChildren<TextMeshProUGUI>().text = PaintGame.FESmA.ToString("F0");
            if (PaintGame.FESmAmax < 0) { GetComponent<Image>().color = new Color(1f, 1 - PaintGame.FESmA / PaintGame.FESmAmax, 1 - PaintGame.FESmA / PaintGame.FESmAmax, 1); }
            else { GetComponent<Image>().color = new Color(1 - ((PaintGame.FESmA+10) / PaintGame.FESmAmax), 1f, 1 - ((PaintGame.FESmA + 10) / PaintGame.FESmAmax), 1); }
        }
        else {
            button.GetComponent<Image>().color = new Color(0, 0, 0, 0);
            GetComponentInChildren<TextMeshProUGUI>().text = "";
        }
    }
}
