using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UnlockButton : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        


    }

    public void UnlockAbility()
    {
        if (this.tag == "SwiftStrikes" && GameManager.Main.crystals >= 1)
        {
            GameManager.Main.crystals = GameManager.Main.crystals - 1;
            GameManager.Main.SwiftStrikes = true;
        }
        if (this.tag == "BlessingZhang" && GameManager.Main.crystals >= 2)
        {
            GameManager.Main.crystals = GameManager.Main.crystals - 2;
            GameManager.Main.BlessingZhang = true;
        }
        if (this.tag == "SwordGuan" && GameManager.Main.crystals >= 3)
        {
            GameManager.Main.crystals = GameManager.Main.crystals - 3;
            GameManager.Main.SwordGuan = true;
        }
        if (this.tag == "StaggeringBlow" && GameManager.Main.crystals >= 1)
        {
            GameManager.Main.crystals = GameManager.Main.crystals - 1;
            GameManager.Main.StaggeringBlow = true;
        }
        if (this.tag == "PracticedSword" && GameManager.Main.crystals >= 2)
        {
            GameManager.Main.crystals = GameManager.Main.crystals - 2;
            GameManager.Main.PracticedSword = true;
        }
        if (this.tag == "CrushingStrike" && GameManager.Main.crystals >= 3)
        {
            GameManager.Main.crystals = GameManager.Main.crystals - 3;
            GameManager.Main.CrushingStrike = true;
        }
        if (this.tag == "QuickEvasion" && GameManager.Main.crystals >= 1)
        {
            GameManager.Main.crystals = GameManager.Main.crystals - 1;
            GameManager.Main.QuickEvasion = true;
        }
        if (this.tag == "ProtectionofDivine" && GameManager.Main.crystals >= 2)
        {
            GameManager.Main.crystals = GameManager.Main.crystals - 2;
            GameManager.Main.ProtectionofDivine = true;
        }
        if (this.tag == "Thornmail" && GameManager.Main.crystals >= 3)
        {
            GameManager.Main.crystals = GameManager.Main.crystals - 3;
            GameManager.Main.Thornmail = true;
        }
    }
}
