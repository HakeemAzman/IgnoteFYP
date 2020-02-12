using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pressurepress : MonoBehaviour
{
    public Animator anim;
    //public AudioClip pressurePlateSound;
    public AudioSource As;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" || other.gameObject.tag == "Companion")
        {
            anim.SetTrigger("pressed");
        }
    }

    void pressureSound()
    {
        As.Play();
    }
}
