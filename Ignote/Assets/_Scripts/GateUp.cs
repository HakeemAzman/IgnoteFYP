using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateUp : MonoBehaviour
{
    [SerializeField] float maxY;
    [SerializeField] float minY;
    [SerializeField] float speedOfGate;

    Vector3 moveDirection = Vector3.up;

    // Update is called once per frame
    void Update()
    {
        GateDir();

        if (this.gameObject.GetComponent<GateUp>().enabled)
        {
            speedOfGate = 5;
        }
    }

    void GateDir()
    {
        //print(transform.position.y);
        if (transform.position.y > maxY)
        {
            speedOfGate = 0;
        }

        if(transform.position.y <= minY)
        {
            moveDirection = Vector3.up;
        }

        transform.Translate(moveDirection * Time.deltaTime * speedOfGate);
    }
}
