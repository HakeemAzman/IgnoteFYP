using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalePressurePlate : MonoBehaviour
{
    public GameObject animLeftDoor;
    public GameObject animRightDoor;
    [SerializeField] bool isTriggered;
    [SerializeField] Gate6E g6eScript;

    // Update is called once per frame
    void Update()
    {
        if(isTriggered && g6eScript.isTriggered)
        {
            animLeftDoor.GetComponent<Animator>().enabled = true;
            animRightDoor.GetComponent<Animator>().enabled = true;
        }
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
