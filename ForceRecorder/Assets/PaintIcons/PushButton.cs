using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start() { 
    }

    // Update is called once per frame
    void Update() {
        if (PaintGame.programState.Contains("select")) {
            //GetComponent<SpriteRenderer>().color = Color.gray;
        }
        else {
            //GetComponent<SpriteRenderer>().color = new Color(0.9f + (Mathf.Sin(Time.time * 4f) * .1f), 0, 0, 1);
        }
    }

    private void OnMouseDown() {
        PaintGame.pushSelected = true;
    }
}
