using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class GirlAnimEventSound : MonoBehaviour
{
    public AudioClip walking;
    public AudioClip repairing;
    public AudioSource aS;
    public GameObject repairEffect;
    public PlayerMovement pMovement;
    public Transform wrenchPos;
    // Start is called before the first frame update


    void girlWalking()
    {
        aS.PlayOneShot(walking);
    }

    void girlRepair()
    {
        if (pMovement.isCompanion)
        {
            GameObject repairVFX = (GameObject)Instantiate(repairEffect, wrenchPos.transform.position, wrenchPos.transform.rotation);
            Destroy(repairVFX, 1);
            aS.PlayOneShot(repairing);
        }
        else
        {
            return;
        }
        
    }

}
