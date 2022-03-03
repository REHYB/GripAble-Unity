using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Instructions2 : MonoBehaviour {
    // Start is called before the first frame update
    void Start() { 
    }

    // Update is called once per frame
    void Update() {
        TextMeshPro textmeshPro = GetComponent<TextMeshPro>();
        textmeshPro.SetText("" + PaintGame.instruction2);
        if (PaintGame.instruction2.Contains("Relax")) { textmeshPro.color = new Color(0.85f, 0, 0, 1); }
        else { textmeshPro.color = new Color(0, 0.85f, 0, 1); } 
        //if (PaintGame.set >= PaintGame.order.Length) { textmeshPro.color = new Color(0, 0.85f, 0, 1); }
        //else if (PaintGame.gameLevel == 1 || PaintGame.gameLevel == 2 || (PaintGame.gameLevel == 4 && PaintGame.order[PaintGame.set] == 2)) { textmeshPro.color = new Color(0.85f, 0, 0, 1); }
        //else if (PaintGame.order[PaintGame.set] != 2) { textmeshPro.color = new Color(0, 0.85f, 0, 1); }
    }
}

