using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooterHealth : EnemyHealth
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Wrench") //Add power down animation or smoke VFX to indicate its disabled
        {
            gameObject.GetComponent<EnemyShooterCombat>().enabled = false;
        }
    }
}


