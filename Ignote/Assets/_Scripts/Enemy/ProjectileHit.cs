using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileHit : MonoBehaviour
{
    public GameObject explosionParticle;

    [SerializeField]
    private float damageOutput;
    private Rigidbody rb;

    #region Player
    Transform targetPlayer;
    #endregion

    #region Companion
    Transform targetCompanion;
    #endregion
    
    PlayerHealth playerHealth;
    CompanionHealth companionHealth;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        targetPlayer = PlayerManager.instance.player.transform;
        targetCompanion = PlayerManager.instance.companion.transform;
        
        companionHealth = targetCompanion.GetComponent<CompanionHealth>();
        playerHealth = targetPlayer.GetComponent<PlayerHealth>();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Terrain") //if the projectile hit anything else that's not a player or companion first, it is no longer a threat
        {
            gameObject.SetActive(false);
        }
        else if (other.transform == targetPlayer) //does damage to the player
        {
            playerHealth.playerCurrentHealth -= damageOutput;
            Instantiate(explosionParticle, transform.position, transform.rotation);
            Destroy(gameObject);
            gameObject.SetActive(false);
        }
        else if (other.transform == targetCompanion) //does damage to the companion
        {
            companionHealth.companionHealth -= damageOutput;
            Instantiate(explosionParticle, transform.position, transform.rotation);
            Destroy(gameObject);
            gameObject.SetActive(false);
        }
    }
}
