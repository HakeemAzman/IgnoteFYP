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
            StartCoroutine(SpawnVFX());

            if (enemy.CompareTag("Enemy"))
                enemy.gameObject.GetComponent<EnemyHealth>().enemy_Health -= compScript.damage;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(arms.position, compScript.radius);
    }

    IEnumerator SpawnVFX()
    {
        GameObject aoeVFX = Instantiate(compScript.psAOE, arms.position, transform.rotation);
        yield return new WaitForSeconds(3f);
        Destroy(aoeVFX);
    }
}
