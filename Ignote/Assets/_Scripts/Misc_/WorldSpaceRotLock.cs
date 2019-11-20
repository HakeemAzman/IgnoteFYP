using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldSpaceRotLock : MonoBehaviour
{
    public Vector3 offset;
    public Transform parent;

    // Update is called once per frame
    void Update()
    {
        transform.position = parent.position + offset;
    }
}
