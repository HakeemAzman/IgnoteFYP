using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneActivate : MonoBehaviour
{
    public GameObject obs;
    public float cutsceneLengthInSecs;

    bool cutsceneActivated;
    public GameObject player;
    public GameObject cinemachineCutCams;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            obs.SetActive(true);
            StartCoroutine(WaitForCutscene());
        }
    }

    IEnumerator WaitForCutscene()
    {
        player.GetComponent<PlayerMovement>().player_SetSpeed = 0;
        yield return new WaitForSeconds(cutsceneLengthInSecs);
        player.GetComponent<PlayerMovement>().player_SetSpeed = 8;
        cinemachineCutCams.SetActive(false);
        Destroy(this.gameObject);
    }
}
