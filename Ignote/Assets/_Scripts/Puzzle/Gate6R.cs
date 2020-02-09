using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate6R : MonoBehaviour
{
    [SerializeField] bool isTriggered = false;
    [SerializeField] Gate6E geScript;
    [SerializeField] GateUp gateUpScript;

    // Update is called once per frame
    void Update()
    {
        if (isTriggered && geScript.isTriggered) gateUpScript.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Companion")) isTriggered = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Companion")) isTriggered = false;
    }
}
