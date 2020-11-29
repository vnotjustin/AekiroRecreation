using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosingRing : MonoBehaviour
{
    public Sprite[] Sprites;
    public SpriteRenderer MyRenderer;
    public float Lifetime;
    private float timer;
    public int i;

    void Start()
    {
        MyRenderer.sprite = Sprites[0];
    }
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > Lifetime / Sprites.Length)
        {
            if (i < Sprites.Length)
            { }
        }

    }
}
