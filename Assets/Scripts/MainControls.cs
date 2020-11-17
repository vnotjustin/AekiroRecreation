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
        [Tooltip("For use with PlayerStatistics, can give more or less health shown on the bottom UI bar")]
        public int maxPLife; 

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
        [Space]
        public Animator[] heartRenders;
       



        bool struckLeft; //Alternates light strikes based on what the last strike was. 

        bool inLightStrike;
        float inputDelay;

        [Space]
        [Header("Audio")]
        public AudioClip hitByEnemyClip;
        public AudioClip blockedAttackClip;
        public AudioClip dodgeClip;
        [Header("Juice")]
        public ParticleSystem blockParticles;


        private void Awake()
        {
            Main = this;
        }

        // Start is called before the first frame update
        void Start()
        {
            pLife = maxPLife;
            m_Animator = gameObject.GetComponent<Animator>();
            timeLeft = chargeTime;



        }

        // Update is called once per frame
        void Update()
        {

            if (Input.GetKeyDown(KeyCode.X) && !inLightStrike && !canDS)
            {
                Dodge();
            }


            bool previousDodge = canDS;
            canDS = m_Animator.GetCurrentAnimatorStateInfo(0).IsName("Dodge");



            bool lightStrikeStore = inLightStrike;

            inLightStrike = m_Animator.GetCurrentAnimatorStateInfo(0).IsName("LightStrike") || m_Animator.GetCurrentAnimatorStateInfo(0).IsName("LightStrikeAlt");
            if (!lightStrikeStore && inLightStrike)
            {
                print("DAMAGE" + Time.time);
                Enemy.Main.TakeDamage(baseDamage);
            }


            if (canDS)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    DodgeStrike();
                }
            }

            if (Input.GetKey(KeyCode.Space) && !reachedTime && !canDS && !inLightStrike)
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

            if ((Input.GetKeyUp(KeyCode.Space) || inputDelay>0) && !canDS && !inLightStrike)
            {
                reachedTime = false;
                PCharge = false;
                if (liTrue && !inLightStrike)
                {
                    timeLeft = chargeTime;
                    LiStrike();
                }

            }

            if (Input.GetKeyUp(KeyCode.Space) && inLightStrike)
            {
                inputDelay = .1f;
            }



            inputDelay -= Time.deltaTime;
        }

        public void StartStrike()
        {
            isStrike = true;
        }

        public void EndStrike()
        {
            isStrike = false;
        }

        public void LiStrike()
        {
            m_Animator.ResetTrigger("LightStrike");
            m_Animator.ResetTrigger("LightStrikeAlt");
            string triggerName = struckLeft ? "LightStrike" : "LightStrikeAlt";
            m_Animator.SetTrigger(triggerName);
            struckLeft = !struckLeft;

            //if (!BSwing && canDam)
            //{
            //    Enemy.Main.TakeDamage(baseDamage);
            //    Debug.Log("Light Strike");
            //    canDam = false;
            //}

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
            {
                if (!canDS)
                {
                    Deflect();
                }
            }
            else Break();
        }

        public bool CanDeflect()
        {
            if (inLightStrike)
                return false;
            if (PCharge)
                return false;
            if (pLife <= 0)
                return false;
            return true;
        }

        public void Stasised() //Called when the enemy does a stasis attack on the player
        {
            if (isStrike || canDS)
            {

            }
            else
            {
                Break();
            }
        }

        public void AttackedByUnblockable() //Called when the enemy attacks with an unblockable (only dodgable) attack
        {
            if (!canDS)
            {
                Break();
            }
        }

        public void AttackedAtDodgePosition() //Called when the enemy does an attack that will hurt the player if it is backwards(in dodge), but nothing if still at the front.
        {
            if (canDS)
            {
                Break();
            }
        }

        public void Deflect()
        {
            m_Animator.ResetTrigger("Block");
            m_Animator.SetTrigger("Block");

            if (blockParticles.isPlaying)
            {
                blockParticles.Stop();
                blockParticles.Clear();
            }
            blockParticles.Play();


            SFXManager.main.Play(blockedAttackClip, .6f, 1, .1f, .12f);
        }

        public void Dodge()
        {
            m_Animator.ResetTrigger("Dodge");
            m_Animator.SetTrigger("Dodge");

            SFXManager.main.Play(dodgeClip, .7f, 1, .1f, 0);
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

            SFXManager.main.Play(hitByEnemyClip, .7f, 1, 0, .07f);
            LoseHeart();
        }

        public void LoseHeart()
        {
            heartRenders[pLife].SetTrigger("Break");
        }

    }
}
