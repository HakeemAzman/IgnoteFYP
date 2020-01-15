using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoungeEnter : MonoBehaviour
{
    [SerializeField] GameObject loungeCamera;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            loungeCamera.GetComponent<Animator>().Play("LoungeEnter");
        }
    }
}
