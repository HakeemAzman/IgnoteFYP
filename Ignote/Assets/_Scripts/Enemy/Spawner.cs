using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header ("Spawner Settings")]
    public GameObject ENEMY_TYPE;        
    public float spawnTime = 0f;
    public int AMOUNT_OF_ENEMIES;

    [Space]
    public Transform[] spawnPoints;

    void Start()
    {
         InvokeRepeating("Spawn", spawnTime, spawnTime);
    }

    private void Update()
    {
        if(GameObject.FindGameObjectsWithTag("Enemy").Length >= AMOUNT_OF_ENEMIES)
        {
            CancelInvoke("Spawn");
        }
    }

    void Spawn()
    {
        int spawnPointIndex = Random.Range(0, spawnPoints.Length);
        
        Instantiate(ENEMY_TYPE, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
    }
}
