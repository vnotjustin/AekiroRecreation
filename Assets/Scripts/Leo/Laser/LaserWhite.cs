using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserWhite : MonoBehaviour
{
    void Update()
    {
        if (transform.parent.transform.localScale.x <= 1)
        {
            transform.localScale = new Vector3(0, 0, 1);
        }
        else if (transform.parent.transform.localScale.x >= 1)
        {
            transform.localScale = (1 - (1/(transform.parent.transform.localScale.x)))/2 * new Vector2(1, 1);
        }
    }
}
