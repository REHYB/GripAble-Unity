using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Reps : MonoBehaviour {
    // Start is called before the first frame update
    void Start() { 
    }

    // Update is called once per frame
    void Update()  {
        if (PaintGame.applyUserID == true) {
            TextMeshPro textmeshPro = GetComponent<TextMeshPro>();
            textmeshPro.SetText("" + PaintGame.reps);
        }
        else {
            TextMeshPro textmeshPro = GetComponent<TextMeshPro>();
            textmeshPro.SetText("0");
        }
    }
}

