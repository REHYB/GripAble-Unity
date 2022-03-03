using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Instructions : MonoBehaviour {
    // Start is called before the first frame update
    void Start() { 
    }

    // Update is called once per frame
    void Update()  {
        if (PaintGame.initialize == false) {
            TextMeshPro textmeshPro = GetComponent<TextMeshPro>();
            textmeshPro.SetText("" + PaintGame.instruction);
        }
        else {
            TextMeshPro textmeshPro = GetComponent<TextMeshPro>();
            textmeshPro.SetText("SuperPup");
        }

        //textmeshPro.SetText("climb: " + PaintGame.climberPosition + " , chall: " + PaintGame.challengeHeight);
    }
}

