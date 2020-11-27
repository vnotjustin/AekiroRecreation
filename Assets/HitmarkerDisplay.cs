using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AEK;
using TMPro;

public class HitmarkerDisplay : MonoBehaviour
{
    public static HitmarkerDisplay Main;

    private void Awake()
    {
        Main = this;
    }

    public TextMeshPro textbox;

    public AnimationCurve alphaMap;

    float timeLeft;
    float previousHealth;

    float newHealth;


    // Start is called before the first frame update
    void Start()
    {
        previousHealth = Enemy.Main.Life;
    }

    // Update is called once per frame
    void Update()
    {
        float alphaVal = Mathf.Clamp01(alphaMap.Evaluate(timeLeft));
        Color setColor = textbox.color;
        setColor.a = alphaVal;
        textbox.color = setColor;



        float currentHealth = Enemy.Main.Life;



        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
        }


    }

    public void TakeHit(float dmg)
    {
        int displayNum = Mathf.RoundToInt((dmg) * 5);
        textbox.text = displayNum.ToString();
        timeLeft = .75f;
    }
}
