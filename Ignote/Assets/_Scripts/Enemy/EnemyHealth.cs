using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
    public int enemy_Health;
    public GameObject deathParticle;
    public int score = 1;
    [Space]
    public CompanionScript cs;
    [Space]
    Animator enemyMovement;

    protected virtual void Start()
    {
        cs = GameObject.FindWithTag("Companion").GetComponent<CompanionScript>();
        enemyMovement = gameObject.GetComponent<Animator>();
    }

    protected virtual void Update()
    {
        if (enemy_Health <= 0)
        {
        }
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        
    }
}
