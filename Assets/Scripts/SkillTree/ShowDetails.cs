using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShowDetails : MonoBehaviour
{
    public GameObject unlockButton;
    public Button otherB1;
    public Button otherB2;
    public Button otherB3;
    public Button otherB4;
    public Button otherB5;
    public Button otherB6;
    public Button otherB7;
    public Button otherB8;

    public TMP_Text title;
    public TMP_Text description;
    [Space]
    public Sprite equipSpr;
    public Sprite unequipSpr;
    public Image lockImage;

    public bool thisButton = false;
    // Start is called before the first frame update
    void Start()
    {
        otherB1.onClick.AddListener(OtherClicked);
        otherB2.onClick.AddListener(OtherClicked);
        otherB3.onClick.AddListener(OtherClicked);
        otherB4.onClick.AddListener(OtherClicked);
        otherB5.onClick.AddListener(OtherClicked);
        otherB6.onClick.AddListener(OtherClicked);
        otherB7.onClick.AddListener(OtherClicked);
        otherB8.onClick.AddListener(OtherClicked);
    }

    // Update is called once per frame
    void Update()
    {

        if(this.tag == "SwiftStrikes" && thisButton)
        {
            title.text = "Swift Slashes";
            description.text = "Slightly increases light attack damage. Cost 1 crystal.";
            lockImage.sprite = GameManager.Main.SwiftStrikes ? unequipSpr : equipSpr;
        }
        if (this.tag == "BlessingZhang" && thisButton)
        {
            title.text = "Thundercloud";
            description.text = "Every 5th light attack deals double damage. Costs 2 crystals.";
            lockImage.sprite = GameManager.Main.BlessingZhang ? unequipSpr : equipSpr;
        }
        if (this.tag == "SwordGuan" && thisButton)
        {
            title.text = "Blade Dance";
            description.text = "Consecutive strikes deal slightly more damage with each attack. Costs 3 crystals.";
            lockImage.sprite = GameManager.Main.SwordGuan ? unequipSpr : equipSpr;
        }
        if (this.tag == "StaggeringBlow" && thisButton)
        {
            title.text = "Searing Blow";
            description.text = "Heavy attacks now do burn damage for 2 seconds. Costs 1 crystal.";
            lockImage.sprite = GameManager.Main.StaggeringBlow ? unequipSpr : equipSpr;
        }
        if (this.tag == "PracticedSword" && thisButton)
        {
            title.text = "Cobra Bites";
            description.text = "All attacks can now critically strike. Costs 2 crystals.";
            lockImage.sprite = GameManager.Main.PracticedSword ? unequipSpr : equipSpr;
        }
        if (this.tag == "CrushingStrike" && thisButton)
        {
            title.text = "Crushing Strike";
            description.text = "Dodge Strike increases damage from all other attacks for 3 seconds. Costs 3 crystals.";
            lockImage.sprite = GameManager.Main.CrushingStrike ? unequipSpr : equipSpr;
        }
        if (this.tag == "QuickEvasion" && thisButton)
        {
            title.text = "Quick Evasion";
            description.text = "Increases the window of time for dodges.  Costs 1 crystal.";
            lockImage.sprite = GameManager.Main.QuickEvasion ? unequipSpr : equipSpr;
        }
        if (this.tag == "ProtectionofDivine" && thisButton)
        {
            title.text = "Divine Protection";
            description.text = "Start fight with shield that blocks first hit attack. Costs 2 crystals.";
            lockImage.sprite = GameManager.Main.ProtectionofDivine ? unequipSpr : equipSpr;
        }
        if (this.tag == "Thornmail" && thisButton)
        {
            title.text = "Second Wind";
            description.text = "Blocking 15 times without getting hit grants you a shield. Costs 3 crystals.";
            lockImage.sprite = GameManager.Main.Thornmail ? unequipSpr : equipSpr;
        }

        if (thisButton)
        {
            unlockButton.SetActive(true);
        }

        if (!thisButton)
        {
            unlockButton.SetActive(false);
        }
    }

    public void ShowUnlock()
    {
        thisButton = true; 
    }

    public void OtherClicked()
    {
        thisButton = false;
    }
}
