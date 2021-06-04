using UnityEngine;
using Difficulty;

public class CameraZoom : MonoBehaviour
{
    private LevelDifficulty levelDifficulty;

    private Camera cam;
    private const float StartSize = 1f; 

    private void Awake()
    {
        levelDifficulty = GameObject.Find("LevelManager").GetComponent<LevelDifficulty>();
        cam = GetComponent<Camera>();

        cam.orthographicSize = StartSize;
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
