using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArchduchessBedroom : MonoBehaviour
{
    public GameObject obj;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            obj.GetComponent<Animator>().enabled = true;
        }
    }
}
