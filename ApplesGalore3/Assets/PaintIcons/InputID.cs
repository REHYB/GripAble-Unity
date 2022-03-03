using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class InputID : MonoBehaviour
{
    //public GameObject input;
    // Start is called before the first frame update
    void Start() {
        //input.SetActive(false); // false to hide, true to show
    }

    // Update is called once per frame
    void Update()  {
        TextMeshPro textmeshPro = GetComponent<TextMeshPro>();
        //textmeshPro.SetText("" + PaintGame.score);
    }
}
