using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StudyRoomPlate : MonoBehaviour
{
    public GameObject gate3;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Companion") || other.CompareTag("Player"))
        {
            gate3.GetComponent<GateUp>().enabled = true;
            gate3.GetComponent<GateDown>().enabled = false;
            print("Companion");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Companion") || other.CompareTag("Player"))
        {
            gate3.GetComponent<GateUp>().enabled = false;
            gate3.GetComponent<GateDown>().enabled = true;
        }
    }
}
