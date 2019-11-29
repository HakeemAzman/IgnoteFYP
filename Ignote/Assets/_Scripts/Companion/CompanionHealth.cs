using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CompanionHealth : MonoBehaviour
{
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
        companionCurrentHealth = (float)companionHealth;
        enduranceBar.value = companionCurrentHealth;

        if (companionHealth <= 0)
        {
            companionDisabled = true;
            anim.gameObject.GetComponent<Animator>().SetFloat("wSpeed", 0);
            anim.gameObject.GetComponent<Animator>().SetBool("enemyF", false);
            anim.gameObject.GetComponent<Animator>().SetBool("isDisabled", true);
            cs.speedFloat = 0;
            companionHealth = 0;
            cs.gameObject.GetComponent<CompanionScript>().enabled = false;
            anim.SetBool("isDisabled", true);
            StartCoroutine(bootup());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "EProjectile")
        {
            companionCurrentHealth -= 2f;
        }
    }

    IEnumerator bootup()
    {
        yield return new WaitForSeconds(10);
        anim.SetBool("isDisabled", false);
        cs.gameObject.GetComponent<CompanionScript>().enabled = true;
        cs.speedFloat = 5;
        companionDisabled = true;
        companionHealth = 100;
    }
}
