using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gripable;

public class YesButtonSelect : MonoBehaviour
{
    void Start() {
    }

    // Update is called once per frame
    void Update() {
        GetComponent<SpriteRenderer>().color = PaintGame.yesButtonColor;
    }
        void OnMouseDown() {
        if (PaintGame.programState == 4) {
            PaintGame.yesSelected = true;
        }
    }
}
