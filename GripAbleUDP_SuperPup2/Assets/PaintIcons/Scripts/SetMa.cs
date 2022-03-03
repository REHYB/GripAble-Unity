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
        if (PaintGame.applyUserID == true && PaintGame.gameLevel > 3 && PaintGame.fesStop == false) { GetComponent<TextMeshPro>().SetText("Set Intensity"); }
        else { GetComponent<TextMeshPro>().SetText(""); }
    }
}

