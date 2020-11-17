using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CrystalCount : MonoBehaviour
{
    public TMP_Text crystalsT;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        crystalsT.text = GameManager.Main.crystals.ToString();
    }
}
