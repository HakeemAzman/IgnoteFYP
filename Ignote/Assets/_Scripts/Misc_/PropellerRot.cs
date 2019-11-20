using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropellerRot : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, 1000 * Time.deltaTime);       
    }
}
