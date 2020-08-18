using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : MonoBehaviour
{
    float speed = 6f;
    float amount = 0.5f;
    // Start is called before the first frame update
    void Start() {
        speed = Random.Range(4f, 10f);
        amount = Random.Range(1f, 1.8f);
    }

    // Update is called once per frame
    int counter = 0;
    void Update()  {
        if (PaintGame.programState == "grip") {
            //transform.rotation = new Quaternion(0, 0, Mathf.Sin(Time.time * 1),0);
            transform.Rotate(0f, 0f, Mathf.Cos(Time.time * speed) * amount, Space.Self);
        }


        if (PaintGame.destroyApples == true) {
            if (counter == 0) {
                Explode();
                counter = 1;
            }
        }
    }

    void Explode() {
        GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
        ParticleSystem exp = GetComponent<ParticleSystem>();
        var main = exp.main;
        if (PaintGame.programState.Contains("wait")) {
            main.startLifetime = PaintGame.waitTime;
        }
        else if (PaintGame.programState.Contains("grip")) {
            main.startLifetime = 1f;
        }
        exp.Play();
        Destroy(gameObject, exp.main.duration);
    }
}
