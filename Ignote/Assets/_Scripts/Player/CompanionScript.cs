using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CompanionScript : MonoBehaviour
{
    private NavMeshAgent agent;
    private Vector3 playerAI;
    public Animator anim;
    [Space]

    [Header("Scripts")]
    public Companion_Commands cc;
    public CompanionHealth ch;
    [Space]

    public float speedFloat;
    [Space]

    [Header("Bool")]
    public bool isPlayer;
    public bool haveEnemy;
    public bool isEnemy;
    public bool playerAiFound = true;
    public bool enemyInSight;
    [Space]

    [Header("Gameobjects")]
    public GameObject Player;
    public GameObject Overcharge;
    [Space]

    [Header("AOE")]
    public float radius;
    public float kbForce;
    public int damage = 30;
    public LayerMask check;
    float dist;
    [Space]

    public GameObject psAOE;
    public GameObject[] aoeVFXStore;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        cc.GetComponent<Companion_Commands>();
        ch.GetComponent<CompanionHealth>();
        speedFloat = 10; //When the robot wakes up at the start
    }

    protected virtual void Update()
    {
        if (((ch.companionCurrentHealth / 100) * 100) > 75)
        {
            psAOE = aoeVFXStore[0];
            damage = 30;
        }

        if (((ch.companionCurrentHealth / 100) * 100) < 75)
        {
            if(((ch.companionCurrentHealth / 100) * 100) > 50)
            {
                psAOE = aoeVFXStore[1];
                damage = 40;
            }
        }

        if (((ch.companionCurrentHealth / 100) * 100) < 50)
        {
            if (((ch.companionCurrentHealth / 100) * 100) > 25)
            {
                psAOE = aoeVFXStore[2];
                damage = 50;
            }
        }

        if (((ch.companionCurrentHealth / 100) * 100) < 25)
        {
            if (((ch.companionCurrentHealth / 100) * 100) > 0)
            {
                psAOE = aoeVFXStore[3];
                damage = 60;
            }
        }

        playerAI = FindClosestPlayer().transform.position;
        agent.destination = playerAI;
        gameObject.GetComponent<NavMeshAgent>().speed = speedFloat;

        if (!isEnemy)
        {
            anim.gameObject.GetComponent<Animator>().SetBool("enemyF", false);
            anim.gameObject.GetComponent<Animator>().SetFloat("walk", 5);
        }

        if (!haveEnemy)
        {
            anim.gameObject.GetComponent<Animator>().SetBool("enemyF", false);
        }

        dist = Vector3.Distance(transform.position, GameObject.FindWithTag("Enemy").transform.position);
        
        if (dist <= 20)
        {
            haveEnemy = true;
            enemyInSight = true;
        }

        if (dist > 20)
        {
            haveEnemy = false;

            enemyInSight = false;
        }
    }

#region Find Closest Player
    GameObject FindClosestPlayer()
    {
        GameObject[] eTargets;
        GameObject[] targets;

        eTargets = GameObject.FindGameObjectsWithTag("Enemy");
        targets = GameObject.FindGameObjectsWithTag("Player");

        if (enemyInSight && eTargets.Length >= 1)
        {
            targets = GameObject.FindGameObjectsWithTag("Enemy");
        }

        if (eTargets.Length == 0)
        {
            enemyInSight = false;
            haveEnemy = false;
            targets = GameObject.FindGameObjectsWithTag("Player");
        }

        GameObject closestPlayer = null;
        var distance = Mathf.Infinity;
        Vector3 position = transform.position;

        // Iterate through them and find the closest one
        foreach (GameObject target in targets)
        {
            Vector3 difference = (target.transform.position - position);
            float curDistance = difference.sqrMagnitude;
            if (curDistance < distance)
            {
                closestPlayer = target;
                distance = curDistance;
            }
        }

        if (speedFloat >= 10)
        {
            anim.SetFloat("wSpeed", 5);
        }

        else if (speedFloat <= 0 && isPlayer)
        {
            anim.SetFloat("wSpeed", 0);
        }
        return closestPlayer;
        #endregion

    }

    protected virtual void OnTriggerStay(Collider other) //Stops before reaching the player so it's not directly behind the player.
    {
        if (other.gameObject.tag == "Player")
        {
            speedFloat = 0;
            anim.SetFloat("wSpeed", 0);
            isPlayer = true;
        }

        if (other.gameObject.tag == "Enemy")
        {
            isPlayer = false;
            isEnemy = true;
            EnemyInRange();
        }

        if (!haveEnemy)
        {
            Player.gameObject.GetComponent<BoxCollider>().enabled = true;
        }

        else if (haveEnemy)
        {
            Player.gameObject.GetComponent<BoxCollider>().enabled = false;
        }

        if (other.gameObject.tag == "Point1")
        {
            StartCoroutine("interacting");
        }
    }

    protected virtual void OnTriggerExit(Collider other) //Starts following when the player is too far again.
    {
        if (other.gameObject.tag == "Player")
        {
            isPlayer = false;
            speedFloat = 10;
            gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = true;
        }

        if(other.gameObject.CompareTag("Enemy"))
        {
            EnemyOutOfRange();
        }
    }

    protected void EnemyInRange()
    {
        isEnemy = true;
        speedFloat = 0;
        anim.gameObject.GetComponent<Animator>().SetBool("enemyF", true);
        transform.LookAt(GameObject.FindWithTag("Enemy").transform.position);
    }

    protected void EnemyOutOfRange()
    {
        isEnemy = false;
        speedFloat = 10;
        anim.gameObject.GetComponent<Animator>().SetFloat("walk", 5);
        anim.gameObject.GetComponent<Animator>().SetBool("enemyF", false);
    }
}
