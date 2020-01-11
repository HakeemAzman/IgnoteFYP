﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyShooterMover : MonoBehaviour
{
    [SerializeField] float lookRadius = 5f;
    [SerializeField] float chaseCompanionDistance = 10f;

    NavMeshAgent agent;

    #region Player, PlayerHealth
    Transform targetPlayer;
    PlayerHealth playerHealth;
    #endregion

    #region Enemy Combat System, Enemy Idle Behaviour
    EnemyShooterCombat Combat;
    
    float timeSinceLastSawPlayer = Mathf.Infinity;
    #endregion

    #region Companion
    Transform targetCompanion;
    #endregion

    private void Start()
    {
        targetPlayer = PlayerManager.instance.player.transform;
        targetCompanion = PlayerManager.instance.companion.transform;

        Combat = GetComponent<EnemyShooterCombat>();
        agent = GetComponent<NavMeshAgent>();

        playerHealth = targetPlayer.GetComponent<PlayerHealth>();
    }

    private void Update()
    {
        if (playerHealth.playerCurrentHealth == 0) return;

        if (targetCompanion != null && InChasingRangeCompanion())
        {
            timeSinceLastSawPlayer = 0;
            ChaseCompanion();
        }
        else
        {
            timeSinceLastSawPlayer = 0;
            ChasePlayer();
        }

        timeSinceLastSawPlayer += Time.deltaTime;

        UpdateAnimator();
    }

    private void UpdateAnimator()
    {
        Vector3 velocity = GetComponent<NavMeshAgent>().velocity;
        Vector3 localVelocity = transform.InverseTransformDirection(velocity);
        float speed = localVelocity.z;
        GetComponent<Animator>().SetFloat("forwardSpeed", speed);
    }

    //When player is in front of the enemy then the enemy will start chasing.
    private bool InLineOfSightPlayer()
    {
        Vector3 directionToTarget = targetPlayer.position - transform.position;
        //the angle between the enemy's face and the player.
        float angle = Vector3.Angle(transform.forward, directionToTarget);
        return Mathf.Abs(angle) < 90;
    }

    private bool InChasingRangeCompanion()
    {
        float distanceFromCompanion = Vector3.Distance(targetCompanion.position, transform.position);
        return distanceFromCompanion < chaseCompanionDistance;
    }

    private bool InChasingRangePlayer()
    {
        float distanceFromPlayer = Vector3.Distance(targetPlayer.position, transform.position);
        return distanceFromPlayer < lookRadius;
    }

    private void ChasePlayer()
    {
        //the distance between the player and the enemy.
        float distanceFromPlayer = Vector3.Distance(targetPlayer.position, transform.position);

        //agent.SetDestination(targetPlayer.position);

        //if the desired distance between the enemy and player is met
        //or when the player is in enemy's attack range, enemy will rotate to face the player.
        if (distanceFromPlayer <= agent.stoppingDistance)
        {
            FaceTarget(targetPlayer);

            //Calling the enemy's attack function
            Combat.AttackPlayer();
        }
    }

    private void ChaseCompanion()
    {
        //the distance between the bot and the enemy.
        float distanceFromCompanion = Vector3.Distance(targetCompanion.position, transform.position);

        //agent.SetDestination(targetCompanion.position);

        //if the desired distance between the enemy and bot is met
        //or when the bot is in enemy's attack range, enemy will rotate to face the bot.
        if (distanceFromCompanion <= agent.stoppingDistance)
        {
            FaceTarget(targetCompanion);
           //Combat.AttackCompanion();
        }
    }

    void FaceTarget(Transform target)
    {
        Vector3 directionToTarget = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(directionToTarget.x, 0, directionToTarget.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
