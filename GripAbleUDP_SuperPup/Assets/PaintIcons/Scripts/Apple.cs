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

    void Start() {
        GetComponent<SpriteRenderer>().color = new Color(0.7830189f, 0f, 0f, 1f);
    }

    void Update() {
        transform.position = new Vector2(0, (float)UDPReceiver.sharedValue - 7.5f); //-1.8f between
        if (Score.error < 0.1) {
            GetComponent<SpriteRenderer>().color = Color.green;
        }
        else { GetComponent<SpriteRenderer>().color = Color.red; }
    }

    void OnTriggerEnter2D(Collider2D col) {
        
    }

    void Explode() {
        ParticleSystem exp = GetComponent<ParticleSystem>();
        if (boneContact == true && boneCounted == false) {
            boneCounted = true;
            exp.Play();
        }
        Destroy(gameObject, exp.main.duration);
    }
}
