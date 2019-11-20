using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooterHealth : MonoBehaviour
{
    public int enemy_Health;
    public GameObject deathParticle;
    [Space]
    public Compbat combatS;
    public CompanionScript cs;
    public EnemyShooterCombat esC;
    [Space]
    Animator enemyMovement;

    public void Start()
    {
        cs = GameObject.FindWithTag("Companion").GetComponent<CompanionScript>();
        combatS = GameObject.FindWithTag("Companion").GetComponent<Compbat>();
        enemyMovement = gameObject.GetComponent<Animator>();
        esC.GetComponent<EnemyShooterCombat>();
    }

    void Update()
    {
        if (enemy_Health <= 0)
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
            gameObject.GetComponent<EnemyShooterCombat>().enabled = false;
            StartCoroutine("reactivate");
        }

        if (other.gameObject.tag == "Projectile")
        {
            enemy_Health -= 40;
        }
    }

    IEnumerator reactivate()
    {
        yield return new WaitForSeconds(3);
        gameObject.GetComponent<EnemyShooterCombat>().enabled = true;
    }
}


