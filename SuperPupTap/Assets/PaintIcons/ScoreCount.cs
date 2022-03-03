using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreCount : MonoBehaviour {
    // Start is called before the first frame update
    void Start() { 
    }

    // Update is called once per frame
    void Update()  {
        if (PaintGame.applyUserID == true) {
            TextMeshPro textmeshPro = GetComponent<TextMeshPro>();
            textmeshPro.SetText(PaintGame.bonesCaught.ToString());
        }
        else {
            TextMeshPro textmeshPro = GetComponent<TextMeshPro>();
            textmeshPro.SetText("0");
        }
    }
}

