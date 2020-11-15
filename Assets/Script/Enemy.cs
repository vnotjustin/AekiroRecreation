using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AEK
{
    public class Enemy : MonoBehaviour {
        public static Enemy Main;
        public Animator Anim;
        [Space]
        public float Life;
        public float Phase1Life;
        public float Phase2Life;
        public float Phase3Life;
        public float Phase4Life;
        public List<LifeRenderer> LRs;
        [Space]
        public int Phase;
        public float Breaking;
        [Space]
        public bool Sliding;
        public float SlidePoint;
        public float SlideTarget;
        public float SlideTime;
        [HideInInspector]
        public bool AlreadyDead;

        public void Awake()
        {
            Main = this;
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        public void StartGame()
        {
            StartCoroutine("Process");
        }

        // Update is called once per frame
        void Update()
        {
            if (Sliding)
            {
                SlideTime += Time.deltaTime;
                if (SlideTime > 1)
                {
                    SlideTime = 1;
                    Sliding = false;
                }
                float x = SlidePoint + (SlideTarget - SlidePoint) * CombatControl.Main.SlideCurve.Evaluate(SlideTime / CombatControl.Main.MaxSlideTime);
                transform.position = new Vector3(x, transform.position.y, transform.position.z);
            }

            if (Phase == 1)
            {
                if (Life >= Phase1Life * 0.5f)
                {
                    LRs[6].Value = (Life - Phase1Life * 0.5f) / (Phase1Life * 0.5f);
                    LRs[5].Value = 1;
                }
                else if (Life > 0)
                {
                    LRs[6].Value = 0;
                    LRs[5].Value = Life / (Phase1Life * 0.5f);
                }
                else
                {
                    LRs[6].Value = 0;
                    LRs[5].Value = 0;
                }
            }
            else if (Phase == 2)
            {
                if (Life >= Phase2Life * 0.5f)
                {
                    LRs[4].Value = (Life - Phase2Life * 0.5f) / (Phase2Life * 0.5f);
                    LRs[3].Value = 1;
                }
                else if (Life > 0)
                {
                    LRs[4].Value = 0;
                    LRs[3].Value = Life / (Phase2Life * 0.5f);
                }
                else
                {
                    LRs[4].Value = 0;
                    LRs[3].Value = 0;
                }
            }
            else if (Phase == 3)
            {
                if (Life >= Phase3Life * 0.5f)
                {
                    LRs[2].Value = (Life - Phase3Life * 0.5f) / (Phase3Life * 0.5f);
                    LRs[1].Value = 1;
                }
                else if (Life > 0)
                {
                    LRs[2].Value = 0;
                    LRs[1].Value = Life / (Phase3Life * 0.5f);
                }
                else
                {
                    LRs[2].Value = 0;
                    LRs[1].Value = 0;
                }
            }
            else
            {
                if (Life > 0)
                    LRs[0].Value = Life / Phase4Life;
                else
                    LRs[0].Value = 0;
            }

            if ((Phase == 4 && Life <= 0) || MC.Main.Life <= 0)
            {
                StopAllCoroutines();
                if (Life <= 0 && !AlreadyDead)
                    Death();
            }
        }

        public void LateUpdate()
        {
            Breaking -= Time.deltaTime;
        }

        public void TakeDamage(float Value)
        {
            Life -= Value;
        }

        public void AttemptBreak()
        {
            Breaking = 0.1f;
        }

        public IEnumerator Process()
        {
            yield return new WaitForSeconds(3f);
            CombatControl.Main.Tutorial1.SetTrigger("Play");
            yield return new WaitForSeconds(3f);
            yield return StandardCombo();
            yield return new WaitForSeconds(2f);
            yield return DoubleStrike();
            int Last = 2;
            while (true)
            {
                if (Phase == 1)
                {
                    if (Life <= 0)
                    {
                        yield return PhaseChange2();
                        continue;
                    }

                    yield return new WaitForSeconds(2f);
                    if (Last == 2)
                    {
                        yield return StandardCombo();
                        Last = 1;
                    }
                    else
                    {
                        if (Random.Range(-0.99f,0.99f) >= 0)
                        {
                            yield return StandardCombo();
                            Last = 1;
                        }
                        else
                        {
                            yield return DoubleStrike();
                            Last = 2;
                        }
                    }
                }
                else if (Phase == 2)
                {
                    if (Life <= 0)
                    {
                        yield return PhaseChange3();
                        continue;
                    }

                    yield return new WaitForSeconds(2f);
                    if (Last == 2)
                    {
                        if (Random.Range(-0.99f, 0.99f) >= 0)
                        {
                            yield return StandardCombo();
                            Last = 1;
                        }
                        else
                        {
                            yield return FocusedStrike();
                            Last = 3;
                        }
                    }
                    else if (Last == 3)
                    {
                        if (Random.Range(-0.99f, 0.99f) >= 0)
                        {
                            yield return StandardCombo();
                            Last = 1;
                        }
                        else
                        {
                            yield return DoubleStrike();
                            Last = 2;
                        }
                    }
                    else
                    {
                        float a = Random.Range(0.01f, 0.99f);
                        if (a >= 0.86f)
                        {
                            yield return StandardCombo();
                            Last = 1;
                        }
                        else if (a >= 0.43f)
                        {
                            yield return DoubleStrike();
                            Last = 2;
                        }
                        else
                        {
                            yield return FocusedStrike();
                            Last = 3;
                        }
                    }
                }
                else if (Phase == 3)
                {
                    if (Life <= 0)
                    {
                        yield return PhaseChange4();
                        continue;
                    }

                    yield return new WaitForSeconds(0.75f);
                    if (Last == 2)
                    {
                        if (Random.Range(-0.99f, 0.99f) <= 0.33f)
                        {
                            yield return StandardCombo();
                            Last = 1;
                        }
                        else
                        {
                            yield return FocusedStrike();
                            Last = 3;
                        }
                    }
                    else if (Last == 3)
                    {
                        if (Random.Range(-0.99f, 0.99f) >= 0)
                        {
                            yield return StandardCombo();
                            Last = 1;
                        }
                        else
                        {
                            yield return DoubleStrike();
                            Last = 2;
                        }
                    }
                    else
                    {
                        float a = Random.Range(0.01f, 0.99f);
                        if (a >= 0.86f)
                        {
                            yield return StandardCombo();
                            Last = 1;
                        }
                        else if (a >= 0.3f)
                        {
                            yield return DoubleStrike();
                            Last = 2;
                        }
                        else
                        {
                            yield return FocusedStrike();
                            Last = 3;
                        }
                    }
                }
                else if (Phase == 4)
                {
                    yield return new WaitForSeconds(0.25f);
                    float a = Random.Range(0.01f, 0.99f);
                    if (a >= 0.67f)
                    {
                        yield return P4A();
                        Last = 1;
                    }
                    else if (a >= 0.33f)
                    {
                        yield return P4A_Stasis();
                        Last = 2;
                    }
                    else
                    {
                        yield return P4A_Focused();
                        Last = 3;
                    }
                }
            }
        }

        public IEnumerator PhaseChange2()
        {
            yield return new WaitForSeconds(3f);
            Anim.SetTrigger("PC2");
            yield return new WaitForSeconds(1.25f);
            CombatControl.Main.Tutorial2.SetTrigger("Play");
            Life = Phase2Life;
            Phase = 2;
            yield return new WaitForSeconds(2.25f);

            yield return new WaitForSeconds(2.5f);
            yield return FocusedStrike();
        }

        public IEnumerator PhaseChange3()
        {
            yield return new WaitForSeconds(3f);
            Anim.SetTrigger("PC3");
            yield return new WaitForSeconds(1.25f);
            CombatControl.Main.Tutorial3.SetTrigger("Play");
            Life = Phase3Life;
            Phase = 3;
            yield return new WaitForSeconds(2.25f);

            yield return new WaitForSeconds(2.5f);
            yield return SC_S1();
        }

        public IEnumerator PhaseChange4()
        {
            yield return new WaitForSeconds(3f);
            Anim.SetTrigger("PC4");
            yield return new WaitForSeconds(1.25f);
            Life = Phase4Life;
            Phase = 4;
            yield return new WaitForSeconds(1.25f);
            yield return new WaitForSeconds(1.5f);
            yield return P4A();
        }

        public IEnumerator StandardCombo()
        {
            print("SC Start");
            if (Phase == 1)
                yield return SC_Basic();
            else if (Phase == 2)
            {
                if (Random.Range(-0.99f, 0.99f) >= 0f)
                    yield return SC_F3();
                else
                    yield return SC_Add();
            }
            else if (Phase == 3)
            {
                float a = Random.Range(0.01f, 0.99f);
                if (a >= 0.9f)
                    yield return SC_F3();
                else if (a >= 0.75f)
                    yield return SC_S1();
                else if (a >= 0.6f)
                    yield return SC_S2();
                else if (a >= 0.45f)
                    yield return SC_S3();
                else if (a >= 0.3f)
                    yield return SC_S4();
                else if (a >= 0.15f)
                    yield return SC_S5();
                else
                    yield return SC_S6();
            }
        }

        public IEnumerator SC_Basic()
        {
            Anim.SetTrigger("StandardCombo");
            yield return new WaitForSeconds(1f);
            MC.Main.Damaged();
            yield return new WaitForSeconds(1.5f);
            MC.Main.Damaged();
            yield return new WaitForSeconds(1.5f);
            MC.Main.Damaged();
            yield return new WaitForSeconds(1f);
            print("SC_Basic End");
        }

        public IEnumerator SC_F3()
        {
            Anim.SetTrigger("SC_F3");
            yield return new WaitForSeconds(1f);
            MC.Main.Damaged();
            yield return new WaitForSeconds(1.5f);
            MC.Main.Damaged();
            yield return new WaitForSeconds(0.75f);
            float a = 0f;
            while (a < 1.75f && Breaking <= 0)
            {
                a += Time.deltaTime;
                yield return 0;
            }
            if (Breaking > 0)
            {
                Anim.SetTrigger("Break");
                yield return new WaitForSeconds(0.5f);
            }
            else
            {
                MC.Main.Break();
                yield return new WaitForSeconds(1f);
            }
            print("SC_F3 End");
        }

        public IEnumerator SC_Add()
        {
            Anim.SetTrigger("SC_Add");
            yield return new WaitForSeconds(1f);
            MC.Main.Damaged();
            yield return new WaitForSeconds(1.5f);
            MC.Main.Damaged();
            yield return new WaitForSeconds(1.5f);
            MC.Main.Damaged();
            yield return new WaitForSeconds(0.5f);
            MC.Main.Damaged();
            yield return new WaitForSeconds(1f);
            print("SC_Add End");
        }

        public IEnumerator SC_S1()
        {
            Anim.SetTrigger("SC_S1");
            yield return new WaitForSeconds(1f);
            MC.Main.Stasised();
            yield return new WaitForSeconds(1.5f);
            MC.Main.Damaged();
            yield return new WaitForSeconds(1.5f);
            MC.Main.Stasised();
            yield return new WaitForSeconds(1f);
            print("SC_S1 End");
        }

        public IEnumerator SC_S2()
        {
            Anim.SetTrigger("SC_S2");
            yield return new WaitForSeconds(1f);
            MC.Main.Stasised();
            yield return new WaitForSeconds(1.5f);
            MC.Main.Damaged();
            yield return new WaitForSeconds(0.75f);
            float a = 0f;
            while (a < 1.75f && Breaking <= 0)
            {
                a += Time.deltaTime;
                yield return 0;
            }
            if (Breaking > 0)
            {
                Anim.SetTrigger("Break");
                yield return new WaitForSeconds(0.5f);
            }
            else
            {
                MC.Main.Break();
                yield return new WaitForSeconds(1f);
            }
            print("SC_S2 End");
        }

        public IEnumerator SC_S3()
        {
            Anim.SetTrigger("SC_S3");
            yield return new WaitForSeconds(1f);
            MC.Main.Damaged();
            yield return new WaitForSeconds(1.5f);
            MC.Main.Stasised();
            yield return new WaitForSeconds(0.75f);
            float a = 0f;
            while (a < 1.75f && Breaking <= 0)
            {
                a += Time.deltaTime;
                yield return 0;
            }
            if (Breaking > 0)
            {
                Anim.SetTrigger("Break");
                yield return new WaitForSeconds(0.5f);
            }
            else
            {
                MC.Main.Break();
                yield return new WaitForSeconds(1f);
            }
            print("SC_S3 End");
        }

        public IEnumerator SC_S4()
        {
            Anim.SetTrigger("SC_S4");
            yield return new WaitForSeconds(1f);
            MC.Main.Damaged();
            yield return new WaitForSeconds(1.5f);
            MC.Main.Stasised();
            yield return new WaitForSeconds(1.5f);
            MC.Main.Damaged();
            yield return new WaitForSeconds(0.5f);
            MC.Main.Stasised();
            yield return new WaitForSeconds(1f);
            print("SC_S4 End");
        }

        public IEnumerator SC_S5()
        {
            Anim.SetTrigger("SC_S5");
            yield return new WaitForSeconds(1f);
            MC.Main.Stasised();
            yield return new WaitForSeconds(1.5f);
            MC.Main.Damaged();
            yield return new WaitForSeconds(1.5f);
            MC.Main.Damaged();
            yield return new WaitForSeconds(0.5f);
            MC.Main.Stasised();
            yield return new WaitForSeconds(1f);
            print("SC_S5 End");
        }

        public IEnumerator SC_S6()
        {
            Anim.SetTrigger("SC_S6");
            yield return new WaitForSeconds(1f);
            MC.Main.Stasised();
            yield return new WaitForSeconds(1.5f);
            MC.Main.Damaged();
            yield return new WaitForSeconds(1.5f);
            MC.Main.Damaged();
            yield return new WaitForSeconds(0.5f);
            MC.Main.Damaged();
            yield return new WaitForSeconds(1f);
            print("SC_S6 End");
        }

        public IEnumerator DoubleStrike()
        {
            print("DS Start");
            if (Phase != 3)
                yield return DS_Basic();
            else
            {
                float a = Random.Range(0.01f, 0.99f);
                if (a >= 0.67f)
                    yield return DS_S1();
                else if (a >= 0.33f)
                    yield return DS_S2();
                else
                    yield return DS_Basic();
            }
        }

        public IEnumerator DS_Basic()
        {
            Anim.SetTrigger("DoubleStrike");
            yield return new WaitForSeconds(1f);
            MC.Main.Damaged();
            yield return new WaitForSeconds(0.75f);
            MC.Main.Damaged();
            yield return new WaitForSeconds(1.25f);
            print("DS_Standard End");
        }

        public IEnumerator DS_S1()
        {
            Anim.SetTrigger("DS_S1");
            yield return new WaitForSeconds(1f);
            MC.Main.Stasised();
            yield return new WaitForSeconds(0.75f);
            MC.Main.Damaged();
            yield return new WaitForSeconds(1.25f);
            print("DS_S1 End");
        }

        public IEnumerator DS_S2()
        {
            Anim.SetTrigger("DS_S2");
            yield return new WaitForSeconds(1f);
            MC.Main.Damaged();
            yield return new WaitForSeconds(0.75f);
            MC.Main.Stasised();
            yield return new WaitForSeconds(1.25f);
            print("DS_S2 End");
        }

        public IEnumerator DS_S3()
        {
            Anim.SetTrigger("DS_S3");
            yield return new WaitForSeconds(1f);
            MC.Main.Stasised();
            yield return new WaitForSeconds(0.75f);
            MC.Main.Stasised();
            yield return new WaitForSeconds(1.25f);
            print("DS_S3 End");
        }

        public IEnumerator FocusedStrike()
        {
            print("FocusedStrike Start");
            Anim.SetTrigger("FocusedStrike");
            yield return new WaitForSeconds(0.5f);
            float a = 0f;
            while (a < 1.75f && Breaking <= 0)
            {
                a += Time.deltaTime;
                yield return 0;
            }
            if (Breaking > 0)
            {
                Anim.SetTrigger("Break");
                yield return new WaitForSeconds(0.5f);
            }
            else
            {
                MC.Main.Break();
                yield return new WaitForSeconds(1f);
            }
            print("FocusedStrike End");
        }

        public IEnumerator P4A()
        {
            print("P4A Start");
            Anim.SetTrigger("P4A");
            yield return new WaitForSeconds(0.6f);
            MC.Main.Damaged();
            yield return new WaitForSeconds(0.3f);
            print("P4A End");
        }

        public IEnumerator P4A_Stasis()
        {
            print("P4A_Stasis Start");
            Anim.SetTrigger("P4A_Stasis");
            yield return new WaitForSeconds(0.6f);
            MC.Main.Stasised();
            yield return new WaitForSeconds(0.3f);
            print("P4A_Stasis End");
        }

        public IEnumerator P4A_Focused()
        {
            print("P4A_Focused Start");
            Anim.SetTrigger("P4A_Focused");
            float a = 0f;
            bool Broke = false;
            while (a < 1.5f)
            {
                if (Breaking > 0 && !Broke)
                {
                    Broke = true;
                    Anim.SetTrigger("Break");
                }
                a += Time.deltaTime;
                yield return 0;
            }
            if (!Broke)
            {
                MC.Main.Break();
            }
            yield return new WaitForSeconds(0.3f);
            print("P4A_Focused End");
        }

        public void Death()
        {
            AlreadyDead = true;
            MC.Main.CurrentTimeStop = 0.1f;
            MC.Main.CurrentSlow = 0.5f;
            Anim.SetTrigger("Death");
            CombatControl.Main.Finished = true;
            CombatControl.Main.Victory();
            SoundtrackControl.Main.End();
        }
    }
}