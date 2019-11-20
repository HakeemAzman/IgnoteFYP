using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotPlayerGateUp : MonoBehaviour
{
    public Animator Gate_Robot_Animator;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Gate_Robot_Animator.GetComponent<Animator>().Play("RDMove");
        }
    }

    private void OnTriggerExit(Collider exit)
    {
        if (exit.gameObject.tag == "Player")
        {
            Gate_Robot_Animator.GetComponent<Animator>().Play("RDDown");
        }
    }
}
