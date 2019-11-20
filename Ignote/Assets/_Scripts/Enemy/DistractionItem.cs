using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistractionItem : MonoBehaviour
{
    public bool isdropped;
    
    void Start()
    {
        isdropped = false;
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.contactCount >= 1)
        {
            StartCoroutine(WaitAfterDropping());
        }
    }

    IEnumerator WaitAfterDropping()
    {
        isdropped = true;
        yield return new WaitForSeconds(2f);
        isdropped = false;
    }
}
