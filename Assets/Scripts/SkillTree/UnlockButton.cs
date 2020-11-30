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
        UpdateIconDisplay();
    }



    public void UnlockAbility()
    {
        if (this.tag == "SwiftStrikes")
        {
            if (!GameManager.Main.SwiftStrikes)
            {
                if (GameManager.Main.crystals >= 1)
                {
                    GameManager.Main.crystals = GameManager.Main.crystals - 1;
                    GameManager.Main.SwiftStrikes = true;
                    SetUsed(true);
                }
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
            if (!GameManager.Main.BlessingZhang)
            {
                if (GameManager.Main.crystals >= 2)
                {
                    GameManager.Main.crystals = GameManager.Main.crystals - 2;
                    GameManager.Main.BlessingZhang = true;
                    SetUsed(true);
                }
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
            if (!GameManager.Main.SwordGuan)
            {
                if (GameManager.Main.crystals >= 3)
                {
                    GameManager.Main.crystals = GameManager.Main.crystals - 3;
                    GameManager.Main.SwordGuan = true;
                    SetUsed(true);
                }
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
            if (!GameManager.Main.StaggeringBlow)
            {
                if (GameManager.Main.crystals >= 1)
                {
                    GameManager.Main.crystals = GameManager.Main.crystals - 1;
                    GameManager.Main.StaggeringBlow = true;
                    SetUsed(true);
                }
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
            if (!GameManager.Main.PracticedSword)
            {
                if (GameManager.Main.crystals >= 2)
                {
                    GameManager.Main.crystals = GameManager.Main.crystals - 2;
                    GameManager.Main.PracticedSword = true;
                    SetUsed(true);
                }
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
            if (!GameManager.Main.CrushingStrike)
            {
                if (GameManager.Main.crystals >= 3)
                {
                    GameManager.Main.crystals = GameManager.Main.crystals - 3;
                    GameManager.Main.CrushingStrike = true;
                    SetUsed(true);
                }
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
            if (!GameManager.Main.QuickEvasion)
            {
                if (GameManager.Main.crystals >= 1)
                {
                    GameManager.Main.crystals = GameManager.Main.crystals - 1;
                    GameManager.Main.QuickEvasion = true;
                    SetUsed(true);
                }
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
            if (!GameManager.Main.ProtectionofDivine)
            {
                if (GameManager.Main.crystals >= 2)
                {
                    GameManager.Main.crystals = GameManager.Main.crystals - 2;
                    GameManager.Main.ProtectionofDivine = true;
                    SetUsed(true);
                }
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
            if (!GameManager.Main.Thornmail)
            {
                if (GameManager.Main.crystals >= 3) {
                    GameManager.Main.crystals = GameManager.Main.crystals - 3;
                    GameManager.Main.Thornmail = true;
                    SetUsed(true);
                }
            }
            else
            {
                GameManager.Main.Thornmail = false;
                GameManager.Main.crystals = GameManager.Main.crystals + 3;
                SetUsed(false);
            }
        }
    }

    void UpdateIconDisplay()
    {
        if (this.tag == "SwiftStrikes")
        {
            SetUsed(GameManager.Main.SwiftStrikes);
        }
        if (this.tag == "BlessingZhang")
        {
            SetUsed(GameManager.Main.BlessingZhang);
        }
        if (this.tag == "SwordGuan")
        {
            SetUsed(GameManager.Main.SwordGuan);
        }
        if (this.tag == "StaggeringBlow")
        {
            SetUsed(GameManager.Main.StaggeringBlow);
        }
        if (this.tag == "PracticedSword")
        {
            SetUsed(GameManager.Main.PracticedSword);
        }
        if (this.tag == "CrushingStrike")
        {
            SetUsed(GameManager.Main.CrushingStrike);
        }
        if (this.tag == "QuickEvasion")
        {
            SetUsed(GameManager.Main.QuickEvasion);
        }
        if (this.tag == "ProtectionofDivine")
        {
            SetUsed(GameManager.Main.ProtectionofDivine);
        }
        if (this.tag == "Thornmail")
        {
            SetUsed(GameManager.Main.Thornmail);
        }
        if (this.tag == "Thornmail")
        {
            SetUsed(GameManager.Main.Thornmail);
        }
    }

    public void SetUsed(bool used)
    {
        iconImage.sprite = used ? equippedSprite : nonequippedSprite;
    }
}
