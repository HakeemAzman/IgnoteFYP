using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooterHealth : EnemyHealth
{
    public EnemyShooterCombat esC;

    protected override void Start()
    {
        base.Start();
        esC.GetComponent<EnemyShooterCombat>();
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Wrench")
        {
            gameObject.GetComponent<EnemyShooterCombat>().enabled = false;
            StartCoroutine("Reactivate");
        }

        if (other.gameObject.tag == "Projectile")
        {
            enemy_Health -= 40;
        }
    }

    public override IEnumerator Reactivate()
    {
        yield return new WaitForSeconds(3);
        gameObject.GetComponent<EnemyShooterCombat>().enabled = true;
    }
}


