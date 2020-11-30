using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    public Sprite[] Sprite;
    public SpriteRenderer MyRenderer;
    public Color Mycolor;
    public Color32 MyColor32;
    public float timer;
    public Rigidbody2D MyRB;

    void Start()
    {
        MyRB.AddForce(new Vector2 (Random.Range(-1f,1f),Random.Range(2f,4f))*100);
        MyRenderer.sprite = Sprite[Random.Range(0, Sprite.Length)];
        Mycolor = Color.white;
        transform.eulerAngles = new Vector3(0,0, Random.Range(0, 359));
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 3)
        {
            Destroy(this.gameObject);
        }
        if (timer > 1.5) 
        {
            Mycolor = new Color(1,1,1,1-(1/1.5f)*(timer-1.5f));
            Debug.Log(Mycolor.a);
        }
        MyColor32 = Mycolor;
        MyRenderer.color = MyColor32;
    }

}
