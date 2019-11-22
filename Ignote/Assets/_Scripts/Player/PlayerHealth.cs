using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float playerCurrentHealth = 100f;
    public float player_Health = 100f;
    public Image playerHealthImage;

    // Update is called once per frame
    void Update()
    {
        playerHealthImage.fillAmount = (float)playerCurrentHealth / (float)player_Health;
        Death();
    }

    void Death()
    {
        if(playerCurrentHealth == 0)
        {
            transform.position = GameManager.Instance.lastCheckpoint.position;
            playerCurrentHealth = player_Health;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "EProjectile")
        {
            player_Health -= 30;
        }
    }
}
