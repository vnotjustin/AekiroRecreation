using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AEK;

public class JuiceManager : MonoBehaviour
{
    public static JuiceManager Main;

    public PostProcessManager ppManager;
    public CameraControl cameraControl;
    public ParticleSystem fogParticles;
    public AudioSource finalFightAudioEffects;
    public Animator platformEffect;

    private void Awake()
    {
        Main = this;
    }

    void Update()
    {
        if (Enemy.Main.currentPhaseIndex == 3)
        {
            ActivateFog();
            finalFightAudioEffects.volume = Mathf.Min(finalFightAudioEffects.volume + Time.deltaTime, .82f);
        }
    }

    public void PlayerHit()
    {
        ppManager.ActivateChroma(.4f, 1f);
        cameraControl.SetScreenshake(2.5f, .25f);
        platformEffect.SetTrigger("PlatformEffect");
    }

    public void PlayerBlock()
    {
        cameraControl.SetScreenshake(.25f, .1f);
    }

    public void ActivateFog()
    {
        if (!fogParticles.isPlaying)
        {
            fogParticles.Play();
        }
    }
}
