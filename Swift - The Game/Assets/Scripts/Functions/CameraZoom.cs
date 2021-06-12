using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    private LevelSetting levelSetting;

    private Camera cam;
    private const float StartSize = 1f; 

    private void Awake()
    {
        levelSetting = GameObject.Find("LevelManager").GetComponent<LevelSetting>();
        cam = GetComponent<Camera>();

        cam.orthographicSize = StartSize;
    }

    private void Update()
    {
        Zoom(levelSetting.zoomValue, levelSetting.speedOfZoom);
    }

    private void Zoom(int maxSize, float speed)
    {
        cam.orthographicSize = Mathf.SmoothStep(cam.orthographicSize, maxSize, speed);
    }
}
