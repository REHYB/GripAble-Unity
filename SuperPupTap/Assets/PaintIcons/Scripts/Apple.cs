using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Apple : MonoBehaviour {
    public Sprite bone1;
    public Sprite bone2;
    public Sprite bone3;
    public Sprite bone4;
    public Sprite bone5;
    public Sprite bone6;

    float applePositionStart = 14.5f;
    float applePosition = 14.5f;
    float startTime = 0f;
    float appleHeight = 0f;//new
    int trialTag = 0;
    bool boneCounted = false;
    bool boneContact = false;
    int rewardBone = 0;
    int challengeBone = 0;
    bool dataSaved = false;
    int decoyFlagBone = 0;

    void Start() {
        startTime = Time.time;
        transform.localScale = new Vector3 (0.5f, 0.5f, 0.5f);
        appleHeight = GetComponent<Transform>().position.y; //(float)UDPReceiver.sharedValue; //new
        applePositionStart = GetComponent<Transform>().position.x;
        transform.position = new Vector2(applePositionStart, appleHeight); //new
        trialTag = PaintGame.trials;

        if (appleHeight == PaintGame.appleHeightVector[12]) { GetComponent<SpriteRenderer>().sprite = bone1; }
        else if (PaintGame.reward == 1) {
            GetComponent<SpriteRenderer>().sprite = bone1;
        }
        else if (PaintGame.reward == 2) {
            GetComponent<SpriteRenderer>().sprite = bone2;
        }
        else if (PaintGame.reward == 3) {
            GetComponent<SpriteRenderer>().sprite = bone3;
        }
        else if (PaintGame.reward == 4) {
            GetComponent<SpriteRenderer>().sprite = bone4;
        }
    }

    void Update() {
        // move bone
        applePosition = applePositionStart - (Time.time - startTime) * 4f;
        transform.position = new Vector2(applePosition, appleHeight); // -1.8f between
        //if bone is missed or other bone is hit -> document that bone is missed
        if ((applePosition < -2f && dataSaved == false && boneContact == false) || (trialTag == PaintGame.tagDestroy && dataSaved == false && boneContact == false)) {
            dataSaved = true;
            if (appleHeight == PaintGame.appleHeightVector[12]) {
                decoyFlagBone = 1;
            }
            CallSaveSimpleData(0);
            Explode();
        }
    }

    //if bone is hit -> document that bone is hit
    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.name == "climber" && dataSaved == false  && boneContact == false) {
            dataSaved = true;
            boneContact = true;
            PaintGame.tagDestroy = trialTag;
            if (appleHeight == PaintGame.appleHeightVector[12]) {
                decoyFlagBone = 1;
            }
            //adjust challenge level in calib
            if (PaintGame.trials > PaintGame.calibTrialsMin && PaintGame.trials <= PaintGame.calibTrials) {
                PaintGame.challengeTap = PaintGame.challengeTap - 0.05f;
            }
            //adjust challenge level in game
            if (PaintGame.trials > PaintGame.calibTrials && appleHeight == PaintGame.appleHeightVector[8]) {
                PaintGame.challengeTap = PaintGame.challengeTap - 0.005f;
            }
            //save data
            CallSaveSimpleData(1);
            Explode();
        }
    }

    void Explode() {
        ParticleSystem exp = GetComponent<ParticleSystem>();
        if (boneContact == true && boneCounted == false) {
            boneCounted = true;
            if (GetComponent<SpriteRenderer>().sprite == bone1) { PaintGame.bonesCaught = PaintGame.bonesCaught+0.5f; }
            else if (GetComponent<SpriteRenderer>().sprite == bone2) { PaintGame.bonesCaught = PaintGame.bonesCaught+1; }
            else if (GetComponent<SpriteRenderer>().sprite == bone3) { PaintGame.bonesCaught = PaintGame.bonesCaught+2; }
            else if (GetComponent<SpriteRenderer>().sprite == bone4) { PaintGame.bonesCaught = PaintGame.bonesCaught+3f; }
            exp.Play();
        }
        Destroy(gameObject, exp.main.duration);
    }

    int targetReps = 0;
    void CallSaveSimpleData(int targetHit) {
        targetReps++;

        if (GetComponent<SpriteRenderer>().sprite == bone1) { rewardBone = 1;
            if (targetHit == 1 ) { }
        }
        else if (GetComponent<SpriteRenderer>().sprite == bone2) { rewardBone = 2;
            if (targetHit == 1) { GetComponents<AudioSource>()[0].Play(); }
        }
        else if (GetComponent<SpriteRenderer>().sprite == bone3) { rewardBone = 3;
            if (targetHit == 1) { GetComponents<AudioSource>()[1].Play(); }
        }
        else if (GetComponent<SpriteRenderer>().sprite == bone4) { rewardBone = 4;
            if (targetHit == 1) { GetComponents<AudioSource>()[2].Play(); }
        }

        if (appleHeight == PaintGame.appleHeightVector[12]) { challengeBone = 1; }
        else if (appleHeight < PaintGame.appleHeightVector[4]) { challengeBone = 2; }
        else if (appleHeight < PaintGame.appleHeightVector[8]) { challengeBone = 3; }
        else if (appleHeight == PaintGame.appleHeightVector[8]) { challengeBone = 4; }

        Save.SaveSimpleData(trialTag, rewardBone, challengeBone, targetHit, decoyFlagBone);
        //PaintGame.rewardBonePrev = rewardBone;
        //PaintGame.challengeBonePrev = challengeBone;
        //PaintGame.targetHitPrev = targetHit;
    }
}
