using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxManager : MonoBehaviour
{

    public Transform[] spriteTransforms;
    [Space]
    public float unityUnitWidth;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        InitializePositions();
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < spriteTransforms.Length; i++)
        {
            Vector3 position = spriteTransforms[i].localPosition;
            position.x = (position.x + speed * Time.deltaTime) % (unityUnitWidth * (float)(spriteTransforms.Length));
            spriteTransforms[i].localPosition = position;
        }
    }

    void InitializePositions()
    {
        for (int i = 0; i < spriteTransforms.Length; i++)
        {
            Vector3 position = transform.position;
            position.x = i * unityUnitWidth;
            position.y = 0;
            spriteTransforms[i].localPosition = position;
        }
    }
}
