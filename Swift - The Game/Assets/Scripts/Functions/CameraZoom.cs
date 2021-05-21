using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Difficulty;

public class CameraZoom : MonoBehaviour
{
    private LevelDifficulty levelDifficulty;

    private Camera cam;
    private static float startSize = 1f; 

    void Awake()
    {
        levelDifficulty = GameObject.Find("LevelManager").GetComponent<LevelDifficulty>();
        cam = GetComponent<Camera>();

        cam.orthographicSize = startSize;
    }

    void Update()
    {
        Zoom(levelDifficulty.zoomValue, levelDifficulty.speedOfZomm);
    }

    void Zoom(int maxSize, float speed)
    {
        cam.orthographicSize = Mathf.SmoothStep(cam.orthographicSize, maxSize, speed);
    }
}
