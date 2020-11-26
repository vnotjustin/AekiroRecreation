using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBody : MonoBehaviour
{
    public GameObject LaserBase;
    public GameObject LaserTail;

    public Vector2 MyScale;
    public float Length;
    // Update is called once per frame
    void Update()
    {
        transform.right = LaserTail.transform.position - transform.position;
        transform.position = LaserBase.transform.position;
        //MyScale = new Vector2(Vector2.Distance(LaserBase.transform.position, LaserTail.transform.position), 1);
        Length = Vector2.Distance(LaserBase.transform.position, LaserTail.transform.position)*0.65f;
        //Length /= transform.lossyScale.y;
        transform.localScale = new Vector2(Length, LaserBase.transform.lossyScale.y);
    }
}
