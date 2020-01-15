using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoungeGate2 : MonoBehaviour
{
    public LoungeGate lgScript;
    public GateUp guScript;
    public bool isThisTriggered;

    // Update is called once per frame
    void Update()
    {
        if(lgScript.isTriggered && isThisTriggered)
        {
            guScript.enabled = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Crate") || other.CompareTag("Player"))
        {
            isThisTriggered = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Crate") || other.CompareTag("Player"))
        {
            isThisTriggered = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Crate") || other.CompareTag("Player"))
        {
            isThisTriggered = false;
        }
    }
}
