using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AEK
{
    public class StartScene : MonoBehaviour {
        public float ProtectedTime;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            ProtectedTime -= Time.deltaTime;
            if (ProtectedTime <= 0)
                Next();
        }

        public void Next()
        {
            ProtectedTime = 9999f;
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("SampleScene");
        }
    }
}