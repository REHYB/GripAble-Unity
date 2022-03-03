using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class enterMa : MonoBehaviour {
    // Start is called before the first frame update
    void Start() { 
    }

    // Update is called once per frame
    void Update()  {
        if (PaintGame.gameLevel == 2) { GetComponent<TextMeshPro>().SetText("Enter Intensity"); }
        else { GetComponent<TextMeshPro>().SetText(""); }
    }
}

