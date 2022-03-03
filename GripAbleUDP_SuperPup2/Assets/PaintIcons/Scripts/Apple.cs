using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Apple : MonoBehaviour {
    float applePositionStart = 0f;
    float applePosition = 0f;
    float startTime = 0f;
    float appleHeight = 0f;//new
    int  trialTag = 0;
    bool boneCounted = false;
    bool boneContact = false;
    bool setHeight = false;

    void Start() {
        startTime = Time.time;
        transform.localScale = new Vector3 (1f, 1f, 1f);
        appleHeight = GetComponent<Transform>().position.y; //(float)UDPReceiver.sharedValue; //new
        applePositionStart = GetComponent<Transform>().position.x;
        transform.position = new Vector2(applePositionStart, appleHeight); //new
        if (PaintGame.order[PaintGame.set] == 2 && (PaintGame.gameLevel == 3 || PaintGame.gameLevel == 4) && transform.position.y != -8) { GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0); }
        else { GetComponent<SpriteRenderer>().color = new Color(0.85f + (Mathf.Sin(Time.time * 1f) * .15f), 0.85f + (Mathf.Sin(Time.time * 3f) * .15f), 0.85f + (Mathf.Sin(Time.time * 2f) * .15f), 1); }
    }

    void Update() {
        applePosition = applePositionStart - (Time.time - startTime) * 4f;
        transform.position = new Vector2(applePosition, transform.position.y); //-1.8f between
        if (transform.position.x > 3.0 && transform.position.x < 4.0 && setHeight == false) { PaintGame.targetPosition = transform.position.y; setHeight = true; }
        else if (transform.position.x < -3) { 
            if (PaintGame.gameLevel == 4 && PaintGame.order[PaintGame.set] == 5 && transform.position.y != -8) {
                PaintGame.scaleChallenge = PaintGame.scaleChallenge + 0.05f;
                if (PaintGame.scaleChallenge >= 1) { PaintGame.scaleChallenge = 1; }
            }
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.name == "climber" && Mathf.Abs(PaintGame.climberPosition - transform.position.y + 0.3f) < 1.5) {
            Explode();
        }
    }

    void Explode() {
        ParticleSystem exp = GetComponent<ParticleSystem>();
        if (boneCounted == false) {
            boneCounted = true;
            PaintGame.bonesCaught = PaintGame.bonesCaught+1;
            if (PaintGame.gameLevel == 4 && PaintGame.order[PaintGame.set] == 5 && transform.position.y != -8) {
                PaintGame.scaleChallenge = PaintGame.scaleChallenge - 0.05f;
                if (PaintGame.scaleChallenge <= 0) { PaintGame.scaleChallenge = 0; }
            }
            exp.Play();
        }
        Destroy(gameObject, exp.main.duration);
    }
}
