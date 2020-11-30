using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    public static ParticleManager instance;
    public Animator PlayerAnimator;

    public GameObject LightSlashMarker;
    public GameObject DodgeSlashMarker;
    public GameObject HeavySlashMarker;

    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        
    }
    void Update()
    {
        
    }

    public void SpawnLightSlashMarker() 
    {
        Instantiate(LightSlashMarker);
    }

    public void SpawnDodgeSlashMarker() 
    {
        Instantiate(DodgeSlashMarker);
    }

    public void SpawnHeavySlashMarker()
    {
        Instantiate(HeavySlashMarker);
    }
}
