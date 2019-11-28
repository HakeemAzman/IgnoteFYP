using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLibrary : MonoBehaviour
{
    [SerializeField] GameObject LevelLibrary;

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
            LevelLibrary.GetComponent<Animator>().Play("CamLevelLibraryExit");
        }
    }
}
