using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLibraryExit : MonoBehaviour
{
    [SerializeField] GameObject LevelLibrary;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            LevelLibrary.GetComponent<Animator>().Play("CamLevelLibraryExit");
        }
    }
}
