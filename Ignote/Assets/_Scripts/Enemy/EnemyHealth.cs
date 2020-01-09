using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
    public int enemy_Health;
    public GameObject deathParticle;
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
        if(enemy_Health <= 0)
        {
            cs.isEnemy = false;
            cs.speedFloat = 10;
            GameObject deathVFX = Instantiate(deathParticle, transform.position, transform.rotation);
            Destroy(deathVFX, 0.5f);
            Destroy(gameObject, 1F);
        }
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        
    }
}
