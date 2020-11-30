using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTail : MonoBehaviour
{
    public GameObject LaserHead;

    private void Update()
    {
        transform.localScale = new Vector3
                                (
                                    LaserHead.transform.localScale.x * 2,
                                    LaserHead.transform.localScale.y * 2,
                                    1
                                );
    }
}
