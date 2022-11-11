using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class iconChange : MonoBehaviour
{
    public Sprite bronze;
    public Sprite silver;
    public Sprite gold;

    // Start is called before the first frame update
    void Start() {
        GetComponent<SpriteRenderer>().sprite = bronze; //PaintGame.climberColor;
    }

    // Update is called once per frame
    void Update() {
        if (PaintGame.bonesCaught <= PaintGame.stage1) {
            GetComponent<SpriteRenderer>().sprite = bronze; //PaintGame.climberColor;
        }
        else if (PaintGame.bonesCaught > PaintGame.stage1 && PaintGame.bonesCaught <= PaintGame.stage2) {
            GetComponent<SpriteRenderer>().sprite = silver; //PaintGame.climberColor;
        }
        else if (PaintGame.bonesCaught > PaintGame.stage2) {
            GetComponent<SpriteRenderer>().sprite = gold; //PaintGame.climberColor;
        }
    }
}
