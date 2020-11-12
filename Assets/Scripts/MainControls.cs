using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainControls : MonoBehaviour
{
    Animator m_Animator;

    public int Life;
    public int bossLife; 
    public int chargedHit = 2;

    public bool PSwing;
    public bool PCharge;
    public bool BSwing;
    public bool timeOnce = true;
    public bool liTrue = true;
    public bool heaTrue = false;

    public float chargeTime = 1;
    public float timeLeft;
   
    


    // Start is called before the first frame update
    void Start()
    {
        m_Animator = gameObject.GetComponent<Animator>();
        timeLeft = chargeTime;
    }

    // Update is called once per frame
    void Update()
    { 
        if (Input.GetKey(KeyCode.Space))
        {
           
            timeLeft -= Time.deltaTime;

            if (timeLeft > 0)
            {
                liTrue = true;
                heaTrue = false;
            }

            else if (timeLeft < 0)
            {
                heaTrue = true;
                liTrue = false;
                
            }

        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (liTrue)
            {
                timeLeft = chargeTime;
                LiStrike();
            }

            else if (heaTrue)
            {
                timeLeft = chargeTime;
                HeavyStrike();
            }
            
        }
    }

    public void ClearAni()
    {
        m_Animator.SetBool("LightStrike", false);
        m_Animator.SetBool("HeavyStrike", false);
    }

    public void LiStrike()
    {
        m_Animator.SetBool("LightStrike", true);
        if (!BSwing)
            {
                bossLife--;
            Debug.Log("Light Strike");
            }

        
    }

    public void HeavyStrike()
    {
        m_Animator.SetBool("HeavyStrike", true);
        if (!BSwing)
            {
                bossLife -= chargedHit;
            Debug.Log("Heavy Strike");
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
        if (PSwing)
            return false;
        if (PCharge)
            return false;
        if (Life <= 0)
            return false;
        return true;
    }

    public void Deflect()
    {

    }

    public void Dodge()
    {
    
    }

    public void Break()
    {
        Life--;
        if (Life >= 0)
        {
            //dead
        }
      
    }
}
