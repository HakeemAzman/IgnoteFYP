using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftScript : MonoBehaviour
{
    public Animator liftAnim;
    // Start is called before the first frame update
    void Start()
    {
        liftAnim.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            liftAnim.GetComponent<Animator>().SetBool("isDown", true);
        }
    }

    //private void OnTriggerExit(Collider other)
    //{
    //    if(other.gameObject.tag == "Player")
    //    {
    //        liftAnim.GetComponent<Animator>().SetBool("isDown", false);
    //    }
    //}
}
