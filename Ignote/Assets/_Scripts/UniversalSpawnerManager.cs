using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniversalSpawnerManager : MonoBehaviour
{
    public GameObject spawner;

    private void Start()
    {
        spawner.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            spawner.SetActive(true);
        }
    }
}
