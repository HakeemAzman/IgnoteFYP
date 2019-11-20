using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public GameObject distractionItem;
    public Transform spawnTransform;

    private Transform targetPlayer;
    private int count;

    void Start()
    {
        count = 0;
        targetPlayer = PlayerManager.instance.player.transform;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform == targetPlayer && count < 1)
        {
            //object drops.
            count += 1;
            Instantiate(distractionItem, spawnTransform.position, transform.rotation);
        }
    }
}
