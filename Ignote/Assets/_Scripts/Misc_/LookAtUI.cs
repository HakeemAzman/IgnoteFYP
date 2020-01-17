using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LookAtUI : MonoBehaviour
{
    Transform camera;
    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(camera);
    }
}
