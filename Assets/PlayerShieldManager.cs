using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using AEK;
public class PlayerShieldManager : MonoBehaviour
{
    Material shieldMat;
    float shieldVal;

    MeshRenderer mr;

    private void Start()
    {
        mr = GetComponent<MeshRenderer>();
        shieldMat = mr.material;
    }

    void Update()
    {
        if (MainControls.Main.canProt)
        {
            shieldVal = Mathf.Lerp(shieldVal, .3f, .03f);
        }
        else
        {
            shieldVal = Mathf.Lerp(shieldVal, 0, .03f);
        }

        shieldMat.SetFloat("_TransitionValue", shieldVal);
    }
}
