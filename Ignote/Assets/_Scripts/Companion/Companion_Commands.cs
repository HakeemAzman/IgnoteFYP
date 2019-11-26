using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Companion_Commands : MonoBehaviour
{
    public GameObject companion;
    public CompanionScript cs;
    public float timer = 5;
    public float timededuct = 1;
    bool startCount = false;
    public bool Stay;
    public bool canMount;
    public Animator anim;
    public float callTimer =1;
    // Start is called before the first frame update
    void Start()
    {
        cs.gameObject.GetComponent<CompanionScript>();

        Stay = false;
    }

    // Update is called once per frame
    void Update()
    {
        Physics.IgnoreCollision(companion.GetComponent<Collider>(), gameObject.GetComponent<Collider>());
        //if (Input.GetButtonDown("Mount") && canMount)
        //{
        //    cMan.enableCompanion = true;
        //    cMan.enablePlayer = false;
        //    //Mount();
        //}
        //print("Stay");
        if (Input.GetButtonUp("Stay")) //Pressing X allows the Player to make the Companion stay at the current spot.
        {
            if(!Stay)
            {
                Stay = true;
                //print("stay");
                cs.GetComponent<CompanionScript>().enabled = false;
                anim.SetFloat("wSpeed", 0);
            }
            else
            {
                Stay = false;
                print("call");
                cs.GetComponent<CompanionScript>().enabled = true; //Pressing X again allows the Companion to follow from the current spot.
                cs.speedFloat = 5;
                callTimer = 1;
            }
        }


        //if (startCount) //Countdown to start
        //{
        //    timer -= timededuct * Time.deltaTime;
        //}
        //if (timer <= 0 && !cs.haveEnemy) //When it reaches O it stays at 0 and the Companion will start following again
        //{
        //    timer = 0;
        //    cs.GetComponent<CompanionScript>().enabled = true;
        //    Stay = false;
        //    anim.SetFloat("wSpeed", 5);
        //}

    }

    private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.tag == "Companion") //When the player is near the companion, the countdown doesn't start.
            {
                //timer = 3.5f;  
            }
        }

        private void OnTriggerExit(Collider other) //When the player leaves the companion after making it stay, it will start the countdown
        {
            if (other.gameObject.tag == "Companion")
            {
                //startCount = true;
                //canMount = false;
            }
        }

        //void Mount()
        //{
        //    gameObject.transform.position = mountPos.transform.position;
        //    transform.parent = mountPos;
        //    cs.gameObject.GetComponent<CompanionScript>().enabled = false;
        //    if (canMount)
        //    {
        //        gameObject.GetComponent<Rigidbody>().useGravity = false;
        //        gameObject.GetComponent<Rigidbody>().isKinematic = true;
        //    }
        //}

    }

