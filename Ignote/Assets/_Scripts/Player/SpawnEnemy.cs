using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public GameObject[] enemy;

    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "SpawnCathedral")
        {
            enemy[0].SetActive(true);
            enemy[1].SetActive(true);
            enemy[2].SetActive(true);
        }

        if(other.name == "SpawnStudy")
        {
            enemy[3].SetActive(true);
            enemy[5].SetActive(true);
        }

        if(other.name == "SpawnLibrary")
        {
            enemy[6].SetActive(true);
            enemy[7].SetActive(true);
            enemy[8].SetActive(true);
        }

        if (other.name == "SpawnSegregation")
        {
            enemy[14].SetActive(true);
            enemy[15].SetActive(true);
            enemy[16].SetActive(true);
            enemy[17].SetActive(true);
            enemy[18].SetActive(true);
        }
    }
}
