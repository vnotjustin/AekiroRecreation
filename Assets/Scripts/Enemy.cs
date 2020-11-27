using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AEK
{
    public class Enemy : MonoBehaviour {
        public static Enemy Main;
        public bool isBossOne;


        public enum AttackType
        {
            Normal,
            Focused,
            Stasis,
            Unblockable, //This attack can only be dodged.
            HitsDodge, //This attack will hurt the player if its currently fallen back in the dodge.
            MustStasis
        }

        public Animator Anim;
        public Vector2 positionOffset;
        [Space]
        public float Life;
        float totalLife;

        public List<LifeRenderer> LRs;
        [Space]
        [Header("Phase/Attack Info")]
        public Phase[] phases;
        [Space]
        public int currentPhaseIndex;
            int previousAttackIndex; //previous index in a phase that was used.\
        [Space]
        public EnemyAttackGroup[] allAttackGroups;
        [HideInInspector]
        public List<EnemyAttack> allPossibleAttacks;
        [Space(15)]
        public float Breaking;
        [Space]
        public bool Sliding;
        public float SlidePoint;
        public float SlideTarget;
        public float SlideTime;
        [HideInInspector]
        public bool AlreadyDead;

        public int Phase;
        [Space]
        [Header("Audio")]
        public AudioClip chargeSound;
        public AudioClip chargeSoundBig;
        [Space]
        public AudioClip normalAttackClip;
        public AudioClip requiredDodgeAttackClip;
        public AudioClip stasisAttackClip;
        [Space]
        public AudioClip[] hitSounds;


        public void Awake()
        {
            Main = this;
        }

        // Start is called before the first frame update
        void Start()
        {
            currentPhaseIndex = 0;
            totalLife = Life;
            StartGame();
        }

        public void StartGame()
        {
            allPossibleAttacks = GetAllPossibleAttacks(0);
            StartCoroutine(Process());
            
        }

        // Update is called once per frame
        void Update()
        {

            transform.localPosition = positionOffset;
            //if (Sliding)
            //{
            //    SlideTime += Time.deltaTime;
            //    if (SlideTime > 1)
            //    {
            //        SlideTime = 1;
            //        Sliding = false;
            //    }
            //    float x = SlidePoint + (SlideTarget - SlidePoint) * CombatControl.Main.SlideCurve.Evaluate(SlideTime / CombatControl.Main.MaxSlideTime);
            //    transform.position = new Vector3(x, transform.position.y, transform.position.z);
            //}

            #region Update Life
            float lifePerHeart = totalLife / 7;
            int numOfFilledHearts = Mathf.FloorToInt(Life / lifePerHeart);
            for (int i = 0; i < 7; i++)
            {
                if (i <= numOfFilledHearts - 1)
                {
                    LRs[i].Value = 1;
                }
                else if (i==numOfFilledHearts)
                {
                    LRs[i].Value = (Life % lifePerHeart) / lifePerHeart;
                }
                else
                {
                    LRs[i].Value = 0;
                }
            }

            #endregion

            //Death
            if ((Phase == 4 && Life <= 0) || MainControls.Main.pLife <= 0)
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
            PlayHitSound();
        }

        public void AttemptBreak()
        {
            Breaking = 0.1f;
        }

        public void HeavyStruck() 
        {
            Anim.SetTrigger("HeavyStruck");
        }

        public IEnumerator Process()
        {
            //yield return ChangePhase(0);
            bool inGame = true;
            while (inGame)
            {
                print("IN GAME");

                if (currentPhaseIndex < phases.Length - 1)
                {
                    float lifeToChange = phases[currentPhaseIndex + 1].lifeToTrigger;
                    if (Life < lifeToChange)
                    {
                        print("change phase");
                        currentPhaseIndex++;
                        allPossibleAttacks = GetAllPossibleAttacks(currentPhaseIndex);
                        
                        yield return ChangePhase(currentPhaseIndex);
                        continue;
                    }
                }

                yield return new WaitForSeconds(2f);
                print("done waiting");
                if (isBossOne && currentPhaseIndex == 3)
                {
                    SpikeManager.Main.StartSpikes();
                    while (Life > 0)
                    {
                        yield return new WaitForEndOfFrame();
                    }
                    StopCoroutine(Process());
                }
                EnemyAttack newAttack = PickAttack();
                yield return StartAttack(newAttack);
                yield return new WaitForEndOfFrame();
            }
        }



        public IEnumerator StartAttack(EnemyAttack attackType)
        {
            print(attackType.animationName + " Start");
            Anim.SetTrigger(attackType.animationName);
            float t = 0;
            bool Broke = false;

            int currentAttackIndex = 0;

            AudioClip chargeClip = null;
            if (attackType.hasChargeSound)
            {
                chargeClip = chargeSound;
            }
            if (attackType.hasSpecialChargeSound)
            {
                chargeClip = chargeSoundBig;
            }
            if (chargeClip != null)
            {
                SFXManager.main.Play(chargeClip, .7f, 1, 0, .1f);
            }

            while (currentAttackIndex < attackType.attackNodes.Length && !Broke)
            {
                AttackNode currentAttack = attackType.attackNodes[currentAttackIndex];

                //Charging Up
                while (t < currentAttack.hitRegisterDelay && !Broke)
                {
                    Debug.Log(Broke);
                    switch (currentAttack.attackType)
                    {
                        case AttackType.Focused:
                            if (Breaking > 0 && !Broke)
                            {
                                Broke = true;
                                Anim.SetTrigger("Break");
                            }
                            break;
                        case AttackType.Normal:
                            break;
                    }


                    t += Time.deltaTime;
                    yield return new WaitForEndOfFrame();
                }



                AudioClip targetClip = null;
                //Attack
                switch (currentAttack.attackType)
                {
                    case AttackType.Normal:
                        MainControls.Main.Damaged();
                        targetClip = normalAttackClip;
                        break;
                    case AttackType.Focused:
                        if (!Broke)
                        {
                            MainControls.Main.Break();
                        }
                        break;
                    case AttackType.Stasis:
                        MainControls.Main.Stasised();
                        targetClip = stasisAttackClip;
                        break;
                    case AttackType.Unblockable:
                        MainControls.Main.AttackedByUnblockable();
                        targetClip = requiredDodgeAttackClip;
                        break;
                    case AttackType.HitsDodge:
                        MainControls.Main.AttackedAtDodgePosition();
                        break;
                    case AttackType.MustStasis:
                        MainControls.Main.MustStasised();
                        targetClip = stasisAttackClip;
                        break;
                }

                if (targetClip != null && currentAttack.usesSound)
                {
                    SFXManager.main.Play(targetClip, .9f, 1, .2f, .12f);
                }

                currentAttackIndex++;
            }

            yield return new WaitForSeconds(attackType.postAttackCooldown);

        }
        EnemyAttack PickAttack()
        {
            float randomRange = TotalCommonValue(allPossibleAttacks);
            float randomVal = Random.Range(0, randomRange);
            int possibleAttack = GetEnemyAttackIndexFromRandomVal(randomVal, allPossibleAttacks);


            //If same attack as last time, choose a new one.

            while (possibleAttack == previousAttackIndex && allPossibleAttacks.Count>1)
            {
                randomVal = Random.Range(0, randomRange);
                possibleAttack = GetEnemyAttackIndexFromRandomVal(randomVal, allPossibleAttacks);
            }

            previousAttackIndex = possibleAttack;
            return allPossibleAttacks[possibleAttack];
        }

        //Returns the sum of all "commonality" values, so that PickAttack() can randomize in that range and pick an attack.
        float TotalCommonValue(List<EnemyAttack> enemyAttacks)
        {
            float returnVal = 0;
            foreach (EnemyAttack attack in enemyAttacks)
            {
                returnVal += attack.commonness;
            }
            return returnVal;
        }

        //Takes a random value received between 0-TotalCommonValue() and routes that to an index in the possible attack list. 
        int GetEnemyAttackIndexFromRandomVal(float val, List<EnemyAttack> attackList)
        {
            float currentScannedVal = 0;
            for (int i = 0; i < attackList.Count-1; i++)
            {
                if (val > currentScannedVal)
                {
                    currentScannedVal += attackList[i].commonness;
                    if (val < currentScannedVal)
                    {
                        return i;
                    }
                }
                else
                {
                    currentScannedVal += attackList[i].commonness;
                }
            }

            //Failsafe
            return Random.Range(0, attackList.Count);
        }

        List<EnemyAttack> GetAllPossibleAttacks(int phaseNo)
        {
            List<EnemyAttack> outputList = new List<EnemyAttack>();
            string[] allAttackGroupNames = phases[phaseNo].allAttackGroups;
            for (int i = 0; i < allAttackGroupNames.Length; i++)
            {
                EnemyAttackGroup thisEnemyAttackGroup = GetAttackGroupFromName(allAttackGroupNames[i]);

                //If an attack group with this name exists, then add all the EnemyAttacks inside it to the list.
                if (thisEnemyAttackGroup.groupName.Length > 0)
                {
                    for (int j = 0; j < thisEnemyAttackGroup.attacksInGroup.Length; j++)
                    {
                        outputList.Add(thisEnemyAttackGroup.attacksInGroup[j]);
                    }
                }
            }

            return outputList;
        }

        EnemyAttackGroup GetAttackGroupFromName(string groupName)
        {
            for (int i = 0; i < allAttackGroups.Length; i++)
            {
                if (allAttackGroups[i].groupName.Equals(groupName))
                {
                    return allAttackGroups[i];
                }
            }
            return new EnemyAttackGroup();
        }


        public IEnumerator ChangePhase(int newPhaseIndex)
        {
            PhaseChange phaseChange = phases[newPhaseIndex].phaseChange;


            if (!phaseChange.animationName.Equals(""))
            {
                yield return new WaitForSeconds(phaseChange.timeBeforeAnimate);
                Anim.SetTrigger(phaseChange.animationName);
            }
            yield return new WaitForSeconds(phaseChange.delayTilTutorial);

            SetTutorial(phaseChange.tutorialType);
            currentPhaseIndex = newPhaseIndex;

            yield return new WaitForSeconds(phaseChange.delayTilFirstAttack);
            if (currentPhaseIndex != 3)
            {
                yield return StartAttack(phaseChange.firstAttackInPhase);
            }
        }

        public void Death()
        {
            AlreadyDead = true;
           // MainControls.Main.CurrentTimeStop = 0.1f;
            //MainControls.Main.CurrentSlow = 0.5f;
            Anim.SetTrigger("Death");
            CombatControl.Main.Finished = true;
            CombatControl.Main.Victory();
            //SoundtrackControl.Main.End();
        }

        public void SetTutorial(PhaseChange.TutorialType tutorialType)
        {
            switch (tutorialType)
            {
                case PhaseChange.TutorialType.None:
                    break;
                case PhaseChange.TutorialType.NormalAttack:
                    CombatControl.Main.Tutorial1.SetTrigger("Play");
                    break;
                case PhaseChange.TutorialType.FocusedAttack:
                    CombatControl.Main.Tutorial2.SetTrigger("Play");
                    break;
                case PhaseChange.TutorialType.StasisAttack:
                    CombatControl.Main.Tutorial3.SetTrigger("Play");
                    break;
                case PhaseChange.TutorialType.Dodge:
                    CombatControl.Main.Tutorial4.SetTrigger("Play");
                    break;
            }
        }



        public void PlayHitSound()
        {
            AudioClip clipToPlay = hitSounds[Random.Range(0, hitSounds.Length)];
            SFXManager.main.Play(clipToPlay, .6f, 1, .2f, .1f);
        }
    }


    [System.Serializable]
    public struct EnemyAttackGroup
    {
        [Tooltip("The name of the group of attacks referenced by a phase to decide moves")]
        public string groupName;
        [Space]
        public EnemyAttack[] attacksInGroup;

    }

    [System.Serializable]
    public struct EnemyAttack
    {
        [Tooltip("Name of animation clip to play. [CASE SENSITIVE]")]
        public string animationName;
        [Range(.01f,1f)]
        [Tooltip(".01 means very rare, 1 means super common.")]
        public float commonness;
        [Space]
        [Tooltip("Must include one node for every possible time a player gets hit.")]
        public AttackNode[] attackNodes; //For each possible point of being hit, so there would be two attack nodes for a double-swipe, each with attack type normal.
        [Tooltip("Additional time after finishing all hits before going to next attack.")]
        public float postAttackCooldown;
        [Space]
        public bool hasChargeSound;
        public bool hasSpecialChargeSound;
        
    }

    [System.Serializable]
    public struct AttackNode
    {
        public Enemy.AttackType attackType;

        //The time from the start of the animation at which it will check the player's current hitbox/state.
        [Tooltip("The amount of time from start of animation it will try to hit the player.")]
        public float hitRegisterDelay;
        public bool usesSound;
    }

    [System.Serializable]
    public struct Phase
    {
        [Tooltip("The life the enemy must be at to trigger the next stage")]
        public float lifeToTrigger;
        [Space]
        [Tooltip("Info about the actions and occurences before the enemy goes into random phase attacking")]
        public PhaseChange phaseChange;
        [Tooltip("All possible attack groups an enemy can reference and use in this stage.")]
        public string[] allAttackGroups;
    }

    [System.Serializable]
    public struct PhaseChange
    {
        public float timeBeforeAnimate;
        [Space]
        public string animationName;
        [Space]
        public float delayTilTutorial;

        public enum TutorialType
        {
            None,
            NormalAttack,
            FocusedAttack,
            StasisAttack,
            Dodge
        }
        public TutorialType tutorialType;
        [Space]
        public float delayTilFirstAttack;
        public EnemyAttack firstAttackInPhase;
    }










}

