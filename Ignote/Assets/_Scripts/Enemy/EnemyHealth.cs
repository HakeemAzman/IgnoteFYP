using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
    public int enemy_Health;
    public GameObject deathParticle;
    [Space]
    public Compbat combatS;
    public CompanionScript cs;
    [Space]
    Animator enemyMovement;

    public void Start()
    {
        cs = GameObject.FindWithTag("Companion").GetComponent<CompanionScript>();
        combatS = GameObject.FindWithTag("Companion").GetComponent<Compbat>();
        enemyMovement = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        if(enemy_Health <= 0)
        {
            cs.speedFloat = 5;
            Instantiate(deathParticle, transform.position, transform.rotation);
            combatS.isEnemy = false;
            Destroy(gameObject, 0.5F);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Wrench")
        {
            enemyMovement.GetComponent<Animator>().SetFloat("forwardSpeed", 0);
            gameObject.GetComponent<EnemyAgroCombat>().enabled = false;
            gameObject.GetComponent<EnemyAgroMover>().enabled = false;
            StartCoroutine("reactivate");
        }

        if(other.gameObject.tag == "Projectile")
        {
            enemy_Health -= 40;
        }
    }

    IEnumerator reactivate()
    {
        yield return new WaitForSeconds(3);
        gameObject.GetComponent<EnemyAgroCombat>().enabled = true;
        gameObject.GetComponent<EnemyAgroMover>().enabled = true;
        enemyMovement.GetComponent<Animator>().SetFloat("forwardSpeed", 6);
    }
}
