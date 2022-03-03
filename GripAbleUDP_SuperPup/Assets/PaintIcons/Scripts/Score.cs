using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class Score : MonoBehaviour {
    public Image timer;
    bool timeStart = false;
    public static float error = 0f;
    float clock = 0f;
    float clockReset = 0f;
    float clockPrev = 0f;
    float level = 2.5f;
    bool trialStarted = false;

    void Start() {
        //timer.color = new Vector4(1, 0, 0, 1);
        PaintGame.bonesCaught = 0;
    }

    void Update() {
        error = Mathf.Abs(PaintGame.climberPosition - ((float)UDPReceiver.sharedValue - 7.5f));

        //calculate score every 50ms; if error of 1 for 1s then score increases by 0.5; if off by 1 for 10 seconds score is 5 (max 10)
        clock = Time.time;
        if (clock > clockPrev + 0.02 ) { PaintGame.bonesCaught = PaintGame.bonesCaught*0.99f + 0.01f*error; clockPrev = clock; }

        //score resets to 0 if target at 0 for 1.5s (experiment rest phase)
        if ( (float)UDPReceiver.sharedValue != 0 ) { trialStarted = true;  clockReset = clock; }
        else if ( clock > clockReset + 1.5f || trialStarted == false) { trialStarted = false; PaintGame.bonesCaught = 0; clockReset = clock; }

        //display error
        timer.fillAmount = PaintGame.bonesCaught / level;

        if (timer.fillAmount < 0.4) {
            timer.color = Color.green; //PaintGame.climberColor;
        }
        else if (timer.fillAmount < 0.8) {
            timer.color = Color.yellow; //PaintGame.climberColor;
        }
        else { timer.color = Color.red; }
    }
}
