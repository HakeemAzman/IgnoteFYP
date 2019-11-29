using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public float playerCurrentHealth = 100f;
    public float player_Health = 100f;
    public Slider playerHealthImage;

    // Update is called once per frame
    void Update()
    {
        playerHealthImage.value = (float)playerCurrentHealth;
        Death();
    }

    void Death()
    {
        if(playerCurrentHealth == 0)
        {
           // transform.position = GameManager.Instance.lastCheckpoint.position;
            playerCurrentHealth = player_Health;
            SceneManager.LoadScene(0);
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
