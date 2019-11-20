using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Companion : MonoBehaviour
{
    public Transform player;
    public Transform myTransform;
    public float followSpeed;
    public float unfollowSpeed;
    public GameObject MountComp;
    public bool canDismount;
    Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

  
    void Update() 
    {
        follow(); //Calls the follow function 

        
    }

    private void OnTriggerEnter(Collider other) //Stops before reaching the player so it's not directly behind the player.
    {
        if(other.gameObject.tag == "Player")
        {
            followSpeed = 0;
            rb.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
            
        }
    }

    private void OnTriggerExit(Collider other) //Starts following when the player is too far again.
    {
        if(other.gameObject.tag == "Player")
        {
            followSpeed = 2f;
        }


    }

    void follow() //Allows the companion to follow the player and look at the player constantly.
    {
        transform.LookAt(player.transform);
        myTransform.Translate(Vector3.forward * followSpeed * Time.deltaTime);
    }
}
