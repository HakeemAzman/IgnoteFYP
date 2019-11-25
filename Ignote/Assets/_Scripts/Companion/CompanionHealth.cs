using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CompanionHealth : MonoBehaviour
{
    public float companionHealth;
    public float companionCurrentHealth;
    public CompanionScript cs;
    public Compbat compS;
    public Animator anim;
    public Slider enduranceBar;

    // Start is called before the first frame update
    void Start()
    {
        companionHealth = 1000;
        companionCurrentHealth = companionHealth;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        enduranceBar.value = companionCurrentHealth;
        if (companionHealth <= 0)
        {
            anim.gameObject.GetComponent<Animator>().SetFloat("wSpeed", 0);
            anim.gameObject.GetComponent<Animator>().SetBool("enemyF", false);
            anim.gameObject.GetComponent<Animator>().SetBool("isDisabled", true);
            cs.speedFloat = 0;
            companionHealth = 0;
            cs.gameObject.GetComponent<CompanionScript>().enabled = false;
            compS.gameObject.GetComponent<Compbat>().enabled = false;
            anim.SetBool("isDisabled", true);
            StartCoroutine(bootup());
        }
    }

    IEnumerator bootup()
    {
        yield return new WaitForSeconds(10);
        anim.SetBool("isDisabled", false);
        cs.gameObject.GetComponent<CompanionScript>().enabled = true;
        compS.gameObject.GetComponent<Compbat>().enabled = true;
        cs.speedFloat = 5;
        companionHealth = 1000;
    }
}
