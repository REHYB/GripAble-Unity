using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Apple : MonoBehaviour {

    float applePostion = 14.5f;
    float startTime = 0f;
    bool boneNew = true;

    void Start() {
        startTime = Time.time;
        transform.localScale = new Vector3 (1f, 1f, 1f);
        //transform.position = new Vector2(applePostion, PaintGame.appleHeight); //-1.8f between
        GetComponent<SpriteRenderer>().color = GetComponent<SpriteRenderer>().color = new Color(0.85f + (Mathf.Sin(Time.time * 1f) * .15f), 0.85f + (Mathf.Sin(Time.time * 3f) * .15f), 0.85f + (Mathf.Sin(Time.time * 2f) * .15f), 1);
    }


    void Update() {
        applePostion = (Time.time - startTime) * -4 + 14.5f;
        transform.position = new Vector2(applePostion, transform.position.y); //-1.8f between 
        //GetComponent<SpriteRenderer>().color = PaintGame.appleColor;

        if (applePostion > -2 && applePostion < 2) {
            if ((PaintGame.climberPosition + 1.25 > PaintGame.appleHeight) && (PaintGame.climberPosition - 0.25 < PaintGame.appleHeight)) {
                if (boneNew == true) {
                    PaintGame.bonesCaught++;
                    boneNew = false;
                }
                PaintGame.score++;
                //Explode();
            }
        }
        if (applePostion < -20) {
            Explode();
        }

    }

    void Explode() {
        ParticleSystem exp = GetComponent<ParticleSystem>();
        exp.Play();
        Destroy(gameObject, exp.main.duration);
    }


}
