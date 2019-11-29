using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLibrary : MonoBehaviour
{
    [SerializeField] GameObject LevelLibrary;
    [SerializeField] GameObject zoomInTrigger;

    private void Awake()
    {
        zoomInTrigger.SetActive(false);
    }

    private void OnTriggerStay (Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            LevelLibrary.GetComponent<Animator>().Play("CamLevelLibrary");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            zoomInTrigger.SetActive(true);
        }
    }
}
