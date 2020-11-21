using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashMarker : MonoBehaviour
{

    private float timer;
    public int i;
    public float InitalTime;
    public Sprite[] sprites;
    public SpriteRenderer SR;

    public int SlashType;
    //0 = light
    //1 = dodge
    //2 = heavy

    void Start()
    {
        timer = 0;
        i = 0;
        if (SlashType == 0) 
        {
            transform.eulerAngles = new Vector3(0, 0, Random.Range(-15f, 15f));
            transform.position += new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);
        }
        if (SlashType == 1)
        { 
             
        }
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= InitalTime / sprites.Length) 
        {
            i += 1;
            if (i < sprites.Length)
            {
                timer = 0;
                SR.sprite = sprites[i];
            }
            else 
            {
                Destroy(this.gameObject);
            }
        }
        
    }
}
