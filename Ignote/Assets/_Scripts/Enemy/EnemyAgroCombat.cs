using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAgroCombat : MonoBehaviour
{
    public GameObject attackParticleTemp;

    [SerializeField] float timeBetweenAttacks = 1f;
    [SerializeField] float damageOutput = 5f;

    float timeSinceLastAttack = Mathf.Infinity;

    GameObject player;
    GameObject companion;

    PlayerHealth playerHealth;
    CompanionHealth companionHealth;
    
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        companion = GameObject.FindWithTag("Companion");

        playerHealth = player.GetComponent<PlayerHealth>();
        companionHealth = companion.GetComponent<CompanionHealth>();
    }
    
    void Update()
    {
        timeSinceLastAttack += Time.deltaTime;
    }

    public void AttackPlayer()
    {
        Vector3 directionToTarget = player.transform.position - transform.position;
        //the angle between the enemy's face and the player.
        float angle = Vector3.Angle(directionToTarget, transform.forward);

        //Damage to player
        if (Mathf.Abs(angle) < 60 && timeSinceLastAttack > timeBetweenAttacks)
        {
            timeSinceLastAttack = 0;

            playerHealth.playerCurrentHealth -= damageOutput;

            GetComponent<Animator>().SetTrigger("attack");
        }

        //When enemy is facing the player at an angle of 10, it will attack the player. But NO DAMAGE.
        //This is to simulate near miss effect where the player barely misses the enemy's attacks.
        else if (Mathf.Abs(angle) < 100 && timeSinceLastAttack > timeBetweenAttacks)
        {
            timeSinceLastAttack = 0;

            GetComponent<Animator>().SetTrigger("attack");
        }
    }

    public void AttackCompanion()
    {
        Vector3 directionToTarget = companion.transform.position - transform.position;

        //the angle between the enemy's face and the player.
        float angle = Vector3.Angle(directionToTarget, transform.forward);

        //Damage to player
        if (Mathf.Abs(angle) < 40 && timeSinceLastAttack > timeBetweenAttacks)
        {
            timeSinceLastAttack = 0;

            companionHealth.companionCurrentHealth -= damageOutput;

            GetComponent<Animator>().SetTrigger("attack");
        }

        //When enemy is facing the player at an angle of 10, it will attack the player. But NO DAMAGE.
        //This is to simulate near miss effect where the player barely misses the enemy's attacks.
        else if (Mathf.Abs(angle) < 100 && timeSinceLastAttack > timeBetweenAttacks)
        {
            timeSinceLastAttack = 0;

            GetComponent<Animator>().SetTrigger("attack");
        }
    }
    

}
