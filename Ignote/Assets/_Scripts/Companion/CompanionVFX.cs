using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompanionVFX : MonoBehaviour
{
    public float DestroyTimer;

    void OnEnable()
    {
        Invoke("Destroy", DestroyTimer);
    }

    void OnDisable()
    {
        CancelInvoke();
    }

    void Destroy()
    {
        gameObject.SetActive(false);
    }
}
