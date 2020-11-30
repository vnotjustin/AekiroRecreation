using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AEK
{
    public class CameraControl : MonoBehaviour
    {

        public float Speed;
        Vector3 targetPos;

        bool inShake;
        float amplitude;
        float shakeTimeLeft;

        private void Start()
        {
            targetPos = transform.position;
        }

        // Update is called once per frame
        void Update()
        {
            targetPos.x = Mathf.Lerp(targetPos.x, (MainControls.Main.transform.position.x + Enemy.Main.transform.position.x) / 2f, Speed * Time.deltaTime);
            transform.position = targetPos;

            if (shakeTimeLeft > 0)
            {
                transform.position = targetPos + (Vector3)Random.insideUnitCircle * amplitude;
                shakeTimeLeft -= Time.deltaTime;
            }
        }

        public void SetScreenshake(float _amplitude, float _duration)
        {
            inShake = true;
            amplitude = _amplitude;
            shakeTimeLeft = _duration;
        }


    }
}