using UnityEngine;
using TMPro;

public class FpsCount : MonoBehaviour
{
    private float timer, refresh, avgFramerate;
    public TextMeshProUGUI frameRateText;

    // Update is called once per frame
    private void Update()
    {
        var timelapse = Time.smoothDeltaTime;
        timer = timer <= 0 ? refresh : timer -= timelapse;

        if (timer <= 0) avgFramerate = (int)(1f / timelapse);
        frameRateText.text = avgFramerate + "FPS";
    }
}
