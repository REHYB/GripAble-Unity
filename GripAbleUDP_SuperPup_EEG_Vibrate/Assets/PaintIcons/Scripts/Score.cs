using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class Score : MonoBehaviour {
    public Image timer;
    public Image cape;
    bool timeStart = false;

    void Start() {
        timer.color = new Vector4(1, 0, 0, 1);
    }

    void Update() {
        if (PaintGame.bonesCaught <= PaintGame.stage1) {
            timer.fillAmount = PaintGame.bonesCaught / 50f;
            timer.color = Color.red; //PaintGame.climberColor;
        }
        else if (PaintGame.bonesCaught > PaintGame.stage1 && PaintGame.bonesCaught <= PaintGame.stage2) {
            timer.fillAmount = (PaintGame.bonesCaught- PaintGame.stage1) / 100f;
            timer.color = new Vector4(0.804f, 0.498f, 0.196f, 1); 
        }
        else if (PaintGame.bonesCaught > PaintGame.stage2) {
            timer.fillAmount = (PaintGame.bonesCaught - PaintGame.stage2) / (PaintGame.stage3 - PaintGame.stage2);
            timer.color = new Vector4(0.753f, 0.753f, 0.753f, 1);
        }
    }
}
