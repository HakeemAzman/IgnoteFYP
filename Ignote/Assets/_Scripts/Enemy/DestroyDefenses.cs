using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyDefenses : MonoBehaviour
{
    public GameObject[] defenses;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            defenses[0].SetActive(false);
            defenses[1].SetActive(false);
            defenses[2].SetActive(false);
        }
    }
}
