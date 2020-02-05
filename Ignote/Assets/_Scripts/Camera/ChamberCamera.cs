using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChamberCamera : MonoBehaviour
{
    [SerializeField] GameObject chamberCam;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            chamberCam.GetComponent<Animator>().Play("Chambers");
        }
    }
}
