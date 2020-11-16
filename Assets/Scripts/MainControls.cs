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
        public int baseDamage = 1;

        public bool PCharge;
        public bool BSwing;
        public bool timeOnce = true;
        public bool liTrue = true;
        public bool isStrike = false;
        public bool isDeflecting = false;
        public bool reachedTime = false;
        public bool canDS = false;
        public bool canDam = false;

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

            if (canDS)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    DodgeStrike();
                }
            }

            if (Input.GetKey(KeyCode.Space) && !reachedTime && !canDS && !isStrike)
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
                }

            }

            if (Input.GetKeyUp(KeyCode.Space) && !canDS && !isStrike)
            {
                reachedTime = false;
                PCharge = false;
                if (liTrue && !isStrike)
                {
                    timeLeft = chargeTime;
                    LiStrike();
                }

            }
        }

        public void StartStrike()
        {
            isStrike = true;
        }

        public void EndStrike()
        {
            isStrike = false;
            canDam = true;
        }

        public void LiStrike()
        {
            m_Animator.ResetTrigger("LightStrike");
            m_Animator.SetTrigger("LightStrike");

            if (!BSwing && canDam)
            {
                Enemy.Main.TakeDamage(baseDamage);
                Debug.Log("Light Strike");
                canDam = false;
            }

        }

        public void HeavyStrike()
        {
            m_Animator.ResetTrigger("HeavyStrike");
            m_Animator.SetTrigger("HeavyStrike");

            if (!BSwing)
            {
                Enemy.Main.TakeDamage(chargedHit);
                Debug.Log("Heavy Strike");
            }

        }

        public void DodgeStrike()
        {
            m_Animator.ResetTrigger("DodgeStrike");
            m_Animator.SetTrigger("DodgeStrike");

            if (!BSwing)
            {
                Enemy.Main.TakeDamage(baseDamage);
                Debug.Log("Dodge Strike");
            }
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
            m_Animator.ResetTrigger("Block");
            m_Animator.SetTrigger("Block");
        }

        public void Dodge()
        {
            m_Animator.ResetTrigger("Dodge");
            m_Animator.SetTrigger("Dodge");
            canDS = true;
        }

        public void Break()
        {
            pLife--;
            m_Animator.ResetTrigger("Hit");
            m_Animator.SetTrigger("Hit");
            if (pLife <= 0)
            {
                CombatControl.Main.Finished = true;
                CombatControl.Main.DeathProtectedTime = 999f;
                CombatControl.Main.DefeatAnim.SetTrigger("Play");
                //dead
            }
            m_Animator.ResetTrigger("LightStrike");
            m_Animator.ResetTrigger("HeavyStrike");
            m_Animator.ResetTrigger("Block");
            m_Animator.ResetTrigger("Dodge");
            m_Animator.ResetTrigger("DodgeStrike");
        }
    }
}
