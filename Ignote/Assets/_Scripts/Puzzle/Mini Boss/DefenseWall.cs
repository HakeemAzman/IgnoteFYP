using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseWall : MonoBehaviour
{

    public GameObject defenseWall;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Crate") || other.CompareTag("Player") || other.CompareTag("Companion"))
        {
            defenseWall.GetComponent<GateDown>().enabled = true;
            defenseWall.GetComponent<GateUp>().enabled = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Crate") || other.CompareTag("Player") || other.CompareTag("Companion"))
        {
            defenseWall.GetComponent<GateDown>().enabled = true;
            defenseWall.GetComponent<GateUp>().enabled = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Crate") || other.CompareTag("Player") || other.CompareTag("Companion"))
        {
            defenseWall.GetComponent<GateDown>().enabled = false;
            defenseWall.GetComponent<GateUp>().enabled = true;
        }
    }
}
