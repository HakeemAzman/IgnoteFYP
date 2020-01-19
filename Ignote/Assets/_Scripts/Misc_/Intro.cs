using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intro : MonoBehaviour
{
    public GameObject introCutscene, introPlane, playerChar, playerCharUI;
    float timer = 23f;
    public bool startIntro = false;
    public AudioSource asPlane;
    public AudioClip wind, arrowHit, parachuteDeploy, planeCrash, playerLand;

    private void Start()
    {
        StartCoroutine(PlaneIntro());
    }

    // Update is called once per frame
    void Update()
    {
        if(!startIntro)
        {
            introCutscene.SetActive(false);
            introPlane.SetActive(false);
            playerChar.SetActive(true);
            playerCharUI.SetActive(true);
        }

        if(startIntro)
        {
            playerChar.SetActive(false);
            playerCharUI.SetActive(false);

            timer -= Time.deltaTime;

            if(timer <= 2.2f)
                introCutscene.SetActive(false);

            if (timer <= 1f)
            {
                introPlane.SetActive(false);
                playerChar.SetActive(true);
                playerCharUI.SetActive(true);
            }

            if (timer <= -3f) Destroy(this.gameObject);
        }
    }

    IEnumerator PlaneIntro()
    {
        yield return new WaitForSeconds(6.2f);
        asPlane.PlayOneShot(arrowHit);
        yield return new WaitForSeconds(3f);
        asPlane.PlayOneShot(parachuteDeploy);
        yield return new WaitForSeconds(5.5f);
        asPlane.PlayOneShot(planeCrash);
        yield return new WaitForSeconds(6f);
        asPlane.PlayOneShot(playerLand);
    }
}
