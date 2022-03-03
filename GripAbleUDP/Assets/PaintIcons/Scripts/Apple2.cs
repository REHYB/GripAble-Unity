using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple2 : MonoBehaviour {

    float applePositionStart = 14.5f;
    float applePosition = 14.5f;
    float startTime = 0f;
    float appleHeight = 0f;//new

    void Start() {
        startTime = Time.time;
        applePositionStart = GetComponent<Transform>().position.x;
        transform.position = new Vector2(applePositionStart, appleHeight); //new
    }

    void Update() {
        applePosition = applePositionStart - (Time.time - startTime) * 4f;
        transform.position = new Vector2(applePosition, appleHeight); //-1.8f between 
        if (applePosition < -2f) {
            Explode();
        }
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.name == "climber") {
            PaintGame.wallContact = true;
            Debug.Log(PaintGame.wallContact);
        }
    }

    void Explode() {
        ParticleSystem exp = GetComponent<ParticleSystem>();
        Destroy(gameObject, exp.main.duration);
    }
}