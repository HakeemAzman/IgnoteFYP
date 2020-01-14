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
            wrench.SetActive(true);
            emilyAnim.SetBool("isStandby", true);
            pmScript.playerCanMove = false;
        }
        else
        {
            wrench.SetActive(false);
            emilyAnim.SetBool("isStandby", false);
            pmScript.playerCanMove = true;
        }
    }
}
