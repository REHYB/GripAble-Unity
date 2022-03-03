using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trunk : MonoBehaviour {
    public Sprite trunk0;
    public Sprite trunk1;
    public Sprite trunk2;
    public Sprite trunk3;
    public Sprite trunk4;
    public Sprite trunk5;
    public Sprite trunk6;

    // Start is called before the first frame update
    void Start() {
        GetComponent<SpriteRenderer>().sprite = trunk0;
    }

    // Update is called once per frame
    void Update() {
        if (PaintGame.challengeForce < 6) {
            GetComponent<SpriteRenderer>().sprite = trunk0;
        }
        else if (PaintGame.challengeForce == 6) {
            GetComponent<SpriteRenderer>().sprite = trunk1;
        }
        else if (PaintGame.challengeForce == 7) {
            GetComponent<SpriteRenderer>().sprite = trunk2;
        }
        else if (PaintGame.challengeForce == 8) {
            GetComponent<SpriteRenderer>().sprite = trunk3;
        }
        else if (PaintGame.challengeForce == 9) {
            GetComponent<SpriteRenderer>().sprite = trunk4;
        }
        else if (PaintGame.challengeForce == 10) {
            GetComponent<SpriteRenderer>().sprite = trunk5;
        }
        else if (PaintGame.challengeForce == 11) {
            GetComponent<SpriteRenderer>().sprite = trunk6;
        }
    }
}