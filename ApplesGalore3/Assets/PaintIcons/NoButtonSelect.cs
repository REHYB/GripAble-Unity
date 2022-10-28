using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoButtonSelect : MonoBehaviour
{
    // Start is called before the first frame update
    void Start() { 
    }

    // Update is called once per frame
    void Update() {
        GetComponent<SpriteRenderer>().color = PaintGame.noButtonColor;

        //if (PaintGame.programState.Contains("select")) {
        //    GetComponent<SpriteRenderer>().color = new Color(0, 0.85f + (Mathf.Sin(Time.time * 2f) * .15f), 0, 1);

        //}
        //else {
        //    GetComponent<SpriteRenderer>().color = Color.gray;
        //}
    }

    private void OnMouseDown() {
        //if (PaintGame.programState == 3) {
        if (PaintGame.programState == 5) {
            PaintGame.noSelected = true;
        }
    }
}
