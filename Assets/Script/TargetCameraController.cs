using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetCameraController : MonoBehaviour
{

    float zoomInRate = 0.01f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void zoomIn()
    {
        Camera camera = gameObject.GetComponent<Camera>();

        camera.rect = new Rect(
            new Vector2(1 - (camera.rect.width + zoomInRate), 0),
            new Vector2(camera.rect.width + zoomInRate, camera.rect.height + zoomInRate)
            );
    }

    public float getSize()
    {
        return gameObject.GetComponent<Camera>().rect.width;
    }
}
