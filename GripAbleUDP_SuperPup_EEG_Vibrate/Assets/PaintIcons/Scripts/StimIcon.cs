using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class StimIcon : MonoBehaviour {
    public GameObject input;
    public GameObject disappear;

    // Start is called before the first frame update
    void Start() {
    }

    // Update is called once per frame
    void Update() {
        if (PaintGame.gameLevel == 0) { transform.position = new Vector2(0, (5-8)/2); } //-1.8f between
        else { transform.position = new Vector2(0, PaintGame.climberPosition); } //-1.8f between
        GetComponent<SpriteRenderer>().color = new Color(1-PaintGame.eegSignalColor, 1, 1-PaintGame.eegSignalColor,1);
    }
}