using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static GameManager instance;
    public static GameManager Main;

    public bool SwiftStrikes = false;
    public bool BlessingZhang = false;
    public bool SwordGuan = false;
    public bool StaggeringBlow = false;
    public bool PracticedSword = false;
    public bool CrushingStrike = false;
    public bool QuickEvasion = false;
    public bool ProtectionofDivine = false;
    public bool Thornmail = false;

    public int crystals = 9;



    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
        }

        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}
