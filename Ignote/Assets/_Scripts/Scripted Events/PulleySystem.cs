using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulleySystem : MonoBehaviour
{
    public Animator GateAnimator;
    
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            GateAnimator.gameObject.GetComponent<Animator>().Play("GateOpenPuzzle2");
        }
    }
}
