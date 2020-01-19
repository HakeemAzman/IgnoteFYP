using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyDefenses : MonoBehaviour
{
    public GameObject[] defenses;
    public GameObject fenceDown;
    public GameObject fenceUp;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Wrench"))
        {
            fenceUp.GetComponent<GateUp>().enabled = true;
            fenceDown.GetComponent<GateDown>().enabled = true;
            defenses[0].SetActive(false);
            defenses[1].SetActive(false);
            defenses[2].SetActive(false);
        }
    }
}
