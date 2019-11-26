using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverlapSphereAOE : MonoBehaviour
{
    public CompanionScript compScript;
    private EnemyHealth ehScript;
    Collider[] colliders;
    public Transform arms;

    public void AreaOfEffect()
    {
        colliders = Physics.OverlapSphere(arms.position, compScript.radius, compScript.check);
        foreach (Collider enemy in colliders)
        {
            print(enemy.name);
            compScript.charges -= 1;

            if (enemy.CompareTag("Enemy"))
                enemy.gameObject.GetComponent<EnemyHealth>().enemy_Health -= compScript.damage;
        }
        print("Active");
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(arms.position, compScript.radius);
    }
}
