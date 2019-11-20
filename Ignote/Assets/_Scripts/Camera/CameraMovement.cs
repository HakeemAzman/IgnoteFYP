using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    public GameObject player;

    Vector3 distFromPlayer;

    // Start is called before the first frame update
    void Start()
    {
        distFromPlayer = transform.position - player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position + distFromPlayer;
    }
}
