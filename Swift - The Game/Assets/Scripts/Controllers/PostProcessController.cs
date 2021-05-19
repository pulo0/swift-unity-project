using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessController : MonoBehaviour
{
    private static float startIntensity = 0f;

    [Header("Post Process Components")]
    public PostProcessVolume postProcessVolume;
    private Vignette vignette;
    private ChromaticAberration chromaticAberration;

    void Awake()
    {
        postProcessVolume.profile.TryGetSettings(out vignette);
        postProcessVolume.profile.TryGetSettings(out chromaticAberration);

        vignette.intensity.value = startIntensity;
    }


    public void IncreaseOnDamage()
    {
        float increasingValue = 0.05f;
        float increase = 0.05f;

        if(vignette.intensity.value <= 0.5f)
        {
            vignette.intensity.value += increasingValue;
            increasingValue += increase;
        }
    }
}
