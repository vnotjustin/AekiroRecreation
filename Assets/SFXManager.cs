using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{

    public static SFXManager main;

    private void Awake()
    {
        if (main != null && main != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            main = this;
            DontDestroyOnLoad(main);
        }
    }


    public AudioSource[] srcs;
    int currentSource;



    public void Play(AudioClip clip, float volume, float pitch)
    {

        srcs[currentSource].clip = clip;
        srcs[currentSource].volume = volume;
        srcs[currentSource].pitch = pitch;
        srcs[currentSource].Play();


        currentSource = (currentSource + 1) % srcs.Length;
    }

    public void Play(AudioClip clip, float volume, float pitch, float volumeVariability, float pitchVariability)
    {
        srcs[currentSource].clip = clip;
        srcs[currentSource].volume = volume + Random.Range(-volumeVariability, volumeVariability);
        srcs[currentSource].pitch = pitch + Random.Range(-pitchVariability, pitchVariability);
        srcs[currentSource].Play();

        currentSource = (currentSource + 1) % srcs.Length;
    }


}
