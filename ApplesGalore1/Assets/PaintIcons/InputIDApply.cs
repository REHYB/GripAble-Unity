using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class InputIDApply : MonoBehaviour
{
    public Button yourButton;
    public GameObject input;

    // Start is called before the first frame update
    void Start() {
        Button btn = yourButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    // Update is called once per frame
    void Update()  {
        //TextMeshPro textmeshPro = GetComponent<TextMeshPro>();
        //textmeshPro.SetText("" + PaintGame.score);
    }

    void TaskOnClick() {
        PaintGame.userID = input.GetComponent<TMP_InputField>().text;
        PaintGame.applyUserID = true;
        PaintGame.programState = "RelaxCal";
        input.SetActive(false);
    }
}
