using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UnlockButton : MonoBehaviour
{
    private bool canUnlock = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(canUnlock && this.tag == "SwiftStrikes" && GameManager.Main.crystals >= 1)
        {
            GameManager.Main.crystals = GameManager.Main.crystals - 1;
            GameManager.Main.SwiftStrikes = true;
        }
        if (canUnlock && this.tag == "BlessingZhang" && GameManager.Main.crystals >= 2)
        {
            GameManager.Main.crystals = GameManager.Main.crystals - 2;
            GameManager.Main.BlessingZhang = true;
        }
        if (canUnlock && this.tag == "SwordGuan" && GameManager.Main.crystals >= 3)
        {
            GameManager.Main.crystals = GameManager.Main.crystals - 3;
            GameManager.Main.SwordGuan = true;
        }
        if (canUnlock && this.tag == "StaggeringBlow" && GameManager.Main.crystals >= 1)
        {
            GameManager.Main.crystals = GameManager.Main.crystals - 1;
            GameManager.Main.StaggeringBlow = true;
        }
        if (canUnlock && this.tag == "PracticedSword" && GameManager.Main.crystals >= 2)
        {
            GameManager.Main.crystals = GameManager.Main.crystals - 2;
            GameManager.Main.PracticedSword = true;
        }
        if (canUnlock && this.tag == "CrushingStrike" && GameManager.Main.crystals >= 3)
        {
            GameManager.Main.crystals = GameManager.Main.crystals - 3;
            GameManager.Main.CrushingStrike = true;
        }
        if (canUnlock && this.tag == "QuickEvasion" && GameManager.Main.crystals >= 1)
        {
            GameManager.Main.crystals = GameManager.Main.crystals - 1;
            GameManager.Main.QuickEvasion = true;
        }
        if (canUnlock && this.tag == "ProtectionofDivine" && GameManager.Main.crystals >= 2)
        {
            GameManager.Main.crystals = GameManager.Main.crystals - 2;
            GameManager.Main.ProtectionofDivine = true;
        }
        if (canUnlock && this.tag == "Thornmail" && GameManager.Main.crystals >= 3)
        {
            GameManager.Main.crystals = GameManager.Main.crystals - 3;
            GameManager.Main.Thornmail = true;
        }


    }

    public void UnlockAbility()
    {
        canUnlock = true;
    }
}
