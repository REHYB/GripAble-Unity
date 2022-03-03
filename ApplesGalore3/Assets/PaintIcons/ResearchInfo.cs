using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResearchInfo : MonoBehaviour {
    // Start is called before the first frame update
    void Start() { 
    }

    // Update is called once per frame
    void Update()  {
        TextMeshPro textmeshPro = GetComponent<TextMeshPro>();
        textmeshPro.SetText("Trial: " + Save.increment + ", Reps: " + PaintGame.reps + " /50");
        //textmeshPro.SetText(PaintGame.challengeHeight + ", Reps: " + PaintGame.reps);

    }
}

