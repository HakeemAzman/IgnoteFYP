using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTurret : MonoBehaviour
{
    [SerializeField] GameObject CameraTurretEnter;
    [SerializeField] GameObject cameraTurretExitTrigger;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            CameraTurretEnter.GetComponent<Animator>().Play("CamTurret");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            cameraTurretExitTrigger.SetActive(true);
        }
    }
}
