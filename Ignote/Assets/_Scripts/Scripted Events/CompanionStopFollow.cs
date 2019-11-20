using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompanionStopFollow : MonoBehaviour
{
    public CompanionScript companion_Script;


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            companion_Script.GetComponent<CompanionScript>().speedFloat = 0;
        }
    }
}
