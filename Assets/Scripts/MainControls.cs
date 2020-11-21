﻿using System.Collections;
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

        public float chargedHit = 2;
        public float baseDamage = 1;

        public bool PCharge;
        public bool BSwing;
        public bool timeOnce = true;
        public bool liTrue = true;
        public bool isStrike = false;
        public bool isDeflecting = false;
        public bool reachedTime = false;
        public bool canDS = false;
        public bool canDam = false;
        public bool canHeavy = false;
        public bool canProt = false;

        public float chargeTime = 1;
        public float timeLeft;
        public float strikeTimer;
        public float dsTimer;
        public int strikeCounter;

        public float ogBD;
        public float ogCD;


        [Space]
        public Animator[] heartRenders;

        
       



        bool struckLeft; //Alternates light strikes based on what the last strike was. 

        bool inLightStrike;
        float inputDelay;
        float dodgeInputDelay;

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
            if (GameManager.Main.heavyUnlocked)
            {
                canHeavy = true;
            }

            pLife = maxPLife;
            m_Animator = gameObject.GetComponent<Animator>();
            timeLeft = chargeTime;

            if (GameManager.Main.ProtectionofDivine)
            {
                canProt = true;
            }
            if (GameManager.Main.SwiftStrikes)
            {
                baseDamage = 1.25f;
            }
            ogBD = baseDamage;
            ogCD = chargedHit;

        }

        // Update is called once per frame
        void Update()
        {
            strikeTimer -= Time.deltaTime;
            dsTimer -= Time.deltaTime;
            if (Input.GetKey(KeyCode.Space) && dsTimer < 0 && strikeTimer < 0 && GameManager.Main.CrushingStrike)
            {
                baseDamage = ogBD;
                chargedHit = ogCD;
            }

            else if (Input.GetKey(KeyCode.Space) == false && strikeTimer < 0 && GameManager.Main.SwordGuan)
            {
                baseDamage = ogBD;
                chargedHit = ogCD;
            }

           

            if (Input.GetKeyDown(KeyCode.X) || dodgeInputDelay>0)
            {
                if (!inLightStrike && !canDS)
                {
                    Dodge();
                }
                else if (Input.GetKeyDown(KeyCode.X))
                {
                    dodgeInputDelay = .25f;
                }
            }
            dodgeInputDelay -= Time.deltaTime;


            bool previousDodge = canDS;
            canDS = m_Animator.GetCurrentAnimatorStateInfo(0).IsName("Dodge");



            bool lightStrikeStore = inLightStrike;

            inLightStrike = m_Animator.GetCurrentAnimatorStateInfo(0).IsName("LightStrike") || m_Animator.GetCurrentAnimatorStateInfo(0).IsName("LightStrikeAlt");
            if (!lightStrikeStore && inLightStrike && GameManager.Main.SwordGuan)
            {
                print("DAMAGE" + Time.time);
                Enemy.Main.TakeDamage(baseDamage);
                strikeTimer = 1;

                baseDamage = baseDamage + (baseDamage * .05f);
            }

            else if(!lightStrikeStore && inLightStrike)
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

                else if (timeLeft < 0 && canHeavy)
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
                    strikeCounter++;

                    if (strikeCounter == 5 && GameManager.Main.BlessingZhang)
                    {
                        HeavyStrike();
                        strikeCounter = 0;
                    }
                    else
                    { 
                        LiStrike(); 
                    }

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
            if (!GameManager.Main.StaggeringBlow)
            {

                m_Animator.ResetTrigger("LightStrike");
                m_Animator.ResetTrigger("LightStrikeAlt");
                string triggerName = struckLeft ? "LightStrike" : "LightStrikeAlt";
                m_Animator.SetTrigger(triggerName);
                struckLeft = !struckLeft;

            }
            else
            {
                m_Animator.ResetTrigger("LightStrikeFast");
                m_Animator.ResetTrigger("LightStrikeAltFast");
                string triggerName = struckLeft ? "LightStrikeFast" : "LightStrikeAltFast";
                m_Animator.SetTrigger(triggerName);
                struckLeft = !struckLeft;
            }

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

            if (GameManager.Main.CrushingStrike)
            {
                baseDamage = 2f;
                chargedHit = 4f;
                dsTimer = 2;
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
            if (inLightStrike || canDS)
            {

            }
            else
            {
                Break();
            }
        }

        public void MustStasised() //Called when the enemy does a stasis attack on the player
        {
            if (inLightStrike)
            {

            }
            else if (canDS && !m_Animator.GetCurrentAnimatorStateInfo(0).IsName("DodgeStrike"))
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
            if (transform.position.x>9)
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
            if (canProt)
            {
                canProt = false;
            }

            else
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
        }

        public void LoseHeart()
        {
            heartRenders[pLife].SetTrigger("Break");
        }

    }
}
