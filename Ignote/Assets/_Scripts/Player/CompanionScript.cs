using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class CompanionScript : MonoBehaviour
{
    private NavMeshAgent agent;
    private Vector3 playerAI;
    private Animator anim;
    [Space]
    [Header("Scripts")]
    public Companion_Commands cc;
    public CompanionHealth ch;
    public Compbat cbScript;
    [Space]
    public float speedFloat;
    public float charges;
    [Space]
    [Header("Bool")]
    public bool canSlam;
    public bool isPlayer;
    public bool haveEnemy;
    public bool isWandering;
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

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        cc.GetComponent<Companion_Commands>();
        ch.GetComponent<CompanionHealth>();
        cbScript.GetComponent<Compbat>();
    }

    void Update()
    {
        //if (!isPlayer && !haveEnemy)
        //{
        //    speedFloat = 0;
        //    anim.SetFloat("wSpeed", 0);
        //}

        if (charges >= 1)
        {
            damage = 100;
            //speedFloat = 10;
        }
        if (charges >= 3)
        {
            charges = 3;
        }
        if(charges <= 0)
        {
            Overcharge.SetActive(false);
            charges = 0;
        }

        if(charges <= 0)
        {
            damage = 30;
        }
       
        playerAI = FindClosestPlayer().transform.position;
        agent.destination = playerAI;
        gameObject.GetComponent<NavMeshAgent>().speed = speedFloat;

        float dist = Vector3.Distance(transform.position, GameObject.FindWithTag("Enemy").transform.position);
        
        if(dist <= 20)
        {
            enemyInSight = true;
        }

        if(dist > 20)
        {
            haveEnemy = false;

            enemyInSight = false;
        }
        //if (dist >= 25)
        //{
        //    canSlam = true;
        //    speedFloat = 10;
        //}

        if (Input.GetKeyDown(KeyCode.Space))
        {
            ch.companionHealth -= 1;
        }

        //print(DistanceToPlauer());
        //print(DistanceToEnemy());

        if(!haveEnemy)
        {
            if (DistanceToPlauer() >= 3 && DistanceToPlauer() <= 5)
            {
                speedFloat = 0;
                anim.SetFloat("wSpeed", 0);
                isPlayer = true;
            }

            if (DistanceToPlauer() > 5)
            {
                isPlayer = false;
                speedFloat = 5;
                gameObject.GetComponent<NavMeshAgent>().enabled = true;
            }
        }

        if (haveEnemy && DistanceToEnemy() <= DistanceToPlauer())
        {
            cbScript.EnemyInRange();
        }
        else if (DistanceToEnemy() >= DistanceToPlauer())
        {
            cbScript.EnemyOutOfRange();
        }
    }

    GameObject FindClosestPlayer()
    {
        GameObject[] eTargets;
        GameObject[] targets;

        eTargets = GameObject.FindGameObjectsWithTag("Enemy");
        targets = GameObject.FindGameObjectsWithTag("Player");

        if (enemyInSight && eTargets.Length >= 1)
        {
            haveEnemy = true;
            targets = GameObject.FindGameObjectsWithTag("Enemy");
        }

        //if (targets.Length == 0 && isWandering)
        //{
        //    targets = GameObject.FindGameObjectsWithTag("Interest");
        //}

        if (eTargets.Length == 0 && !isWandering)
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

        if (speedFloat >= 5)
        {
            anim.SetFloat("wSpeed", 5);
        }

        else if (speedFloat <= 0 && isPlayer)
        {
            anim.SetFloat("wSpeed", 0);
        }
        return closestPlayer;

    }
    private void OnTriggerStay(Collider other) //Stops before reaching the player so it's not directly behind the player.
    {
        if (other.gameObject.tag == "Player")
        {
            speedFloat = 0;
            anim.SetFloat("wSpeed", 0);
            isPlayer = true;
        }

        else if (other.gameObject.tag == "Enemy")
        {
            isPlayer = false;
        }
        
        //if (!haveEnemy)
        //{
        //    Player.gameObject.GetComponent<BoxCollider>().enabled = true;
        //}

        //else if(haveEnemy)
        //{
        //    Player.gameObject.GetComponent<BoxCollider>().enabled = false;
        //}

        if (other.gameObject.tag == "Point1")
        {
            StartCoroutine("interacting");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Wrench")
        {
            Overcharge.SetActive(true);
            charges += 1;
        }
    }

    //private void OnTriggerExit(Collider other) //Starts following when the player is too far again.
    //{
    //    if (other.gameObject.tag == "Player")
    //    {
    //        isPlayer = false;
    //        speedFloat = 5;
    //        gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = true;
    //    }
    //}

    public void ShieldBash()
    {
        float dist = Vector3.Distance(gameObject.transform.position, GameObject.FindWithTag("Enemy").transform.position);
    }

    IEnumerator interacting()
    {
        yield return new WaitForSeconds(5);
        //Play animation here to show interactivity
        isWandering = false;
    }
    
    private float DistanceToPlauer()
    {
        return Vector3.Distance(transform.position, Player.transform.position);
    }

    private float DistanceToEnemy()
    {
        return Vector3.Distance(transform.position, GameObject.FindWithTag("Enemy").transform.position);
    }
}
