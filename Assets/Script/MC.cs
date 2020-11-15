using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AEK
{
    public class MC : MonoBehaviour {
        public static MC Main;
        public Animator Anim;
        public float DamageScale = 1f;
        public float Life;
        public List<Animator> Hearts;
        [Space]
        public Vector2 FocusRange;
        public float DelayedMaxFocus;
        public float StrikeFocusLock;
        public float StrikeDeflectLock;
        public float DeflectFocusLock;
        public float BreakFocusLock;
        public float AfterStrikeLock;
        [Space]
        public bool Focusing;
        public float CurrentFocusTime;
        public bool AlreadyStartedFocus;
        public bool AlreadyStoppedFocus;
        public bool AlreadyConfirmed;
        public float FocusCoolDown;
        public float DeflectCoolDown;
        public float AfterStrikeTime;
        [Space]
        public float Range;
        public float FocusedRange;
        public float DeflectRange;
        public Vector3 StrikeForce;
        public Vector3 FocusedStrikeForce;
        public Vector3 DeflectForce;
        public Vector3 BreakForce;
        public Vector3 IgnoreForce;
        [Space]
        public bool Sliding;
        public float SlidePoint;
        public float SlideTarget;
        public float SlideTime;
        [Space]
        public float ShadeRange;
        public GameObject ShadePrefab;
        public List<GameObject> ShadeTargets;
        [Space]
        public GameObject DamagedPrefab;
        public GameObject DamagePoint;
        public float CurrentTimeStop;
        public float CurrentSlow;
        [HideInInspector]
        public bool AlreadyDead;

        private void Awake()
        {
            Main = this;
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (AlreadyStartedFocus && CanFocus())
                StartFocus();

            if (Focusing)
                CurrentFocusTime += Time.deltaTime;

            if (KeyUp())
                AlreadyStoppedFocus = true;

            if (CurrentFocusTime >= FocusRange.y)
            {
                if (!AlreadyConfirmed)
                {
                    Anim.SetTrigger("Confirm");
                    AlreadyConfirmed = true;
                }
            }
            else
                AlreadyConfirmed = false;

            if ((AlreadyStoppedFocus || CurrentFocusTime >= FocusRange.y) && CanStopFocus())
                StopFocus();

            if (KeyDown())
                AlreadyStartedFocus = true;

            if (FocusCoolDown > 0)
                FocusCoolDown -= Time.deltaTime;

            if (DeflectCoolDown > 0)
                DeflectCoolDown -= Time.deltaTime;

            if (AfterStrikeTime > 0)
                AfterStrikeTime -= Time.deltaTime;

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

            if (CurrentTimeStop > 0)
                CurrentTimeStop -= Time.unscaledDeltaTime;

            if (CurrentTimeStop > 0)
                Time.timeScale = 0;
            else if (CurrentSlow > 0.1f)
            {
                CurrentSlow -= Time.unscaledDeltaTime;
                Time.timeScale = 0.3f;
            }
            else if (CurrentSlow > 0)
            {
                CurrentSlow -= Time.unscaledDeltaTime;
                Time.timeScale = 0.6f;
            }
            else
                Time.timeScale = 1;
        }

        public void StartFocus()
        {
            Focusing = true;
            AlreadyStartedFocus = false;
            AlreadyStoppedFocus = false;
            CurrentFocusTime = 0;
            Anim.SetTrigger("Focus");
        }

        public void StopFocus()
        {
            Focusing = false;
            AlreadyStoppedFocus = false;

            if (CurrentFocusTime < FocusRange.y)
                Strike();
            else
                FocusedStrike();
        }

        public bool CanFocus()
        {
            if (Focusing)
                return false;
            if (FocusCoolDown > 0)
                return false;
            if (Life <= 0)
                return false;
            return true;
        }

        public bool CanStopFocus()
        {
            if (!Focusing)
                return false;
            if (CurrentFocusTime < FocusRange.x)
                return false;
            if (CurrentFocusTime >= FocusRange.y && CurrentFocusTime < DelayedMaxFocus)
                return false;
            return true;
        }

        public void Strike()
        {
            StartCoroutine("StrikeIE");
            AlreadyStartedFocus = false;
            FocusCoolDown = StrikeFocusLock;
            DeflectCoolDown = StrikeDeflectLock;
            AfterStrikeTime = AfterStrikeLock;
            AddForce(StrikeForce, Range);
            Enemy.Main.TakeDamage(1f * DamageScale);
            Anim.SetTrigger("Strike");
            CombatControl.Main.PlaySound("Hit");
        }

        public IEnumerator StrikeIE()
        {
            yield return 0;
        }

        public void FocusedStrike()
        {
            AlreadyStartedFocus = false;
            FocusCoolDown = StrikeFocusLock;
            DeflectCoolDown = StrikeDeflectLock;
            AfterStrikeTime = AfterStrikeLock;
            AddForce(FocusedStrikeForce, FocusedRange);
            Enemy.Main.AttemptBreak();
            Enemy.Main.TakeDamage(3f * DamageScale);
            Anim.SetTrigger("StrikeII");
            CombatControl.Main.PlaySound("Hit");
        }

        public void AddForce(Vector3 Force, float range)
        {
            float a = Enemy.Main.transform.position.x - Force.x;
            if (Mathf.Abs(transform.position.x - a) >= ShadeRange)
            {
                foreach (GameObject G in ShadeTargets)
                {
                    GameObject G1 = Instantiate(ShadePrefab);
                    G1.transform.position = G.transform.position;
                    G1.transform.eulerAngles = G.transform.eulerAngles;
                    Destroy(G1, 5);
                }
            }
            transform.position = new Vector3(a, transform.position.y, transform.position.z);
            Sliding = true;
            SlideTime = 0;
            SlidePoint = transform.position.x;
            SlideTarget = transform.position.x - Force.y;

            Enemy.Main.transform.Translate(-Force.x - range, 0, 0);
            Enemy.Main.Sliding = true;
            Enemy.Main.SlideTime = 0;
            Enemy.Main.SlidePoint = Enemy.Main.transform.position.x;
            Enemy.Main.SlideTarget = Enemy.Main.transform.position.x - Force.z;
        }

        public void TakeForce(Vector3 Force, bool Fixed)
        {
            Enemy.Main.transform.position = new Vector3(transform.position.x + Force.x, Enemy.Main.transform.position.y, Enemy.Main.transform.position.z);
            Enemy.Main.Sliding = true;
            Enemy.Main.SlideTime = 0;
            Enemy.Main.SlidePoint = Enemy.Main.transform.position.x;
            Enemy.Main.SlideTarget = Enemy.Main.transform.position.x + Force.y;

            if (Fixed)
            {
                transform.Translate(Force.x + DeflectRange - 3, 0, 0);
                Sliding = true;
                SlideTime = 0;
                SlidePoint = transform.position.x;
                SlideTarget = transform.position.x + Force.z + 3;
            }
            else
            {
                transform.Translate(Force.x + DeflectRange, 0, 0);
                Sliding = true;
                SlideTime = 0;
                SlidePoint = transform.position.x;
                SlideTarget = transform.position.x + Force.z;
            }
        }

        public void Damaged()
        {
            if (CanDeflect())
                Deflect();
            else
                Break();
        }

        public void Stasised()
        {
            if (IsMoving())
                Ignore();
            else
                Break();
        }

        public bool CanDeflect()
        {
            if (Focusing)
                return false;
            if (DeflectCoolDown > 0)
                return false;
            if (Life <= 0)
                return false;
            return true;
        }

        public void Deflect()
        {
            AlreadyStartedFocus = false;
            FocusCoolDown = DeflectFocusLock;
            TakeForce(DeflectForce, false);
            Anim.SetTrigger("Deflect");
            CombatControl.Main.PlaySound("Deflect");

            Anim.ResetTrigger("Strike");
            Anim.ResetTrigger("Focus");
            Anim.ResetTrigger("StrikeII");
            Anim.ResetTrigger("Confirm");
            if (Focusing)
            {
                Focusing = false;
                AlreadyStoppedFocus = false;
            }
        }

        public void Break()
        {
            Life--;
            if (Life >= 0)
                Hearts[(int)Life].SetTrigger("Break");
            CombatControl.Main.PlaySound("Break");

            GameObject G = Instantiate(DamagedPrefab);
            G.transform.position = DamagePoint.transform.position;
            Destroy(G, 5);
            if (Life > 0)
            {
                CurrentTimeStop = 0.15f;
                AlreadyStartedFocus = false;
                FocusCoolDown = BreakFocusLock;
                TakeForce(BreakForce, true);
                Anim.SetTrigger("Break");
            }
            else
            {
                if (!AlreadyDead)
                    Death();
            }

            Anim.ResetTrigger("Strike");
            Anim.ResetTrigger("Focus");
            Anim.ResetTrigger("StrikeII");
            Anim.ResetTrigger("Confirm");
            if (Focusing)
            {
                Focusing = false;
                AlreadyStoppedFocus = false;
            }
        }

        public void Ignore()
        {
            //TakeForce(IgnoreForce, false);
            CombatControl.Main.PlaySound("Ignore");
        }

        public bool IsMoving()
        {
            return (Focusing && CurrentFocusTime < FocusRange.x + 0.03f) || AfterStrikeTime > 0;
        }

        public bool KeyDown()
        {
            if (CombatControl.Main.Finished)
                return false;
            if (CombatControl.Main.StartProtectedTime > 0)
                return false;
            bool a = Input.GetKeyDown(KeyCode.Space);
            if (a && !CombatControl.Main.AlreadyStarted)
                CombatControl.Main.StartGame();
            return a;
            //return Input.GetMouseButtonDown(0);
        }

        public bool KeyUp()
        {
            return !Input.GetKey(KeyCode.Space);
            //return !Input.GetMouseButton(0);
        }

        public void TimeStop()
        {

        }

        public void Death()
        {
            AlreadyDead = true;
            Anim.SetTrigger("Death");
            CurrentTimeStop = 0.1f;
            CurrentSlow = 0.5f;
            CombatControl.Main.Finished = true;
            CombatControl.Main.DeathProtectedTime = 999f;
            CombatControl.Main.DefeatAnim.SetTrigger("Play");
            CombatControl.Main.Defeat();
        }
    }
}