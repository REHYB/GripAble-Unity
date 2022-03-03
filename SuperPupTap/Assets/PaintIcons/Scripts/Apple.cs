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

    void Start() {
        startTime = Time.time;
        transform.localScale = new Vector3 (1f, 1f, 1f);
        appleHeight = GetComponent<Transform>().position.y; //(float)UDPReceiver.sharedValue; //new
        applePositionStart = GetComponent<Transform>().position.x;
        transform.position = new Vector2(applePositionStart, appleHeight); //new
        trialTag = PaintGame.trials;

        if (appleHeight == PaintGame.appleHeightVector[0] && PaintGame.decoyBone == false) { GetComponent<SpriteRenderer>().sprite = bone1; }
        else if (appleHeight == PaintGame.appleHeightVector[0] && PaintGame.decoyBone == true) {
            GetComponent<SpriteRenderer>().sprite = bone6;
            GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 0 / 5f, 1f);
        }
        else if (PaintGame.reward == 1) {
            GetComponent<SpriteRenderer>().sprite = bone1;
            GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 5/5f, 1f);
        }
        else if (PaintGame.reward == 2) {
            GetComponent<SpriteRenderer>().sprite = bone2;
            GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 4/5f, 1f);
        }
        else if (PaintGame.reward == 3) {
            GetComponent<SpriteRenderer>().sprite = bone3;
            GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 3/5f, 1f);
        }
        else if (PaintGame.reward == 4) {
            GetComponent<SpriteRenderer>().sprite = bone4;
            GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 2/5f, 1f);
        }
        else if (PaintGame.reward == 5) {
            GetComponent<SpriteRenderer>().sprite = bone5;
            GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1/5f, 1f);
        }
        else if (PaintGame.reward == 6) {
            GetComponent<SpriteRenderer>().sprite = bone6;
            GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 0/5f, 1f);
        }
        trialTag = PaintGame.trials;
    }

    void Update() {
        applePosition = applePositionStart - (Time.time - startTime) * 4f;
        transform.position = new Vector2(applePosition, appleHeight); //-1.8f between 
        if (applePosition < -2f && boneContact == false && dataSaved == false) {
            if (appleHeight != PaintGame.appleHeightVector[0] ) {
                CallSaveSimpleData(0);
                dataSaved = true;
                if (trialTag % 2 == 0 && trialTag > 4 && trialTag < 20 && appleHeight != PaintGame.appleHeightVector[0]) {
                    PaintGame.challengeTap = PaintGame.challengeTap + 0.55f * 1 / trialTag;
                }
            }
            Explode();
            PaintGame.tagDestroy = trialTag;
        }
        else if (trialTag == PaintGame.tagDestroy && boneContact == false && dataSaved == false) {
            if (appleHeight != PaintGame.appleHeightVector[0]) { CallSaveSimpleData(0); dataSaved = true;
            };
            Explode();
        }
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.name == "climber" && boneContact == false && trialTag != PaintGame.tagDestroy) {
            boneContact = true;
            if (trialTag % 2 == 0 && trialTag >= 20 && appleHeight != PaintGame.appleHeightVector[0]) {
                PaintGame.targetHitArray[(trialTag - 20) / 2] = 1;
            }
            else if (trialTag % 2 == 0 && trialTag > 4 && appleHeight != PaintGame.appleHeightVector[0]) {
                PaintGame.challengeTap = PaintGame.challengeTap - 0.55f * 1 / trialTag;
            }
            PaintGame.tagDestroy = trialTag;
            if (appleHeight > PaintGame.appleHeightVector[0]){
                PaintGame.success = true;
            }
            else { PaintGame.success = false; }

            if (appleHeight == PaintGame.appleHeightVector[6]) {
                PaintGame.challengeTap = PaintGame.challengeTap - 0.02f;
            }
            if (appleHeight != PaintGame.appleHeightVector[0] && dataSaved == false) { CallSaveSimpleData(1); dataSaved = true;}
            Explode();
        }
    }

    void Explode() {
        ParticleSystem exp = GetComponent<ParticleSystem>();
        if (boneContact == true && boneCounted == false) {
            boneCounted = true;
            if (GetComponent<SpriteRenderer>().sprite == bone1) { PaintGame.bonesCaught = PaintGame.bonesCaught+1; }
            else if (GetComponent<SpriteRenderer>().sprite == bone2) { PaintGame.bonesCaught = PaintGame.bonesCaught+2; }
            else if (GetComponent<SpriteRenderer>().sprite == bone3) { PaintGame.bonesCaught = PaintGame.bonesCaught+3; }
            else if (GetComponent<SpriteRenderer>().sprite == bone4) { PaintGame.bonesCaught = PaintGame.bonesCaught+4; }
            else if (GetComponent<SpriteRenderer>().sprite == bone5) { PaintGame.bonesCaught = PaintGame.bonesCaught+5; }
            else if (GetComponent<SpriteRenderer>().sprite == bone6) { PaintGame.bonesCaught = PaintGame.bonesCaught+6; }
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
            if (targetHit == 1) { GetComponents<AudioSource>()[1].Play(); }
        }
        else if (GetComponent<SpriteRenderer>().sprite == bone5) { rewardBone = 5;
            if (targetHit == 1) { GetComponents<AudioSource>()[2].Play(); }
        }
        else if (GetComponent<SpriteRenderer>().sprite == bone6) { rewardBone = 6;
            if (targetHit == 1) { GetComponents<AudioSource>()[2].Play(); }
        }
        if (appleHeight == PaintGame.appleHeightVector[1]) { challengeBone = 1; }
        else if (appleHeight == PaintGame.appleHeightVector[2]) { challengeBone = 2; }
        else if (appleHeight == PaintGame.appleHeightVector[3]) { challengeBone = 3; }
        else if (appleHeight == PaintGame.appleHeightVector[4]) { challengeBone = 4; }
        else if (appleHeight == PaintGame.appleHeightVector[5]) { challengeBone = 5; }
        else if (appleHeight == PaintGame.appleHeightVector[6]) { challengeBone = 6; }
        Save.SaveSimpleData(trialTag, rewardBone, challengeBone, targetHit);
        PaintGame.rewardBonePrev = rewardBone;
        PaintGame.challengeBonePrev = challengeBone;
        PaintGame.targetHitPrev = targetHit;
        
    }
}
