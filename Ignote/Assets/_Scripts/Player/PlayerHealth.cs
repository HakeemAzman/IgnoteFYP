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
    public GameObject health, endurance, charges;
    public Image respawnFade;
    public GameObject robot;
    public CompanionHealth chScript;

    PlayerMovement pmScript;
    bool isRegenHealth;
    Vector3 offset = new Vector3(3, 0 , 3);
    Transform currentCheckpointLoc;

    private void Start()
    {
        chScript = GetComponent<CompanionHealth>();
        pmScript = GetComponent<PlayerMovement>();
        health.SetActive(false);
        endurance.SetActive(false);
        charges.SetActive(false);
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
            StartCoroutine(Respawn());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "EProjectile")
        {
            playerCurrentHealth -= 2f;
        }

        if(other.gameObject.CompareTag("Checkpoint"))
        {
            currentCheckpointLoc = other.gameObject.transform;
        }

        if(other.gameObject.name == "BlackBorderCollider")
        {
            health.SetActive(true);
            endurance.SetActive(true);
            charges.SetActive(true);
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

    private IEnumerator Respawn()
    {
        respawnFade.color = Color.Lerp(respawnFade.color, Color.black, 1 * Time.deltaTime);
        yield return new WaitForSeconds(2f);
        transform.position = currentCheckpointLoc.position;
        robot.transform.position = currentCheckpointLoc.position + offset;
        playerCurrentHealth = player_Health;
        respawnFade.color = Color.Lerp(respawnFade.color, Color.clear, 1 * Time.deltaTime);
    }
}
