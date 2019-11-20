using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DurableProjectile : Projectile
{
    public override void Update()
    {
        //base.Update();

        // Close to the target tracker.
        if(Vector3.Distance(target.position,transform.position) < 100)
        {
            // make the bullet glow.
        }
    }
}
