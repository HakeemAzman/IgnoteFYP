using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTurretExit : MonoBehaviour
{
    [SerializeField] GameObject cameraTurretExit;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            cameraTurretExit.GetComponent<Animator>().Play("CamTurretExit");
        }
    }
}
