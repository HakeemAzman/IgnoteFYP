using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Compbat : MonoBehaviour
{
    public CompanionScript cs;
    public CompanionHealth ch;
    public OverlapSphereAOE aoeScript;
    public Animator anim;
    public bool isEnemy;
    // Start is called before the first frame update

    void Start()
    {
        cs.GetComponent<CompanionScript>();
        ch.GetComponent<CompanionHealth>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isEnemy)
        {
            anim.SetBool("enemyF", false);
            anim.gameObject.GetComponent<Animator>().SetFloat("walk", 5);
        }

        if (!cs.haveEnemy)
        {
            anim.gameObject.GetComponent<Animator>().SetBool("enemyF", false);
        }
    }

    public void EnemyInRange()
    {
        StartCoroutine(Activate());
        isEnemy = true;
        cs.speedFloat = 0;
        anim.gameObject.GetComponent<Animator>().SetBool("enemyF", true);
        transform.LookAt(GameObject.FindWithTag("Enemy").transform.position);
    }

    public void EnemyOutOfRange()
    {
        StopCoroutine(Activate());
        isEnemy = false;
        cs.speedFloat = 5;
        anim.gameObject.GetComponent<Animator>().SetFloat("walk", 5);
        anim.gameObject.GetComponent<Animator>().SetBool("enemyF", false);
    }

    IEnumerator Activate()
    {
        yield return new WaitForSeconds(1f);
        aoeScript.AreaOfEffect();
    }
}
