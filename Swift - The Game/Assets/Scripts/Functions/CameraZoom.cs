using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Difficulty;

public class CameraZoom : MonoBehaviour
{
    private LevelDifficulty levelDifficulty;

    private Camera cam;
    private const float startSize = 1f; 

    private void Awake()
    {
        levelDifficulty = GameObject.Find("LevelManager").GetComponent<LevelDifficulty>();
        cam = GetComponent<Camera>();

        cam.orthographicSize = startSize;
    }

    private void Update()
    {
        Zoom(levelDifficulty.zoomValue, levelDifficulty.speedOfZoom);
    }

    private void Zoom(int maxSize, float speed)
    {
        cam.orthographicSize = Mathf.SmoothStep(cam.orthographicSize, maxSize, speed);
    }
}
