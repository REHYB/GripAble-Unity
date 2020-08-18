using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AppleScore : MonoBehaviour
{
    // Start is called before the first frame update
    void Start() { 
    }

    // Update is called once per frame
    void Update()  {
        TextMeshPro textmeshPro = GetComponent<TextMeshPro>();
        textmeshPro.SetText("" + PaintGame.score);
    }
}
