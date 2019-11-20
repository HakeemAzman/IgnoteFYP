using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public Animator planeAnim;

    public void StartGame()
    {
        planeAnim.GetComponent<Animator>().SetBool("canFlyForward", true);

        if(planeAnim.GetComponent<Animator>().GetBool("canFlyForward") == true)
        {
            planeAnim.GetComponent<Animator>().Play("FlyForward");

            StartCoroutine(StartGameAnims());
        }
    }

    IEnumerator StartGameAnims()
    {
        yield return new WaitForSeconds(5f);

        SceneManager.LoadScene("Level 1");
    }
}
