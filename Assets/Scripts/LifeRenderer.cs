using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AEK
{
    public class LifeRenderer : MonoBehaviour {
        public GameObject Mask;
        public float MaxScale;
        public float Value;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            Mask.transform.localScale = new Vector3(Mask.transform.localScale.x, MaxScale * (1 - Value), 1);
        }
    }
}