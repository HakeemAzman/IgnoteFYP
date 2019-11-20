using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotGate : MonoBehaviour
{
    public Animator Gate_Robot_Animator;
    public string tag;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == tag)
        {
            Gate_Robot_Animator.GetComponent<Animator>().Play("RDMove");
        }
    }

    private void OnTriggerExit(Collider exit)
    {
        if (exit.gameObject.tag == tag)
        {
            Gate_Robot_Animator.GetComponent<Animator>().Play("RDDown");
        }
    }
}
