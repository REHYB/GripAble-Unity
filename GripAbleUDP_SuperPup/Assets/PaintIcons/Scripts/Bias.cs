using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;



public class Bias : MonoBehaviour
{
    public Button yourButton;
    public GameObject input;

    // Start is called before the first frame update
    void Start() {
        //Button btn = yourButton.GetComponent<Button>();
        yourButton.onClick.AddListener(TaskOnClick);
    }

    void Update()  {

    }

    void TaskOnClick() {
        PaintGame.bias = PaintGame.force;
    }
}
