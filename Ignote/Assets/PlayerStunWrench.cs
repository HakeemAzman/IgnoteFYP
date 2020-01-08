using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStunWrench : MonoBehaviour
{
    public GameObject wrench;
    public PlayerMovement pmScript;

    private void Start()
    {
        pmScript = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButton("Repair")) //Add animation for repairing here later on
        {
            wrench.SetActive(true);
            pmScript.playerCanMove = false;
        }
        else
        {
            wrench.SetActive(false);
            pmScript.playerCanMove = true;
        }

        /* if (Input.GetButtonDown("Attack") && !isAttack)
         {
             wrenchReady.SetActive(false);
             wrench.SetActive(true);
             StartCoroutine(SpawnVFX());
             isAttack = true;
         }

             if(isAttack)
             {
                 coolDown -= 1f * Time.deltaTime;
                 if(coolDown <= 4.5f)
                 {
                    wrench.SetActive(false);
                 }
                 if (coolDown <= 0)
                 {
                 wrenchReady.SetActive(true);
                 isAttack = false;
                 coolDown = 5;
                 }
         }*/
    }
}
