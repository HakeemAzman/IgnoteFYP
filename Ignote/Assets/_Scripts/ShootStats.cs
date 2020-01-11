using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootStats : MonoBehaviour
{

    public float shootSpeed;
    public Rigidbody rb;
    public GameObject onHit;
    private Transform target;

    public float speed = 30f;

    public void Seek(Transform _target)
    {
        target = _target;
    }

    // Update is called once per frame
    void Update()
    {

        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame)
        {
            Destroy(gameObject);
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        transform.LookAt(target);

    }

    // Use this for initialization
    void Start()
    {
        shootSpeed = 20f;
        rb = GetComponent<Rigidbody>();
    }

    public void shootingSpeed()
    {
        rb.velocity = transform.forward * shootSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            GameObject onHitParticle = Instantiate(onHit, transform.position, Quaternion.identity);
            onHit.transform.SetParent(gameObject.transform, true);
            Destroy(gameObject);
        }
    }

}