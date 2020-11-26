using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glove : MonoBehaviour
{

    // Start is called before the first frame update
    void Start() {
    }

    // Update is called once per frame
    void Update() {
        transform.position = PaintGame.glovePosition;
        GetComponent<SpriteRenderer>().color = PaintGame.gloveColor;

        //    if (PaintGame.programState.Contains("select")) {
        //        transform.position = new Vector2(0, PaintGame.climberPositionMin);
        //    }

        //    if (PaintGame.climberPositionLocked == false) {
        //        GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);
        //        //////Cursor Position Controls Climber Position
        //        //PaintGame.climberPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition)[1];
        //        //transform.position = new Vector2(0, PaintGame.climberPosition);

        //        //Button Press Controls Climber Posiiton
        //        if (PaintGame.pushSelected == true) {
        //            transform.position = new Vector2(0, transform.position.y + .02f);
        //            PaintGame.climberPosition = transform.position.y;
        //            //PaintGame.pushSelected = false;
        //        }
        //    }
        //    else {
        //        GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
        //    }
        //}
    }
}
