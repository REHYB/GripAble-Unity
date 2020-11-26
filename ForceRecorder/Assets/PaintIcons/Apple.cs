using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : MonoBehaviour {
    void Start() {
        transform.localScale = new Vector3 (0.2f, 0.2f, 0.2f);
    }

    void Update() {
        transform.position = new Vector2(0.82f, PaintGame.appleHeight); //-1.8f between 
        GetComponent<SpriteRenderer>().color = PaintGame.appleColor;
    }
}
