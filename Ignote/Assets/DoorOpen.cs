using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    public AudioSource aS;


    void doorOpen()
    {
        aS.Play();
    }
}
