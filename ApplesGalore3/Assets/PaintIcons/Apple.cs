using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : MonoBehaviour
{
    public Sprite apple0;
    public Sprite apple1;
    public Sprite apple2;
    public Sprite apple3;
    public Sprite apple4;
    public Sprite apple5;
    public Sprite apple6;

    // Start is called before the first frame update
    void Start() {
        GetComponent<SpriteRenderer>().sprite = apple0;
    }

    // Update is called once per frame
    void Update() {
        if (PaintGame.rewardApples == 0) {
            GetComponent<SpriteRenderer>().sprite = apple0;
        }
        else if (PaintGame.rewardApples == 1) {
            GetComponent<SpriteRenderer>().sprite = apple1;
        }
        else if (PaintGame.rewardApples == 2) {
            GetComponent<SpriteRenderer>().sprite = apple2;
        }
        else if (PaintGame.rewardApples == 3) {
            GetComponent<SpriteRenderer>().sprite = apple3;
        }
        else if (PaintGame.rewardApples == 4) {
            GetComponent<SpriteRenderer>().sprite = apple4;
        }
        else if (PaintGame.rewardApples == 5) {
            GetComponent<SpriteRenderer>().sprite = apple5;
        }
        else if (PaintGame.rewardApples == 6) {
            GetComponent<SpriteRenderer>().sprite = apple6;
        }
    }
}
