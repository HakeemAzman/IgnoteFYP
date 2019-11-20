using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraBridge : MonoBehaviour
{
    public Animator anim;

    [SerializeField] GameObject playerSkirt, bridgeOffset;

    public GameObject mainCinemachine;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            bridgeOffset.GetComponent<Animator>().Play("StartBridgeOffset");
            mainCinemachine.GetComponent<CinemachineVirtualCamera>().LookAt = bridgeOffset.transform;
            anim.gameObject.GetComponent<Animator>().Play("CameraPanDown");
        }
    }

    private void OnTriggerExit(Collider exit)
    {
        if(exit.gameObject.tag == "Player")
        {
            bridgeOffset.GetComponent<Animator>().Play("EndBridgeOffSet");
            mainCinemachine.GetComponent<CinemachineVirtualCamera>().LookAt = bridgeOffset.transform;
            anim.gameObject.GetComponent<Animator>().Play("CameraPanUp");
        }
    }
}
