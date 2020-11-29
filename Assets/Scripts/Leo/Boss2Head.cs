using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AEK
{
    public class Boss2Head : MonoBehaviour
    {
        public Animator MyAnimator;
        public float timer;
        public float ChargeTimer;
        public bool IsMiddleHead;

        public void QuickAttack_Normal()
        {
            Boss2.Main.QuickAttackNode_Normal();
        }
        public void QuickAttack_Focus()
        {
            Boss2.Main.QuickAttackNode_Focus();
        }

        public void QuicAttack_HitDodge()
        {
            Boss2.Main.QuickAttack_HitsDodge();
        }

        public void SelectATK() 
        {
            MyAnimator.SetInteger("ATKType", Random.Range(1,3));
        }

        void Start()
        {
            if (IsMiddleHead) 
            {
                MyAnimator.SetBool("IsMiddleHead", true);
            }
        }

        void Update()
        {
            if (ChargeTimer > 0)
            {
                ChargeTimer += Time.deltaTime; 
            }

            timer += Time.deltaTime;
            MyAnimator.SetFloat("Timer", timer);
            if (timer >= 5.1)
            {
                timer = 0;
                MyAnimator.SetTrigger("Attack");
            }
            else 
            {
                timer += Time.deltaTime;
            }

        }

        public void Charging() 
        {
            ChargeTimer += Time.deltaTime;
        }
        public void ChargeEnd() 
        {
            ChargeTimer = 0;
        }
        public void CancelStruck() 
        {
            MyAnimator.ResetTrigger("Attack");
        }

    }
}