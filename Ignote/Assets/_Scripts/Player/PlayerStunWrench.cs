using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStunWrench : MonoBehaviour
{
    public GameObject wrench;
    public PlayerMovement pmScript;
    public Animator emilyAnim;

    private void Start()
    {
        emilyAnim = emilyAnim.GetComponent<Animator>();
        pmScript = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButton("Repair")) //Add animation for repairing here later on
        {
            wrench.GetComponent<BoxCollider>().enabled = true;
            emilyAnim.Play("EmilyRepairing");
            pmScript.playerCanMove = false;
        }

        if(Input.GetButtonUp("Repair"))
        {
            emilyAnim.Play("EmilyIdle");
            wrench.GetComponent<BoxCollider>().enabled = false;
            pmScript.playerCanMove = true;
        }
    }
}
