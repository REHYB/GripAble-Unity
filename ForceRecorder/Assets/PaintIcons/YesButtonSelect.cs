using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gripable;

public class YesButtonSelect : MonoBehaviour
{
    void Start() {
    }

    // Update is called once per frame
    void Update() {
        GetComponent<SpriteRenderer>().color = PaintGame.yesButtonColor;

        //if (PaintGame.programState.Contains("select")) {
        //    //transform.Rotate(0f, 0f, Mathf.Cos(Time.time * speed) * amount, Space.Self);
        //    GetComponent<SpriteRenderer>().color = new Color(0, 0.85f + (Mathf.Sin(Time.time * 2f) * .15f), 0, 1);
        //}
        //else {
        //    GetComponent<SpriteRenderer>().color = Color.gray;
        //}
    }

    void OnMouseDown() {
        //PaintGame.yesSelected = true;
    }
}
