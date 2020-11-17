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
            title.text = "Swift Strikes";
            description.text = "Light attacks now swipe twice.";
        }
        if (this.tag == "BlessingZhang" && thisButton)
        {
            title.text = "Blessing of Zhang Fei";
            description.text = "Every 5th light attack now becomes a heavy attack.";
        }
        if (this.tag == "SwordGuan" && thisButton)
        {
            title.text = "Sword of Guan Yu";
            description.text = "Increases light attack damage.";
        }
        if (this.tag == "StaggeringBlow" && thisButton)
        {
            title.text = "Staggering Blow";
            description.text = "Heavy attack now staggers the enemy.";
        }
        if (this.tag == "PracticedSword" && thisButton)
        {
            title.text = "Practiced Swordsman";
            description.text = "Charging becomes faster.";
        }
        if (this.tag == "CrushingStrike" && thisButton)
        {
            title.text = "Crushing Strike";
            description.text = "Increases heavy attack damage.";
        }
        if (this.tag == "QuickEvasion" && thisButton)
        {
            title.text = "Quick Evasion";
            description.text = "Slightly increase Dodge window.";
        }
        if (this.tag == "ProtectionofDivine" && thisButton)
        {
            title.text = "Protection of the Divine";
            description.text = "Start fight with shield that blocks first hit attack";
        }
        if (this.tag == "Thornmail" && thisButton)
        {
            title.text = "Thornmail";
            description.text = "Bosses take 10% of damage done to the player.";
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
