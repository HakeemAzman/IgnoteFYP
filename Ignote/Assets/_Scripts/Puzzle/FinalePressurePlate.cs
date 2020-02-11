using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalePressurePlate : MonoBehaviour
{
    public GameObject animLeftDoor;
    public GameObject animRightDoor;
    public GameObject playerHealthUI, robotHealthUI, endCanvas, robotCompanion, appearObj;
    public GameObject deadRobot;
    [SerializeField] bool isTriggered;
    [SerializeField] Gate6E g6eScript;

    // Update is called once per frame
    void Update()
    {
        if(isTriggered && g6eScript.isTriggered)
        {
            animLeftDoor.GetComponent<Animator>().enabled = true;
            animRightDoor.GetComponent<Animator>().enabled = true;
            StartCoroutine(StartCinematic());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Companion")) isTriggered = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Companion")) isTriggered = false;
    }

    IEnumerator StartCinematic()
    {
        playerHealthUI.SetActive(false);
        robotHealthUI.SetActive(false);
        appearObj.SetActive(true);
        yield return new WaitForSeconds(1f);
        robotCompanion.SetActive(false);
        endCanvas.SetActive(true);
        yield return new WaitForSeconds(3f);
        deadRobot.SetActive(true);
    }
}
