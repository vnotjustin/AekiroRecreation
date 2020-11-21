using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
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

    public bool heavyUnlocked = false;

    public int crystals = 9;



    private void Awake()
    {
        if(Main != null)
        {
            Destroy(gameObject);
        }

        else
        {
            Main = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}
