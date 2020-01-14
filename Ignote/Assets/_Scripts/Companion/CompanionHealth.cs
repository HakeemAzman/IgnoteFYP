using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CompanionHealth : MonoBehaviour
{
    public Companion_Commands ccScript;
    public float rateOfRepair;

    public float companionHealth;
    public float companionCurrentHealth;
    public CompanionScript cs;
    public Animator anim;
    public Slider enduranceBar;
    public bool companionDisabled;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        enduranceBar.value = companionCurrentHealth;

        if(ccScript.isRepairing)
        {
            companionCurrentHealth += rateOfRepair * Time.deltaTime;

            if(companionCurrentHealth > companionHealth)
            {
                companionCurrentHealth = companionHealth;
            }
        }

        if(!ccScript.isRepairing && companionCurrentHealth >= 1)
        {
            RobotRestartSystem();
        }

        if (companionCurrentHealth <= 0)
        {
            RobotDeathFunctions();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "EProjectile")
        {
            companionCurrentHealth -= 5f;
            Destroy(other.gameObject);
        }
    }

    void RobotRestartSystem()
    {
        anim.SetBool("isDisabled", false);
        cs.gameObject.GetComponent<CompanionScript>().enabled = true;
        companionDisabled = true;
    }

    void RobotDeathFunctions()
    {
        companionDisabled = true;
        anim.gameObject.GetComponent<Animator>().SetFloat("wSpeed", 0);
        anim.gameObject.GetComponent<Animator>().SetBool("enemyF", false);
        anim.gameObject.GetComponent<Animator>().SetBool("isDisabled", true);
        cs.speedFloat = 0;
        companionCurrentHealth = 0;
        cs.gameObject.GetComponent<CompanionScript>().enabled = false;
        anim.SetBool("isDisabled", true);
    }
}
