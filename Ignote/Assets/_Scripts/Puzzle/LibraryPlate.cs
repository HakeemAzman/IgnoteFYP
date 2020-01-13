using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LibraryPlate : MonoBehaviour
{
    public GameObject gateLibrary;
    public GameObject libraryFence;

    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            gateLibrary.GetComponent<GateUp>().enabled = true;
            libraryFence.GetComponent<GateUp>().enabled = true;
        }
    }
}
