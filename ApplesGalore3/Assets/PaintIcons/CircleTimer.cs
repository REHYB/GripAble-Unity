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
        timer.fillAmount = 0;
    }

    void Update() {

        if (PaintGame.programState == 0 || PaintGame.programState == 4 || PaintGame.programState == 5) {
            timer.fillAmount = 0; // fill up timer circle
        }
        else if (PaintGame.programState == 1 || PaintGame.programState == 3 ) {
            timer.fillAmount = (Time.time - PaintGame.secondsStart)/PaintGame.waitTime; // fill up timer circle
        }
        else if (PaintGame.programState == 2 || PaintGame.programState == 6) {
            timer.fillAmount = (Time.time - PaintGame.secondsStart) / PaintGame.waitTimeGrip; // fill up timer circle
        }

        if (timer.fillAmount <= 0.5) {
            timer.color = new Vector4(0, 1, 0, 1); // green: plently of time
        }
        else if (timer.fillAmount > 0.5 && timer.fillAmount < 0.8) {
            timer.color = new Vector4 (1, 0.8239f, 0, 1); // yellow: time almost up
        }
        else {
            timer.color = new Vector4(1, 0, 0, 1); // green: plently of time
        }
    }
}
