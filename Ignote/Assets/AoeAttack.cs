using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AoeAttack : MonoBehaviour
{
    public float radius;
    public float kbForce;
    public int damage = 30;
    public CompanionScript cs;
    public GameObject Player;
    //public EnemyHealth eH;

    //public LayerMask affectedTargets;
    //public bool active = false;

    // Start is called before the first frame update
    void Start()
    {
        // eH = GameObject.FindWithTag("Enemy").GetComponent<EnemyHealth>();
        cs.GetComponent<CompanionScript>();
    }

    // Update is called once per frame
    void Update()
    {
        //Physics.IgnoreCollision(Player.GetComponent<Collider>(), gameObject.GetComponent<Collider>());
        //AreaOfEffect();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            print(other.name);
            // AreaOfEffect();
            cs.charges -= 1;
            other.gameObject.GetComponent<EnemyHealth>().enemy_Health -= damage;
            other.gameObject.GetComponent<EnemyShooterHealth>().enemy_Health -= damage;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            
        }
    }

    public void AreaOfEffect()
    {
       // Collider[] terrainCollider = Physics.OverlapSphere(transform.position, radius, affectedTargets);
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        //foreach(Collider terrain in terrainCollider )
        //{
        //    if (terrain.CompareTag("Terrain"))
        //    {
        //        cs.charges -= 1;
        //        terrain.gameObject.GetComponent<EnemyHealth>().enemy_Health -= damage;
        //        terrain.gameObject.GetComponent<EnemyShooterHealth>().enemy_Health -= damage;
        //    }
        //}

        foreach (Collider nearbyEnemy in colliders)
        {
            cs.charges -= 1;
            nearbyEnemy.gameObject.GetComponent<EnemyHealth>().enemy_Health -= damage;
            nearbyEnemy.gameObject.GetComponent<EnemyShooterHealth>().enemy_Health -= damage;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,radius);
    }
}
