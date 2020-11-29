using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AEK;

public class ClosingRing : MonoBehaviour
{
    public Sprite[] Sprites;
    public SpriteRenderer MyRenderer;
    public SpriteRenderer srTwo;

    Vector3 defPos;


    float chargeTime;

    void Start()
    {
        MyRenderer.sprite = Sprites[0];
        chargeTime = MainControls.Main.chargeTime;
        defPos = transform.position;
    }
    void Update()
    {
        float tLeft = MainControls.Main.timeLeft;
        if (tLeft < chargeTime-.1f && tLeft>0)
        {
            float pcnt = 1 - (tLeft / chargeTime);
            int sprNo = Mathf.Clamp(Mathf.FloorToInt(pcnt * (float)Sprites.Length),0,11);
            MyRenderer.sprite = Sprites[sprNo];

            int secondarySprNo = Mathf.Clamp(sprNo + 1, 0, 11);
            srTwo.sprite = Sprites[secondarySprNo];

            srTwo.enabled = true;
            MyRenderer.enabled = true;
            transform.position = defPos;
        }
        else if (MainControls.Main.reachedTime)
        {
            MyRenderer.sprite = Sprites[11];
            srTwo.sprite = Sprites[11];
            transform.position = defPos + (Vector3)Random.insideUnitCircle * .3f;
        }
        else
        {
            MyRenderer.enabled = false;
            srTwo.enabled = false;
        }

    }
}
