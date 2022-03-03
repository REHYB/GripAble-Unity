using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreDisplay : MonoBehaviour {
    // Start is called before the first frame update
    void Start() { 
    }

    // Update is called once per frame
    void Update()  {
        if (PaintGame.initialize == false) {
            TextMeshPro textmeshPro = GetComponent<TextMeshPro>();
            textmeshPro.SetText("0");
        }
        else {
            TextMeshPro textmeshPro = GetComponent<TextMeshPro>();
            textmeshPro.SetText("" + PaintGame.bonesCaught.ToString("F1"));
        }

        //textmeshPro.SetText("climb: " + PaintGame.climberPosition + " , chall: " + PaintGame.challengeHeight);
    }
}

