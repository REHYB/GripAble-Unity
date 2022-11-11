using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SetMa : MonoBehaviour {
    // Start is called before the first frame update
    void Start() { 
    }

    // Update is called once per frame
    void Update()  {
        if (PaintGame.gameLevel == 2 || PaintGame.gameLevel == 0) { GetComponent<TextMeshPro>().SetText(""); }
        else { GetComponent<TextMeshPro>().SetText("Set mA"); }
    }
}

