using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AEK;

public class SpikeManager : MonoBehaviour
{
    public static SpikeManager Main;

    private void Awake()
    {
        Main = this;
    }
    public AudioClip chargeEffect;
    public AudioClip stasisStrike;
    public AudioClip unblockStrike;
    public AudioClip regStrike;

    public Animator[] frontSpikes;
    public Animator[] backSpikes;

    public void StartSpikes()
    {
        ActivateSRS();
        StartCoroutine(SpikeAttacks());
    }

    public IEnumerator SpikeAttacks()
    {
        while (Enemy.Main.Life>0)
        {

            print("InSpikeAttacks");
            yield return new WaitForSeconds(1);
            yield return DoAttack();
        }
    }


    public IEnumerator DoAttack()
    {
        float _attackIndex = Random.Range(0, 6.49f);
        int attackIndex = Mathf.RoundToInt(_attackIndex);
        SFXManager.main.Play(chargeEffect, .8f, 1, .1f, .12f);
        switch (attackIndex)
        {
            case 0:
                StartCoroutine(StartSpikeAnim(true, "Purple"));
                StartCoroutine(StartSpikeAnim(false, "Purple"));
                yield return new WaitForSeconds(1);
                MainControls.Main.MustStasised();
                StasisSFX();
                break;
            case 1:
                StartCoroutine(StartSpikeAnim(true, "Red"));
                yield return new WaitForSeconds(1);
                StartCoroutine(StartSpikeAnim(false, "Red"));

                Unblockable();
                yield return new WaitForSeconds(1);
                DodgePos();
                break;
            case 2:
                StartCoroutine(StartSpikeAnim(true, "Orange"));
                StartCoroutine(StartSpikeAnim(false, "Red"));
                yield return new WaitForSeconds(1);
                MustBeForward();
                yield return new WaitForSeconds(.7f);
                StartCoroutine(StartSpikeAnim(true, "Red"));
                yield return new WaitForSeconds(1);
                Unblockable();
                yield return new WaitForSeconds(.7f);
                StartCoroutine(StartSpikeAnim(true, "Purple"));
                StartCoroutine(StartSpikeAnim(false, "Red"));
                yield return new WaitForSeconds(1);
                MustBeForwardStasis();
                break;
            case 3:
                StartCoroutine(StartSpikeAnim(true, "Red"));
                yield return new WaitForSeconds(1);
                Unblockable();
                yield return new WaitForSeconds(.7f);
                StartCoroutine(StartSpikeAnim(true, "Orange"));
                StartCoroutine(StartSpikeAnim(false, "Red"));
                yield return new WaitForSeconds(1);
                MustBeForward();
                yield return new WaitForSeconds(.7f);
                StartCoroutine(StartSpikeAnim(true, "Purple"));
                StartCoroutine(StartSpikeAnim(false, "Red"));
                yield return new WaitForSeconds(1);
                MustBeForwardStasis();
                yield return new WaitForSeconds(.3f);
                StartCoroutine(StartSpikeAnim(true, "Red"));
                yield return new WaitForSeconds(1);
                Unblockable();
                break;
            case 4:
                StartCoroutine(StartSpikeAnim(true, "Orange"));
                StartCoroutine(StartSpikeAnim(false, "Orange"));
                yield return new WaitForSeconds(1);
                Damaged();
                yield return new WaitForSeconds(.5f);
                StartCoroutine(StartSpikeAnim(true, "Purple"));
                StartCoroutine(StartSpikeAnim(false, "Purple"));
                yield return new WaitForSeconds(1);
                MainControls.Main.MustStasised();
                StasisSFX();
                yield return new WaitForSeconds(.5f);
                StartCoroutine(StartSpikeAnim(true, "Purple"));
                StartCoroutine(StartSpikeAnim(false, "Purple"));
                yield return new WaitForSeconds(1);
                MainControls.Main.MustStasised();
                StasisSFX();
                yield return new WaitForSeconds(.5f);
                StartCoroutine(StartSpikeAnim(true, "Orange"));
                StartCoroutine(StartSpikeAnim(false, "Orange"));
                yield return new WaitForSeconds(1);
                Damaged();
                yield return new WaitForSeconds(.5f);
                StartCoroutine(StartSpikeAnim(true, "Purple"));
                StartCoroutine(StartSpikeAnim(false, "Purple"));
                yield return new WaitForSeconds(1);
                MainControls.Main.MustStasised();
                StasisSFX();
                yield return new WaitForSeconds(.5f);

                StartCoroutine(StartSpikeAnim(true, "Orange"));
                StartCoroutine(StartSpikeAnim(false, "Orange"));
                yield return new WaitForSeconds(1);
                Damaged();
                yield return new WaitForSeconds(.5f);
                StartCoroutine(StartSpikeAnim(true, "Orange"));
                StartCoroutine(StartSpikeAnim(false, "Orange"));
                yield return new WaitForSeconds(1);
                Damaged();
                yield return new WaitForSeconds(.5f);
                break;
            case 5:
                StartCoroutine(StartSpikeAnim(true, "Purple"));
                StartCoroutine(StartSpikeAnim(false, "Purple"));
                yield return new WaitForSeconds(1);
                MainControls.Main.MustStasised();
                StasisSFX();
                yield return new WaitForSeconds(.5f);
                StartCoroutine(StartSpikeAnim(true, "Orange"));
                StartCoroutine(StartSpikeAnim(false, "Orange"));
                yield return new WaitForSeconds(1);
                Damaged();
                yield return new WaitForSeconds(.5f);
                StartCoroutine(StartSpikeAnim(true, "Red"));
                yield return new WaitForSeconds(1);
                Unblockable();
                yield return new WaitForSeconds(.7f);
                StartCoroutine(StartSpikeAnim(true, "Purple"));
                StartCoroutine(StartSpikeAnim(false, "Purple"));
                yield return new WaitForSeconds(1);
                MainControls.Main.MustStasised();
                StasisSFX();
                yield return new WaitForSeconds(.5f);
                StartCoroutine(StartSpikeAnim(true, "Orange"));
                StartCoroutine(StartSpikeAnim(false, "Orange"));
                yield return new WaitForSeconds(1);
                Damaged();
                yield return new WaitForSeconds(.5f);
                StartCoroutine(StartSpikeAnim(true, "Orange"));
                StartCoroutine(StartSpikeAnim(false, "Orange"));
                yield return new WaitForSeconds(1);
                Damaged();
                yield return new WaitForSeconds(.5f);
                break;
            case 6:
                StartCoroutine(StartSpikeAnim(true, "Red"));
                yield return new WaitForSeconds(1);
                Unblockable();
                yield return new WaitForSeconds(.7f);
                StartCoroutine(StartSpikeAnim(true, "Purple"));
                StartCoroutine(StartSpikeAnim(false, "Red"));
                yield return new WaitForSeconds(1);
                MustBeForwardStasis();
                yield return new WaitForSeconds(.7f);
                StartCoroutine(StartSpikeAnim(true, "Orange"));
                StartCoroutine(StartSpikeAnim(false, "Red"));
                yield return new WaitForSeconds(1);
                MustBeForward();
                yield return new WaitForSeconds(.7f);
                StartCoroutine(StartSpikeAnim(true, "Red"));
                yield return new WaitForSeconds(1);
                Unblockable();
                yield return new WaitForSeconds(.7f);
                StartCoroutine(StartSpikeAnim(true, "Purple"));
                StartCoroutine(StartSpikeAnim(false, "Red"));
                yield return new WaitForSeconds(1);
                MustBeForwardStasis();
                break;

        }

        yield return new WaitForSeconds(1.1f);
    }

    public IEnumerator StartSpikeAnim(bool isFrontSpike, string colorName)
    {
        string animationName = colorName + "Spike";
        for (int i = 0; i < 3; i++)
        {


            if (isFrontSpike)
            {
                frontSpikes[i].SetTrigger(animationName);
            }
            else
            {
                backSpikes[i].SetTrigger(animationName);
            }
            yield return new WaitForSeconds(Random.Range(0.05f, .15f));
        }
    }

    public void ActivateSRS()
    {
        for (int i = 0; i < 3; i++)
        {
            frontSpikes[i].GetComponent<SpriteRenderer>().enabled = true;
            backSpikes[i].GetComponent<SpriteRenderer>().enabled = true;
        }
    }

    void Damaged()
    {
        MainControls.Main.Damaged();
        SFXManager.main.Play(regStrike, .9f, 1, .2f, .12f);
    }

    void Unblockable()
    {
        MainControls.Main.AttackedByUnblockable();
        SFXManager.main.Play(unblockStrike, .9f, 1, .2f, .12f);
    }

    void DodgePos()
    {
        MainControls.Main.AttackedAtDodgePosition();
        SFXManager.main.Play(unblockStrike, .9f, 1, .2f, .12f);
    }

    void StasisSFX()
    {
        SFXManager.main.Play(stasisStrike, .9f, 1, .2f, .12f);
    }

    void Normal()
    {
        SFXManager.main.Play(regStrike, .9f, 1, .2f, .12f);
    }

    void MustBeForward()
    {
        SFXManager.main.Play(regStrike, .6f, 1, .2f, .12f);
        SFXManager.main.Play(unblockStrike, .6f, 1, .2f, .12f);
        if (MainControls.Main.canDS)
        {
            MainControls.Main.AttackedAtDodgePosition();
        }
        else
        {
            MainControls.Main.Damaged();
        }
    }

    void MustBeForwardStasis()
    {
        SFXManager.main.Play(stasisStrike, .6f, 1, .2f, .12f);
        SFXManager.main.Play(unblockStrike, .6f, 1, .2f, .12f);
        if (MainControls.Main.canDS)
        {
            MainControls.Main.AttackedAtDodgePosition();
        }
        else
        {
            MainControls.Main.Stasised();
        }
    }
}
