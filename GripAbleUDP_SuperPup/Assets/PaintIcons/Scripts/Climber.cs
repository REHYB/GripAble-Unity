using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Climber : MonoBehaviour
{
    public Sprite red;
    public Sprite bronze;
    public Sprite silver;
    public Sprite gold;

    // Start is called before the first frame update
    void Start() {
    }

    // Update is called once per frame
    void Update() {
        transform.position = new Vector2(0, PaintGame.climberPosition); //-1.8f between 
        GetComponent<SpriteRenderer>().color = Color.yellow; //PaintGame.climberColor;

        if (PaintGame.bonesCaught <= PaintGame.stage1) {
            //GetComponent<SpriteRenderer>().sprite = red; //PaintGame.climberColor;
        }
        else if (PaintGame.bonesCaught > PaintGame.stage1 && PaintGame.bonesCaught <= PaintGame.stage2) {
            //GetComponent<SpriteRenderer>().sprite = bronze; //PaintGame.climberColor;
        }
        else if (PaintGame.bonesCaught > PaintGame.stage2 && PaintGame.bonesCaught <= PaintGame.stage3) {
            //GetComponent<SpriteRenderer>().sprite = silver; //PaintGame.climberColor;
        }
        else if (PaintGame.bonesCaught > PaintGame.stage3) {
            //GetComponent<SpriteRenderer>().sprite = gold; //PaintGame.climberColor;
        }
    }
}

