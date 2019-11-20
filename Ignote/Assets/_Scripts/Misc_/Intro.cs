using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intro : MonoBehaviour
{
    public GameObject introCutscene, introPlane, playerChar, playerCharUI;
    float timer = 23f;
    public bool startIntro = false;

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
}
