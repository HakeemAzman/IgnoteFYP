using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public float playerCurrentHealth;
    public float player_Health;
    public float regenHealth;
    public Slider playerHealthImage;

    PlayerMovement pmScript;
    bool isRegenHealth;

    private void Start()
    {
        pmScript = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        playerHealthImage.value = (float)playerCurrentHealth;

        if(!pmScript.isMoving && playerCurrentHealth <= player_Health && !isRegenHealth)
        {
            StartCoroutine(RegainHealthOverTime());
        }

        Death();
    }

    void Death()
    {
        if(playerCurrentHealth <= 0)
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
            playerCurrentHealth -= 2f;
        }
    }

    void RegenHealth()
    {
        playerCurrentHealth += regenHealth;
        
        if(playerCurrentHealth >= player_Health)
        {
            playerCurrentHealth = player_Health;
        }
    }

    private IEnumerator RegainHealthOverTime()
    {
        isRegenHealth = true;
        while (playerCurrentHealth < player_Health)
        {
            RegenHealth();
            yield return new WaitForSeconds(3);
        }
        isRegenHealth = false;
    }
}
