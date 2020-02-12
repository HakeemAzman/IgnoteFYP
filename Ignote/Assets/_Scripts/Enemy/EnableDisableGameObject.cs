using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableDisableGameObject : MonoBehaviour
{
    public GameObject projectileSystem;

    public float DestroyTimer;

    //public GameObject projectileSystem;

    //void Start()
    //{
    //    projectileSystem.SetActive(true);
    //}

    //private void Update()
    //{
    //    Destroy(gameObject, 5f);
    //}

    void OnEnable()
    {
        Invoke("Destroy", 3f);
    }

    void OnDisable()
    {
        CancelInvoke();
    }

    void Destroy()
    {
        projectileSystem.SetActive(true);
        gameObject.SetActive(false);
    }
}
