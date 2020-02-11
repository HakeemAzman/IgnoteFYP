using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverlapSphereAOE : MonoBehaviour
{
    public CompanionScript compScript;
    private EnemyHealth ehScript;
    //public GameObject deathParticle;

    Collider[] colliders;
    public Transform arms;
    public AudioClip enemyDeathSFX;
    
    public int pooledAmount = 3;
    List<GameObject> m_AOEStorage;


    private void Start()
    {
        m_AOEStorage = new List<GameObject>();
        for (int i = 0; i < pooledAmount; i++)
        {
            GameObject obj = (GameObject)Instantiate(compScript.psAOE);
            m_AOEStorage.Add(obj);
            obj.SetActive(false);
            GameObject.DontDestroyOnLoad(obj);
        }
    }

    public void AreaOfEffect()
    {
        colliders = Physics.OverlapSphere(arms.position, compScript.radius, compScript.check);
        foreach (Collider enemy in colliders)
        {
            //StartCoroutine(SpawnVFX());
            AoeVFX();

            if (enemy.CompareTag("Enemy"))
            {
                enemy.gameObject.GetComponent<EnemyHealth>().enemy_Health -= compScript.damage;

                if(enemy.gameObject.GetComponent<EnemyHealth>().enemy_Health <= 0)
                {
                    compScript.isEnemy = false;
                    compScript.speedFloat = 10;

                    AudioSource.PlayClipAtPoint(enemyDeathSFX, enemy.gameObject.transform.position);

                    GameManager.enemyScore += enemy.gameObject.GetComponent<EnemyHealth>().score;

                    enemy.gameObject.GetComponent<EnemyHealth>().deathParticle.Play();

                    //GameObject deathVFX = Instantiate(deathParticle, transform.position, transform.rotation);
                    //Destroy(deathVFX);

                    Destroy(enemy.gameObject, 0.5f);
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

    void AoeVFX()
    {
        for (int i = 0; i < m_AOEStorage.Count; i++)
        { // Iterate through all pooled objects
            if (!m_AOEStorage[i].activeInHierarchy)
            {
                m_AOEStorage[i].transform.position = arms.position;
                m_AOEStorage[i].transform.rotation = arms.rotation;
                m_AOEStorage[i].SetActive(true);
                break;
            }
        }
    }
}
