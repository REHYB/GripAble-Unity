using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draw_Tutorial : MonoBehaviour
{
    public Camera m_camera;
    public GameObject brush;

    LineRenderer currentLineRenderer;
    Vector2 lastPos;
    Vector3 newPos;
    float y_shift = 1000;
    bool stylus_WasDown = false;
    int stylus_DownCounter = 0;

    private void Start() {
        //Time.fixedDeltaTime = 1 /;
    }

    private void Update() {
        Draw();
    }

    // Start is called before the first frame update
    void Draw()
    {
        //if (Input.GetKeyDown(KeyCode.Mouse0)) {
        //    CreateBrush();
        //}
        //if (Input.GetKey(KeyCode.Mouse0)) {
        //    Vector2 mousePos = m_camera.ScreenToWorldPoint(Input.mousePosition);
        //    if (mousePos != lastPos) {
        //        AddAPoint(mousePos);
        //        lastPos = mousePos;
        //        //Debug.Log(mousePos);
        //    }
        //}

        // new touch
        if (UDPReceiver.stylus_point > -1 && stylus_WasDown == false) {
            newPos = new Vector3((float)UDPReceiver.stylus_x[0], -(float)UDPReceiver.stylus_y[0] + y_shift, 0.0f);
            CreateBrush();
            stylus_WasDown = true;
            //Debug.Log("Create Brush");
        }
        // no touch
        else if (UDPReceiver.stylus_point == -1) {
            stylus_DownCounter++;
            if (stylus_DownCounter > 10) {
                stylus_WasDown = false;
                stylus_DownCounter = 0;
            }
        }

        // new point
        else if (UDPReceiver.stylus_point > -1) {
            newPos = new Vector3((float)UDPReceiver.stylus_x[0], -(float)UDPReceiver.stylus_y[0]+ y_shift, 0.0f);
            //Debug.Log("myro: " + newPos + ", comp: " + Input.mousePosition);
            //Vector2 mousePos = m_camera.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos = m_camera.ScreenToWorldPoint(newPos);
            if (mousePos != lastPos) {
                if (mousePos != lastPos) {
                    AddAPoint(mousePos);
                    lastPos = mousePos;
                    //Debug.Log(mousePos);
                }
            }
        }
        else {
            currentLineRenderer = null;
        }
        if (UDPReceiver.stylus_point > -1) {
            stylus_DownCounter = 0;
        }

    }

    void CreateBrush() {
        GameObject brushInstance = Instantiate(brush);
        currentLineRenderer = brushInstance.GetComponent<LineRenderer>();
        Vector2 mousePos = m_camera.ScreenToWorldPoint(newPos);
        Debug.Log(newPos);
        currentLineRenderer.SetPosition(0, mousePos);
        currentLineRenderer.SetPosition(1, mousePos);

    }

    void AddAPoint(Vector2 pointPos) {
        currentLineRenderer.positionCount++;
        int positionIndex = currentLineRenderer.positionCount - 1;
        currentLineRenderer.SetPosition(positionIndex, pointPos);
    }
  
}
