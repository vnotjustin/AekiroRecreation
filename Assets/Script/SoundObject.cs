using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AEK
{
    public class SoundObject : MonoBehaviour {
        public AudioSource Source;

        // Start is called before the first frame update
        void Awake()
        {
            Source.Play();
            Destroy(gameObject, 5);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}