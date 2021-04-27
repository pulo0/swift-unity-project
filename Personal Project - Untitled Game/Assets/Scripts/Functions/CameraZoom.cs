using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    private Camera cam;
    private static float startSize = 1f;
    public Transform playerTransform; 

    void Awake()
    {
        cam = GetComponent<Camera>();

        cam.orthographicSize = startSize;
    }

    void Update()
    {
        Zoom(8f, 0.11f);
    }

    void Zoom(float maxSize, float speed)
    {
        cam.orthographicSize = Mathf.SmoothStep(cam.orthographicSize, maxSize, speed);
    }
}
