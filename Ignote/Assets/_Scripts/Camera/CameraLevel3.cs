using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLevel3 : MonoBehaviour
{
    [SerializeField] GameObject Level3Cam;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Level3Cam.GetComponent<Animator>().Play("CamLevel3");
        }
    }
}
