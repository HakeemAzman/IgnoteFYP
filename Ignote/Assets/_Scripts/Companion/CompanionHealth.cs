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
    public Image enduranceBar;
    public BoxCollider leftArm;
    public BoxCollider rightArm; 
    public GameObject shieldSphere;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        companionHealth = 1000;
    }

    // Update is called once per frame
    void Update()
    {

        
        enduranceBar.fillAmount = companionCurrentHealth / companionHealth;
        if (companionHealth <= 0)
        {
            leftArm.gameObject.GetComponent<BoxCollider>().enabled = false;
            rightArm.gameObject.GetComponent<BoxCollider>().enabled = false;
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
        leftArm.gameObject.GetComponent<BoxCollider>().enabled = true;
        rightArm.gameObject.GetComponent<BoxCollider>().enabled = true;
        cs.gameObject.GetComponent<CompanionScript>().enabled = true;
        compS.gameObject.GetComponent<Compbat>().enabled = true;
        cs.speedFloat = 5;
        companionHealth = 1000;
    }
}
