using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateDown : MonoBehaviour
{
    [SerializeField] float maxY;
    [SerializeField] float minY;
    [SerializeField] float speedOfGate;

    Vector3 moveDirection = Vector3.up;

    // Update is called once per frame
    void Update()
    {
        GateDir();

        if(this.gameObject.GetComponent<GateDown>().enabled)
        {
            speedOfGate = 5;
        }
    }

    void GateDir()
    {
        //print(transform.position.y);
        if (transform.position.y > maxY)
        {
            moveDirection = Vector3.down;
        }

        if (transform.position.y <= minY)
        {
            speedOfGate = 0;
        }

        transform.Translate(moveDirection * Time.deltaTime * speedOfGate);
    }
}
