using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UnlockButton : MonoBehaviour
{
    public Image iconImage;
    public Sprite nonequippedSprite;
    public Sprite equippedSprite;

    // Start is called before the first frame update
    void Start()
    {
        
    }



    public void UnlockAbility()
    {
        if (this.tag == "SwiftStrikes")
        {
            if (!GameManager.Main.SwiftStrikes && GameManager.Main.crystals >= 1)
            {
                GameManager.Main.crystals = GameManager.Main.crystals - 1;
                GameManager.Main.SwiftStrikes = true;
                SetUsed(true);
            }
            else
            {
                GameManager.Main.SwiftStrikes = false;
                GameManager.Main.crystals = GameManager.Main.crystals + 1;
                SetUsed(false);
            }       
        }
        if (this.tag == "BlessingZhang")
        {
            if (!GameManager.Main.BlessingZhang && GameManager.Main.crystals >= 2)
            {
                GameManager.Main.crystals = GameManager.Main.crystals - 2;
                GameManager.Main.BlessingZhang = true;
                SetUsed(true);
            }
            else
            {
                GameManager.Main.BlessingZhang = false;
                GameManager.Main.crystals = GameManager.Main.crystals + 2;
                SetUsed(false);
            }
        }
        if (this.tag == "SwordGuan")
        {
            if (!GameManager.Main.SwordGuan && GameManager.Main.crystals >= 3)
            {
                GameManager.Main.crystals = GameManager.Main.crystals - 3;
                GameManager.Main.SwordGuan = true;
                SetUsed(true);
            }
            else
            {
                GameManager.Main.SwordGuan = false;
                GameManager.Main.crystals = GameManager.Main.crystals + 3;
                SetUsed(false);
            }
        }
        if (this.tag == "StaggeringBlow")
        {
            if (!GameManager.Main.StaggeringBlow && GameManager.Main.crystals >= 1)
            {
                GameManager.Main.crystals = GameManager.Main.crystals - 1;
                GameManager.Main.StaggeringBlow = true;
                SetUsed(true);
            }
            else
            {
                GameManager.Main.StaggeringBlow = false;
                GameManager.Main.crystals = GameManager.Main.crystals + 1;
                SetUsed(false);
            }
        }
        if (this.tag == "PracticedSword")
        {
            if (!GameManager.Main.PracticedSword && GameManager.Main.crystals >= 2)
            {
                GameManager.Main.crystals = GameManager.Main.crystals - 2;
                GameManager.Main.PracticedSword = true;
                SetUsed(true);
            }
            else
            {
                GameManager.Main.PracticedSword = false;
                GameManager.Main.crystals = GameManager.Main.crystals + 2;
                SetUsed(false);
            }
        }
        if (this.tag == "CrushingStrike")
        {
            if (!GameManager.Main.CrushingStrike && GameManager.Main.crystals >= 3)
            {
                GameManager.Main.crystals = GameManager.Main.crystals - 3;
                GameManager.Main.CrushingStrike = true;
                SetUsed(true);
            }
            else
            {
                GameManager.Main.CrushingStrike = false;
                GameManager.Main.crystals = GameManager.Main.crystals + 3;
                SetUsed(false);
            }
        }
        if (this.tag == "QuickEvasion")
        {
            if (!GameManager.Main.QuickEvasion && GameManager.Main.crystals >= 1)
            {
                GameManager.Main.crystals = GameManager.Main.crystals - 1;
                GameManager.Main.QuickEvasion = true;
                SetUsed(true);
            }
            else
            {
                GameManager.Main.QuickEvasion = false;
                GameManager.Main.crystals = GameManager.Main.crystals + 1;
                SetUsed(false);
            }
        }
        if (this.tag == "ProtectionofDivine")
        {
            if (!GameManager.Main.ProtectionofDivine && GameManager.Main.crystals >= 2)
            {
                GameManager.Main.crystals = GameManager.Main.crystals - 2;
                GameManager.Main.ProtectionofDivine = true;
                SetUsed(true);
            }
            else
            {
                GameManager.Main.ProtectionofDivine = false;
                GameManager.Main.crystals = GameManager.Main.crystals + 2;
                SetUsed(false);
            }
        }
        if (this.tag == "Thornmail")
        {
            if (!GameManager.Main.Thornmail && GameManager.Main.crystals >= 3)
            {
                GameManager.Main.crystals = GameManager.Main.crystals - 3;
                GameManager.Main.Thornmail = true;
                SetUsed(true);
            }
            else
            {
                GameManager.Main.Thornmail = false;
                GameManager.Main.crystals = GameManager.Main.crystals + 3;
                SetUsed(false);
            }
        }
    }

    public void SetUsed(bool used)
    {
        iconImage.sprite = used ? equippedSprite : nonequippedSprite;
    }
}
