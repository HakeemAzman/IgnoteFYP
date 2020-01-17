using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverlapSphereAOE : MonoBehaviour
{
    public CompanionScript compScript;
    private EnemyHealth ehScript;
    public GameObject deathParticle;
    Collider[] colliders;
    public Transform arms;
    public AudioClip enemyDeathSFX;

    public void AreaOfEffect()
    {
        colliders = Physics.OverlapSphere(arms.position, compScript.radius, compScript.check);
        foreach (Collider enemy in colliders)
        {
            StartCoroutine(SpawnVFX());

            if (enemy.CompareTag("Enemy"))
            {
                enemy.gameObject.GetComponent<EnemyHealth>().enemy_Health -= compScript.damage;

                if(enemy.gameObject.GetComponent<EnemyHealth>().enemy_Health <= 0)
                {
                    compScript.isEnemy = false;
                    compScript.speedFloat = 10;

                    AudioSource.PlayClipAtPoint(enemyDeathSFX, enemy.gameObject.transform.position);

                    GameManager.enemyScore += enemy.gameObject.GetComponent<EnemyHealth>().score;
                    GameObject deathVFX = Instantiate(deathParticle, transform.position, transform.rotation);
                    Destroy(deathVFX, 1.5f);

                    Destroy(enemy.gameObject, 1.6f);
                }
            }
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
