using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AEK
{
    public class MainControls : MonoBehaviour
    {
        public static MainControls Main;
        Animator m_Animator;

        public int pLife;
        public int chargedHit = 2;

        public bool PCharge;
        public bool BSwing;
        public bool timeOnce = true;
        public bool liTrue = true;
        public bool isStrike = false;
        public bool isDeflecting = false;
        public bool reachedTime = false;

        public float chargeTime = 1;
        public float timeLeft;


        private void Awake()
        {
            Main = this;
        }

        // Start is called before the first frame update
        void Start()
        {
            pLife = 7;
            m_Animator = gameObject.GetComponent<Animator>();
            timeLeft = chargeTime;
        }

        // Update is called once per frame
        void Update()
        {

            if (Input.GetKeyDown(KeyCode.X) && !isStrike)
            {
                Dodge();
            }

            if (Input.GetKey(KeyCode.Space) && !reachedTime)
            {
                
                PCharge = true;
                timeLeft -= Time.deltaTime;

                if (timeLeft > 0)
                {
                    liTrue = true;
                }

                else if (timeLeft < 0)
                {
                    reachedTime = true;
                    liTrue = false;
                    timeLeft = chargeTime;
                    HeavyStrike();
                    PCharge = false;
                    isStrike = true;
                }

            }

            if (Input.GetKeyUp(KeyCode.Space))
            {
                reachedTime = false;
                PCharge = false;
                isStrike = true;
                if (liTrue)
                {
                    timeLeft = chargeTime;
                    LiStrike();
                }

            }
        }

        public void ClearAni()
        {
            m_Animator.SetBool("LightStrike", false);
            m_Animator.SetBool("HeavyStrike", false);
            m_Animator.SetBool("Block", false);
            m_Animator.SetBool("Dodge", false);
            isStrike = false;
        }

        public void LiStrike()
        {
            m_Animator.SetBool("LightStrike", true);
            if (!BSwing)
            {
                Enemy.Main.TakeDamage(1);
                Debug.Log("Light Strike");
            }
            isStrike = false;

        }

        public void HeavyStrike()
        {
            m_Animator.SetBool("HeavyStrike", true);
            if (!BSwing)
            {
                Enemy.Main.TakeDamage(2);
                Debug.Log("Heavy Strike");
            }
            isStrike = false;

        }

        public void Damaged()
        {
            if (CanDeflect())
                Deflect();
            else
                Break();
        }

        public bool CanDeflect()
        {
            if (isStrike)
                return false;
            if (PCharge)
                return false;
            if (pLife <= 0)
                return false;
            return true;
        }

        public void Stasised() //Called when the enemy does a stasis attack on the player
        {

        }

        public void AttackedByUnblockable() //Called when the enemy attacks with an unblockable (only dodgable) attack
        {

        }

        public void AttackedAtDodgePosition() //Called when the enemy does an attack that will hurt the player if it is backwards(in dodge), but nothing if still at the front.
        {

        }

        public void Deflect()
        {
            m_Animator.SetBool("Block", true);
        }

        public void Dodge()
        {
            m_Animator.SetBool("Dodge", true);
        }

        public void Break()
        {
            pLife--;
            if (pLife >= 0)
            {
                //dead
            }
            m_Animator.ResetTrigger("LightStrike");
            m_Animator.ResetTrigger("HeavyStrike");
            m_Animator.ResetTrigger("Block");
            m_Animator.ResetTrigger("Dodge");
        }
    }
}
