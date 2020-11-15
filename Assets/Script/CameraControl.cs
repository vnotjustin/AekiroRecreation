using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AEK
{
    public class CameraControl : MonoBehaviour {
        public float Speed;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            float a = Mathf.Lerp(transform.position.x, (MC.Main.transform.position.x + Enemy.Main.transform.position.x) / 2f, Speed * Time.deltaTime);
            transform.position = new Vector3(a, transform.position.y, transform.position.z);
        }
    }
}