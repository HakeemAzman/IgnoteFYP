using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactables : MonoBehaviour
{
    public Animator animRobotGate;

    public CompanionScript companion_Script;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            animRobotGate.GetComponent<Animator>().Play("GateUp");
        }
    }
}
