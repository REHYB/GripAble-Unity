using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Climber : MonoBehaviour
{

    // Start is called before the first frame update
    void Start() {
    }

    // Update is called once per frame
    void Update() {
        transform.position = new Vector2(0, PaintGame.climberPosition); //-1.8f between 
        GetComponent<SpriteRenderer>().color = Color.white; //PaintGame.climberColor;
    }
}
