using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate6E : MonoBehaviour
{
    public bool isTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) isTriggered = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) isTriggered = false;
    }
}
