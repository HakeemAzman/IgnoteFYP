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
    public float callTimer = 1;

    public bool canRepair = false; //Controlled in this script and CompanionHealthScript
    public bool isRepairing = false;

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

        if (Input.GetButtonUp("Stay"))
        {
            if(!Stay)
            {
                Stay = true;
                cs.GetComponent<CompanionScript>().enabled = false;
                anim.SetFloat("wSpeed", 0);
            }
            else
            {
                Stay = false;
                cs.GetComponent<CompanionScript>().enabled = true;
                cs.speedFloat = 10;
                callTimer = 1;
            }
        }

        if (Input.GetButton("Repair") && canRepair)
        {
            isRepairing = true;
        }
        else
            isRepairing = false;
    }

    private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.tag == "Companion")
            {
                canRepair = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.tag == "Companion")
            {
                canRepair = false;
            }
        }
    }

