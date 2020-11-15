using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AEK
{
    public class World : MonoBehaviour {
        public float Cycle;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            transform.localPosition = new Vector3(GetPosition(), transform.localPosition.y, transform.localPosition.z);
        }

        private void FixedUpdate()
        {
            transform.localPosition = new Vector3(GetPosition(), transform.localPosition.y, transform.localPosition.z);
        }

        public float GetPosition()
        {
            float a = -Camera.main.transform.position.x;
            while (a >= Cycle)
                a -= Cycle;
            while (a <= -Cycle)
                a += Cycle;
            return a;
        }
    }
}