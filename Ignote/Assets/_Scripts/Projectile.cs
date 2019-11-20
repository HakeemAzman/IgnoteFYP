using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{

    protected Rigidbody rb;
    public float speed = 6f;

    // Homing functionality.
    public bool isHoming = false;
    public Transform target;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * speed;
    }

    // Update is called once per frame
    public virtual void Update()
    {
        if(isHoming && target)
        {
            transform.rotation = Quaternion.LookRotation(target.position - transform.position);
            rb.velocity = transform.forward * speed;
        }
    }
}
