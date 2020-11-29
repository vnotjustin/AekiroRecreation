using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AEK 
{


    public class Boss2 : MonoBehaviour
    {
        public Animator MyAnimator;
        public float SlashLoopCount;
        public static Boss2 Main;

        public Boss2Head Head1;
        public Boss2Head Head2;
        public Boss2Head Head3;
        [Space]
        [Header("SFX")]
        public AudioClip laserCharge;
        public AudioClip laserBreak;
        public AudioClip laserFire;
        public AudioClip laserStasised;
        [Space]
        public AudioClip unblockableStrike;
        public AudioClip regStrike;
        public AudioClip shieldSetup;
        public AudioClip shieldBreak;

        public AudioSource audioSrc;
        public int phaseNo;

        private void Awake()
        {
            Main = this;
        }

        public void QuickAttackNode_Normal()
        {
            SFXManager.main.Play(regStrike, .7f, 1, .1f, .1f);
            MainControls.Main.Damaged();
        }

        public void QuickAttackNode_Focus()
        {
            MainControls.Main.Break();
            if (MyAnimator.GetBool("Lasering"))
            {
                SFXManager.main.Play(laserFire, .7f, 1, .1f, .05f);
            }
            else
            {
                SFXManager.main.Play(laserFire, .7f, 1, .1f, .05f);
            }
            
        }

        public void QuickAttackNode_Stasis()
        {
            MainControls.Main.Stasised();
            if (MyAnimator.GetBool("Lasering"))
            {
                SFXManager.main.Play(laserStasised, .7f, 1, .1f, .05f);
            }
        }

        public void QuickAttack_Unblockable()
        {
            MainControls.Main.AttackedByUnblockable();
            if (MyAnimator.GetBool("Lasering"))
            {
                SFXManager.main.Play(laserFire, .7f, 1, .1f, .05f);
            }
            else
            {
                SFXManager.main.Play(unblockableStrike, .8f, 1, .1f, .05f);
            }
        }

        public void QuickAttack_HitsDodge()
        {
            MainControls.Main.AttackedAtDodgePosition();
        }

        public void QuickAttack_MustStasis()
        {
            MainControls.Main.MustStasised();
        }

        void Start()
        {
            Enemy.Main.Life = 200;
        }

        // Update is called once per frame
        void Update()
        {
            if (Enemy.Main.Life > 180)
            {
                phaseNo = 1;
                MyAnimator.SetInteger("Phase", 1);
            }
            else if (Enemy.Main.Life > 130)
            {
                MyAnimator.SetInteger("Phase", 2);
                phaseNo = 2;
            }
            else if (Enemy.Main.Life > 90)
            {
                MyAnimator.SetInteger("Phase", 3);
                phaseNo = 3;
            }
            else 
            {
                MyAnimator.SetInteger("Phase", 4);
                phaseNo = 4;
            }
        }

        public void PickAnATK()
        {
                if (MyAnimator.GetInteger("Loop") == 1) 
                {
                    MyAnimator.SetInteger("Loop",2);
                }
                else
                {
                    MyAnimator.SetInteger("Loop",1);
                }
        }

        public void ShieldOrSlash() 
        {
            if (MyAnimator.GetInteger("Phase") >= 3)
            {
                SlashLoopCount++;
                if (SlashLoopCount >= 5)
                {
                    MyAnimator.SetTrigger("Fin");
                    SlashLoopCount = 0;
                }
                if (Random.Range(0f, 4f) > 3)
                {
                    MyAnimator.SetBool("Shield", true);
                }
                else
                {
                    MyAnimator.SetBool("Shield", false);
                }
            }
            else if (MyAnimator.GetInteger("Phase") == 2) 
            {
                    MyAnimator.SetTrigger("Fin");
                    SlashLoopCount = 0;
            }
        }

        public void ShieldReset() 
        {
            MyAnimator.SetBool("Shield", false);
        }

        public void Shielding() 
        {
            MyAnimator.SetBool("Shielding", true);
            SFXManager.main.Play(shieldSetup, .7f, 1, .1f, .05f);
            Debug.Log("Shield sat true!");
        }
        public void Unshielding()
        {
            if (MyAnimator.GetBool("Shielding"))
            {
                SFXManager.main.Play(shieldBreak, .7f, 1, .1f, .05f);
            }
            MyAnimator.SetBool("Shielding", false);
            
        }
        public void Lasering() 
        {
            MyAnimator.SetBool("Lasering", true);
            PlaySound(audioSrc, laserCharge, .7f, 1, .1f, .05f);
        }
        public void UnLasering()
        {
            MyAnimator.SetBool("Lasering", false);
           
        }
        public void Tranformed() 
        {
            MyAnimator.SetBool("Transformed", true);
        }
        public void CompareHeadsChargedTime()
        {
            float H1 = Head1.ChargeTimer;
            float H2 = Head2.ChargeTimer;
            float H3 = Head3.ChargeTimer;

            if (Head1 != null) 
            {
                if (Mathf.Max(H1, H2, H3) == H1 && H1 != 0)
                {
                    Head1.MyAnimator.SetTrigger("Struck");
                }
                else if (Mathf.Max(H1, H2, H3) == H2)
                {
                    Head2.MyAnimator.SetTrigger("Struck");
                }
                else
                {
                    Head3.MyAnimator.SetTrigger("Struck");
                }
            }
        }

        public void LaserBroke()
        {
           if (MyAnimator.GetBool("Lasering"))
            {
                audioSrc.Stop();
                SFXManager.main.Play(laserBreak, .8f, 1, .1f, .04f);
            }
            else
            {
                SFXManager.main.Play(laserBreak, .8f, 1, .1f, .04f);
            }
        }



        public void PlaySound(AudioSource src, AudioClip clip, float vol, float pitch, float volVar, float pitchVar)
        {
            src.volume = vol + Random.Range(-volVar, volVar);
            src.pitch = pitch + Random.Range(-pitchVar, pitchVar);
            src.clip = clip;
            src.Play();
        }
    }

}

