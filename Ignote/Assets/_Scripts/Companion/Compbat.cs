using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Compbat : MonoBehaviour
{
    public CompanionScript cs;
    public CompanionHealth ch;
    public Animator anim;
    public TrailRenderer attackVFX;
    public TrailRenderer attackVFX2;
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
            attackVFX.gameObject.GetComponent<TrailRenderer>().enabled = false;
            attackVFX2.gameObject.GetComponent<TrailRenderer>().enabled = false;
            anim.gameObject.GetComponent<Animator>().SetFloat("walk", 5);
        }

        if (!cs.haveEnemy)
        {
            anim.gameObject.GetComponent<Animator>().SetBool("enemyF", false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            isEnemy = true;
            cs.speedFloat = 0;
            anim.gameObject.GetComponent<Animator>().SetBool("enemyF", true);
            attackVFX.gameObject.GetComponent<TrailRenderer>().enabled = true;
            attackVFX2.gameObject.GetComponent<TrailRenderer>().enabled = true;
            transform.LookAt(GameObject.FindWithTag("Enemy").transform.position);
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            isEnemy = false;
            cs.speedFloat = 5;
            anim.gameObject.GetComponent<Animator>().SetFloat("walk", 5);
            anim.gameObject.GetComponent<Animator>().SetBool("enemyF", false);
        }
    }


}
