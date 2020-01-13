using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FenceDown : MonoBehaviour
{
    public GameObject fenceLibrary;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            fenceLibrary.GetComponent<GateDown>().enabled = true;
        }
    }
}
