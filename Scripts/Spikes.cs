using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    public bool hazard;
    public float hazardCounter, hazardTime;

    public int phase;

    private Animator anim;

    private Collider2D coll;
    
    public float readyCounter, readyTime;

    public float strikeCounter, strikeTime;

    public float disarmCounter, disarmTime;
    
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckPhase();
        CheckHazard();
    }

    void CheckPhase()
    {
        switch(phase)
        {
            case 0:
                if(readyCounter >= readyTime)
                {
                    Ready();
                    phase = 1;
                    readyCounter = 0;
                }
                else
                {
                    readyCounter += Time.deltaTime;
                }
                break;
            case 1:
                if(strikeCounter >= strikeTime)
                {
                    Strike();
                    phase = 2;
                    strikeCounter = 0;
                }
                else
                {
                    strikeCounter += Time.deltaTime;
                }
                break;
            case 2:
                if(disarmCounter >= disarmTime)
                {
                    Disarm();
                    phase = 0;
                    disarmCounter = 0;
                }
                else
                {
                    disarmCounter += Time.deltaTime;
                }
                break;
        }
    }

    void CheckHazard()
    {
        if(hazard)
        {
            if(hazardCounter >= hazardTime)
            {
                hazardCounter = 0;
                TurnOffHazard();
            }
            else
            {
                hazardCounter += Time.deltaTime;
            }
        }
    }

    void TurnOnHazard()
    {
        hazard = true;
        gameObject.tag = "Hazard";
    }

    void TurnOffHazard()
    {
        hazard = false;
        gameObject.tag = "Untagged";
    }

    void Ready()
    {
        anim.Play("Ready");
    }

    void Disarm()
    {
        coll.enabled = false;
        anim.Play("Disarm");
    }

    void Strike()
    {
        TurnOnHazard();
        coll.enabled = true;
        anim.Play("Strike");
    }
}
