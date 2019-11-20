using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooterCombat : MonoBehaviour
{
    [SerializeField] float m_timeBetweenAttacks = 1f;
    [SerializeField] float m_damageOutput = 5f;
    [SerializeField] float m_health;

    [SerializeField] float m_distanceFromPlayer;

    float timeSinceLastAttack = Mathf.Infinity;

    #region Player, PlayerHealth
    Transform targetPlayer;
    PlayerHealth playerHealth;
    #endregion

    #region Companion
    Transform targetCompanion;
    CompanionHealth companionHealth;
    #endregion

    #region Enemy
    Transform enemyTransform;
    #endregion

    public Rigidbody m_projectile;
    public Transform m_fireTransform;
    public float m_launchForce;

    public GameObject m_laser;

    // Start is called before the first frame update
    void Start()
    {
        targetPlayer = PlayerManager.instance.player.transform;
        targetCompanion = PlayerManager.instance.companion.transform;
        playerHealth = targetPlayer.GetComponent<PlayerHealth>();
        companionHealth = targetCompanion.GetComponent<CompanionHealth>();
        
    }

    // Update is called once per frame
    void Update()
    {
        //enemyTransform = GameObject.FindWithTag("Enemy").transform;
        //Debug.Log(enemyTransform);
        timeSinceLastAttack += Time.deltaTime;


        AttackPlayer();
        //AttackCompanion();

        Aim();
    }

    public void attackEnemy()
    {
        FaceTarget(enemyTransform);
        Fire(enemyTransform);
    }

    public void AttackPlayer()
    {
        //the distance between the player and the enemy.
        float distFromPlayer = Vector3.Distance(targetPlayer.position, transform.position);
        
        //if the desired distance between the enemy and player is met
        //or when the player is in enemy's attack range, enemy will rotate to face the player.
        if (distFromPlayer <= m_distanceFromPlayer)
        {
            FaceTarget(targetPlayer);
            Fire(targetPlayer);
        }
    }

    public void AttackCompanion()
    {
        //the distance between the bot and the enemy.
        float distanceFromCompanion = Vector3.Distance(targetCompanion.position, transform.position);

        //if the desired distance between the enemy and bot is met
        //or when the bot is in enemy's attack range, enemy will rotate to face the bot.
        if (distanceFromCompanion <= 10)
        {
            FaceTarget(targetCompanion);
            Fire(targetCompanion);
        }
    }


    void Fire(Transform target)
    {
        Vector3 directionToTarget = (target.position - transform.position).normalized;
        float angle = Vector3.Angle(directionToTarget, transform.forward);
        

        if(Mathf.Abs(angle) < 40 && timeSinceLastAttack > m_timeBetweenAttacks)
        {
            GetComponent<Animator>().SetTrigger("attackbow");

            timeSinceLastAttack = 0;

            Rigidbody shellInstance = Instantiate(m_projectile, m_fireTransform.position, Quaternion.LookRotation(directionToTarget)) as Rigidbody;

            shellInstance.velocity = shellInstance.transform.forward * m_launchForce;

            //shellInstance.velocity = directionToTarget * launchForce;
        }

    }

    void Aim()
    {
        if((timeSinceLastAttack >= 2f) && (timeSinceLastAttack < 3f))
        {
            m_laser.SetActive(true);
        }
        else
        {
            m_laser.SetActive(false);
        }
    }


    void FaceTarget(Transform target)
    {
        Vector3 directionToTarget = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(directionToTarget.x, 0, directionToTarget.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }
}
