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
        CameraPos(20);
    }

    void Zoom(float maxSize, float speed)
    {
        cam.orthographicSize = Mathf.SmoothStep(cam.orthographicSize, maxSize, speed);
    }

    void CameraPos(float offset)
    {
        Vector3 playerPos = new Vector3(playerTransform.position.x / offset, playerTransform.position.y / offset, -10);
        float posSpeedLerp = 0.5f;
        cam.transform.position = Vector3.Lerp(cam.transform.position, playerPos, posSpeedLerp);
    }
}
