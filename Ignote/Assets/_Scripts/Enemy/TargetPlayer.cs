using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class TargetPlayer : MonoBehaviour
{
    public float m_timeBetweenAttacks = 1f;
    public float m_damageOutput; //To be changed in the inspector

    public float m_distanceFromPlayer;
    [SerializeField] float offset;

    public float timeSinceLastAttack = Mathf.Infinity;
    [SerializeField] bool onCooldown = false;
    [SerializeField] float cooldownTimer;
    //Transform t;
    public GameObject smokeVFX;
    public Transform BallistaPos;
    Color green = Color.green;
    Color red = Color.red;

    public AudioClip ChargeShoot;
    public AudioSource aS;

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

    public GameObject m_projectileGO;
    public Rigidbody m_projectileRB;
    public Transform m_fireTransform;
    public float m_launchForce;
    public int pooledAmount = 2;
    List<GameObject> projectiles;

    public GameObject m_laser;
    EnemyShooterCombat escScript;

    // Start is called before the first frame update
    void Start()
    {
        projectiles = new List<GameObject>();
        for (int i = 0; i < pooledAmount; i++)
        {
            GameObject obj = (GameObject)Instantiate(m_projectileGO);
            projectiles.Add(obj);
            obj.SetActive(false);
            GameObject.DontDestroyOnLoad(obj);
        }

        escScript = GetComponentInChildren<EnemyShooterCombat>();
        targetPlayer = PlayerManager.instance.player.transform;
        targetCompanion = PlayerManager.instance.companion.transform;
        playerHealth = targetPlayer.GetComponent<PlayerHealth>();
        companionHealth = targetCompanion.GetComponent<CompanionHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        // t.eulerAngles = new Vector3(t.transform.position.x, t.transform.position.y, -90);

        //print(timeSinceLastAttack);
        //enemyTransform = GameObject.FindWithTag("Enemy").transform;
        //Debug.Log(enemyTransform);
        timeSinceLastAttack += Time.deltaTime;

        if (!onCooldown)
        {
            AttackPlayer();
            //AttackCompanion();

            Aim();
        }
    }

    //public void attackEnemy()
    //{
    //    FaceTarget(enemyTransform);
    //    Fire(enemyTransform);
    //}

    public void AttackPlayer()
    {
        //the distance between the player and the enemy.
        float distFromPlayer = Vector3.Distance(targetPlayer.position, transform.position);

        //if the desired distance between the enemy and player is met
        //or when the player is in enemy's attack range, enemy will rotate to face the player.
        if (distFromPlayer <= m_distanceFromPlayer)
        {
            //transform.LookAt(targetPlayer);
            FaceTarget(targetPlayer);
            Fire(targetPlayer);
        }
    }

    //public void AttackCompanion()
    //{
    //    //the distance between the bot and the enemy.
    //    float distanceFromCompanion = Vector3.Distance(targetCompanion.position, transform.position);

    //    //if the desired distance between the enemy and bot is met
    //    //or when the bot is in enemy's attack range, enemy will rotate to face the bot.
    //    if (distanceFromCompanion <= 10)
    //    {
    //        FaceTarget(targetCompanion);
    //        Fire(targetCompanion);
    //    }
    //}

    void Aim()
    {
        if ((timeSinceLastAttack >= 1f) && (timeSinceLastAttack <= 5f))
        {
            chargingShot();
            m_laser.SetActive(true);
            m_laser.GetComponent<Renderer>().material.SetColor("_Color", green);
        }
        else if ((timeSinceLastAttack >= 5f) && (timeSinceLastAttack <= 7f))
        {
            m_laser.GetComponent<Renderer>().material.SetColor("_Color", red);
        }
        else
        {
            m_laser.SetActive(false);
        }
    }

    void FaceTarget(Transform target)
    {
        Vector3 directionToTarget = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(directionToTarget.x, directionToTarget.y, directionToTarget.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    protected void Fire(Transform target)
    {
        Vector3 directionToTarget = (target.position - transform.position).normalized;
        float angle = Vector3.Angle(directionToTarget, transform.forward);
        //Mathf.Abs(angle) < 40 &&
        if (Mathf.Abs(angle) < 40 && timeSinceLastAttack > m_timeBetweenAttacks)
        {
            timeSinceLastAttack = 0;

            //Rigidbody shellInstance = Instantiate(m_projectileRB, m_fireTransform.position, m_fireTransform.rotation);

            //shellInstance.velocity = shellInstance.transform.forward * m_launchForce;


            for (int i = 0; i < projectiles.Count; i++)
            { // Iterate through all pooled objects
                if (!projectiles[i].activeInHierarchy)
                {
                    projectiles[i].transform.position = m_fireTransform.position;
                    projectiles[i].transform.rotation = m_fireTransform.rotation;
                    Rigidbody shellInstance = projectiles[i].GetComponent<Rigidbody>();
                    shellInstance.velocity = shellInstance.transform.forward * m_launchForce;
                    projectiles[i].SetActive(true);
                    break;
                }
            }

            StartCoroutine(cooldown());
        }
    }

    protected IEnumerator cooldown()
    {
        onCooldown = true;
        yield return new WaitForSeconds(cooldownTimer);
        onCooldown = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wrench"))
        {
            GameObject SmokedVFX = (GameObject)Instantiate(smokeVFX, new Vector3(BallistaPos.transform.position.x, BallistaPos.transform.position.y - offset, BallistaPos.transform.position.z), BallistaPos.transform.rotation);
            GameManager.enemyScore++;
            gameObject.GetComponent<BoxCollider>().enabled = false;
            this.gameObject.GetComponent<TargetPlayer>().enabled = false;
        }
    }

    void chargingShot()
    {
        aS.Play();
    }
}
