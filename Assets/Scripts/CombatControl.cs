using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

namespace AEK
{
    public class CombatControl : MonoBehaviour {
        public static CombatControl Main;
        public AnimationCurve SlideCurve;
        public float MaxSlideTime;
        public TextMeshPro LifeText_MC;
        public TextMeshPro LifeText_Enemy;
        public TextMeshPro PhaseText;
        public float DeathProtectedTime;
        public float StartProtectedTime = 1f;
        public bool Finished;
        public bool AlreadyStarted;
        public bool Victoried;
        [Space]
        public float CurrentTime;
        public TextMeshPro TimeText;
        public TextMeshPro DamageText;
        [Space]
        public Animator VeryStartBase;
        public Animator SceneFadeOut;
        public Animator VictoryAnim;
        public Animator DefeatAnim;
        public Animator Tutorial1;
        public Animator Tutorial2;
        public Animator Tutorial3;
        public Animator Tutorial4;
        [Space]
        public GameObject HitSound;
        public GameObject DeflectSound;
        public GameObject BreakSound;
        public AudioClip HitClip;
        public AudioClip DeflectClip;
        public AudioClip BreakClip;
        public AudioClip IgnoreClip;
        public AudioSource Source;

        float hideTimer;

        public void Awake()
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
            if (hideTimer >= 0)
            {
                hideTimer += Time.deltaTime;
                if (hideTimer > 4)
                {
                    hideTimer = 0;
                    VeryStartBase.SetTrigger("Play");
                    Tutorial1.SetTrigger("Play");
                }
            }
            /*float a = MC.Main.Life;
            if (a > 0)
                LifeText_MC.text = a.ToString();
            else
                LifeText_MC.text = "0";

            float b = Enemy.Main.Life;
            if (b > 0)
                LifeText_Enemy.text = b.ToString();
            else
                LifeText_Enemy.text = "0";

            int c = Enemy.Main.Phase;
            if (c >= 4)
                PhaseText.text = "0";
            else if (c == 3)
                PhaseText.text = "1";
            else if (c == 2)
                PhaseText.text = "2";
            else if (c == 1)
                PhaseText.text = "3";*/

            if (DeathProtectedTime <= 0)
            {
                if (Input.GetKeyDown(KeyCode.Space) && Finished)
                    Restart();
            }
            else
                DeathProtectedTime -= Time.deltaTime;

            StartProtectedTime -= Time.deltaTime;

            if (Input.GetKeyDown(KeyCode.Escape))
                Application.Quit();

            if (AlreadyStarted && !Victoried)
                CurrentTime += Time.deltaTime;

            int a = (int)CurrentTime / 60;
            int b = (int)CurrentTime % 60;
            TimeText.text = "Time - " + a + ":" + b;
            DamageText.text = "Damage Taken - " + (7 - MainControls.Main.pLife);
        }

        public void StartGame()
        {
            AlreadyStarted = true;
            Enemy.Main.StartGame();
            VeryStartBase.SetTrigger("Play");

        }

        public void Restart()
        {
            DeathProtectedTime = 9999f;
            SceneFadeOut.SetTrigger("Play");
            StartCoroutine("RestartIE");
        }

        public IEnumerator RestartIE()
        {
            yield return new WaitForSeconds(2f);
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("SampleScene");
        }

        public void Defeat()
        {
            StartCoroutine("DefeatIE");
        }

        public IEnumerator DefeatIE()
        {
            yield return new WaitForSeconds(4f);
            TransitionManager.main.TransitionToScene(0);
        }

        public void Victory()
        {
            Victoried = true;
            DeathProtectedTime = 34;
            VictoryAnim.SetTrigger("Play");
        }

        public void PlaySound(string Key)
        {
            /*GameObject P = null;
            if (Key == "Deflect")
                P = DeflectSound;
            else if (Key == "Hit")
                P = HitSound;
            else if (Key == "Break")
                P = BreakSound;
            GameObject G = Instantiate(P);
            G.transform.position = transform.position;*/

            AudioClip C = null;
            if (Key == "Deflect")
                C = DeflectClip;
            else if (Key == "Hit")
                C = HitClip;
            else if (Key == "Break")
                C = BreakClip;
            else if (Key == "Ignore")
                C = IgnoreClip;
            if (C)
                Source.PlayOneShot(C);
        }
    }
}