using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AEK
{
    public class SoundtrackControl : MonoBehaviour {
        public static SoundtrackControl Main;
        public GameObject Soundtrack;
        public GameObject CurrentObject;
        public double LoopRate;
        public double LastLoopTime;
        [Space]
        public GameObject Endtrack;
        public bool Ended;

        // Start is called before the first frame update
        void Start()
        {
            if (!Main)
            {
                Main = this;
                DontDestroyOnLoad(gameObject);
                StartCoroutine("Process");
            }
            else if (Main.Ended)
            {
                Destroy(Main.gameObject);
                Main = this;
                DontDestroyOnLoad(gameObject);
                StartCoroutine("Process");
            }
            else
            {
                Destroy(gameObject);
            }
        }

        // Update is called once per frame
        void Update()
        {

        }

        public IEnumerator Process()
        {
            GameObject G = Instantiate(Soundtrack, transform);
            Destroy(G, 300f);
            CurrentObject = G;
            LastLoopTime = AudioSettings.dspTime;
            while (!Ended)
            {
                if (AudioSettings.dspTime >= LastLoopTime + LoopRate)
                {
                    GameObject G2 = Instantiate(Soundtrack, transform);
                    Destroy(G2, 180f);
                    LastLoopTime = AudioSettings.dspTime;
                }
                yield return 0;
            }
        }

        public void End()
        {
            Ended = true;
            if (CurrentObject)
                Destroy(CurrentObject);
            GameObject G = Instantiate(Endtrack, transform);
            CurrentObject = G;
        }
    }
}