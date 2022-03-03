using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class CircleTimer : MonoBehaviour
{
    public Image timer;

    void Start() {
    }

    void Update() {
        timer.fillAmount = PaintGame.secondsStart / PaintGame.waitTime; // fill up timer circle
		
        if (PaintGame.secondsStart > (PaintGame.waitTime * 4/5)) {
            timer.color = new Vector4 (1, 0.8239f, 0, 1); // yellow: time almost up
        }
        else {
            timer.color = new Vector4(0, 1, 0, 1); // green: plently of time
        }
    }
}
