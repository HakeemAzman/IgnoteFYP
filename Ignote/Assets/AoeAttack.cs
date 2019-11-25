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

    // Start is called before the first frame update
    void Start()
    {
        
        // eH = GameObject.FindWithTag("Enemy").GetComponent<EnemyHealth>();
        cs.GetComponent<CompanionScript>();
    }

    // Update is called once per frame
    void Update()
    {
        Physics.IgnoreCollision(Player.GetComponent<Collider>(), gameObject.GetComponent<Collider>());
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Terrain")
        {
            areaofEffect();
        }
    }

    void areaofEffect()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider nearbyEnemy in colliders)
        {
            if(nearbyEnemy.tag == "Enemy")
            {
                cs.charges -= 1;
                nearbyEnemy.gameObject.GetComponent<EnemyHealth>().enemy_Health -= damage;
                nearbyEnemy.gameObject.GetComponent<EnemyShooterHealth>().enemy_Health -= damage;
                
            }
            //Rigidbody rb = nearbyEnemy.GetComponent<Rigidbody>();
            //if (rb != null)
            //{
            //    //rb.AddExplosionForce(kbForce, transform.position, radius);
            //    //eH = GameObject.FindWithTag("Enemy").GetComponent<EnemyHealth>();
            //    //GameObject.FindGameObjectsWithTag("Enemy");
            //    //eH.enemy_Health -= 1000;
            //}
        }
    }
}
