using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Companion_Commands : MonoBehaviour
{
    public GameObject companion;
    public PlayerMovement pmScript;
    public Animator emilyAnim;
    public CompanionScript cs;
    public float timer = 5;
    public float timededuct = 1;
    bool startCount = false;
    public bool Stay;
    public bool canMount;
    public Animator anim;
    public float callTimer = 1;

    [SerializeField] bool canCommand = false;

    public bool canRepair = false; //Controlled in this script and CompanionHealthScript
    public bool isRepairing = false;

    // Start is called before the first frame update
    void Start()
    {
        cs.gameObject.GetComponent<CompanionScript>();
        pmScript = GetComponent<PlayerMovement>();
        Stay = false;
    }

    // Update is called once per frame
    void Update()
    {
       Physics.IgnoreCollision(companion.GetComponent<Collider>(), gameObject.GetComponent<Collider>());

        float dist = Vector3.Distance(companion.transform.position, transform.position);

        if (dist > 30)
        {
            canCommand = false;
            robotFollow();
        }
        else if (dist < 30)
        {
            canCommand = true;
        }

        if (Input.GetButtonUp("Stay") && canCommand)
            {
                if (!Stay)
                {
                Stay = true;
                robotStay();
                }
                else
                {
                Stay = false;
                robotFollow();
                }
            }

        if (Input.GetButton("Repair") && canRepair)
        {
            isRepairing = true;
            pmScript.playerCanMove = false;
        }
        else
        {
            isRepairing = false;
            pmScript.playerCanMove = true;
        }
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

    void robotStay()
    {
        cs.GetComponent<CompanionScript>().speedFloat = 0;
        companion.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        companion.GetComponent<NavMeshAgent>().enabled = false;
        companion.GetComponent<CompanionScript>().stayVFX.Play();
        anim.SetFloat("wSpeed", 0);
    }

    void robotFollow()
    {
        cs.GetComponent<CompanionScript>().speedFloat = 10;
        companion.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        companion.GetComponent<NavMeshAgent>().enabled = true;
        companion.GetComponent<CompanionScript>().stayVFX.Stop();
    }
}

