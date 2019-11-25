using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverlapSphereAOE : MonoBehaviour
{
    public CompanionScript compScript;
    private EnemyHealth ehScript;
    Collider[] colliders;

    // Start is called before the first frame update
    void Start()
    {
        //compScript = GetComponentInParent<CompanionScript>();
        
    }
    

    public void AreaOfEffect()
    {
        colliders = Physics.OverlapSphere(transform.position, compScript.radius);
        foreach (var enemy in colliders)
        {
            print(enemy.name);
            compScript.charges -= 1;
            enemy.gameObject.GetComponent<EnemyHealth>().enemy_Health -= compScript.damage;
            enemy.gameObject.GetComponent<EnemyShooterHealth>().enemy_Health -= compScript.damage;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, compScript.radius);
    }
}
