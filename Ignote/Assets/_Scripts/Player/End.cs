using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class End : MonoBehaviour
{
    public GameObject[] deadComps;
    public GameObject[] standingComps;
    public GameObject endGameCutscene, endWrench, playerWrench;

    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("End"))
        {
            if (Input.GetButtonDown("Stay"))
            {
                endGameCutscene.SetActive(true);
                endWrench.SetActive(true);
                playerWrench.SetActive(false);

                StartCoroutine(EndGame());

                deadComps[0].SetActive(false);
                deadComps[1].SetActive(false);
                deadComps[2].SetActive(false);
                deadComps[3].SetActive(false);
                deadComps[4].SetActive(false);
                deadComps[5].SetActive(false);

                standingComps[0].SetActive(true);
                standingComps[1].SetActive(true);
                standingComps[2].SetActive(true);
                standingComps[3].SetActive(true);
                standingComps[4].SetActive(true);
                standingComps[5].SetActive(true);
            }
        }
    }

    IEnumerator EndGame()
    {
        yield return new WaitForSeconds(75f);
        SceneManager.LoadScene(0);
    }
}
