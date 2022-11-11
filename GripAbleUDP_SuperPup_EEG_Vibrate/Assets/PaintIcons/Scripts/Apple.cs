using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Apple : MonoBehaviour {
    float applePositionStart = 14.5f;
    float applePosition = 14.5f;
    float startTime = 0f;
    float appleHeight = 0f;//new
    int  trialTag = 0;
    bool boneCounted = false;
    bool boneContact = false;

    void Start() {
        startTime = Time.time;
        transform.localScale = new Vector3 (1f, 1f, 1f);
        appleHeight = GetComponent<Transform>().position.y; //(float)UDPReceiver.sharedValue; //new
        applePositionStart = GetComponent<Transform>().position.x;
        transform.position = new Vector2(applePositionStart, appleHeight); //new
        //GetComponent<SpriteRenderer>().color = GetComponent<SpriteRenderer>().color = new Color(0.85f + (Mathf.Sin(Time.time * 1f) * .15f), 0.85f + (Mathf.Sin(Time.time * 3f) * .15f), 0.85f + (Mathf.Sin(Time.time * 2f) * .15f), 1);
    }

    void Update() {
        GetComponent<SpriteRenderer>().color = new Color(1 ,1, 1 - PaintGame.eegSignalColor, 1);
        applePosition = applePositionStart - (Time.time - startTime) * 4f;
        transform.position = new Vector2(applePosition, transform.position.y); //-1.8f between 
        if (transform.position.x > 3.5 && transform.position.x < 4.5) { PaintGame.targetPosition = transform.position.y; }
        else if (transform.position.x < -3) { Destroy(gameObject); }
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
            exp.Play();
        }
        Destroy(gameObject, exp.main.duration);
    }
}
