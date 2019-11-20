using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotMeet : MonoBehaviour
{
    public CompanionScript cs;
    public CompanionHealth ch;
    public Compbat compS;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            cs.gameObject.GetComponent<CompanionScript>().enabled = true;
            ch.gameObject.GetComponent<CompanionHealth>().enabled = true;
            compS.gameObject.GetComponent<Compbat>().enabled = true;
            Destroy(this.gameObject, 0.5f);
        }
     
    }
}
