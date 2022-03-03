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
        yourButton.GetComponent<Image>().color = new Color(0f, 1f, 0f, 1f);
    }

    void Update()  {

    }

    void TaskOnClick() {
        PaintGame.applyUserID = true;
        input.SetActive(false);
    }
}
