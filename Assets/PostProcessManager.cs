using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessManager : MonoBehaviour
{


    ChromaticAberration chroma;


    float chromaTimeLeft;
    float chromaMaxTime;
    float chromaMaxStrength;
    public PostProcessVolume volume;
    void Awake()
    {
        volume.profile.TryGetSettings(out chroma);
    }


    void Update()
    {
        if (chromaTimeLeft > 0)
        {
            chroma.intensity.value = (chromaTimeLeft / chromaMaxTime) * chromaMaxStrength;
            chromaTimeLeft -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            ActivateChroma(.25f, 1f);
        }
    }


    public void ActivateChroma(float time, float strength)
    {
        chromaMaxTime = time;
        chromaTimeLeft = time;
        chromaMaxStrength = Mathf.Clamp01(strength);
    }
}
