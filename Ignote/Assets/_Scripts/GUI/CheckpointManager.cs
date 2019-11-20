using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckpointManager : MonoBehaviour
{
    private static int m_checkpointNumber;

    private Transform m_targetPlayer;
    float m_playerHealth;
    
    void Start()
    {
        m_playerHealth = 0;

        m_targetPlayer = PlayerManager.instance.player.transform;
        m_playerHealth = m_targetPlayer.GetComponent<PlayerHealth>().player_Health;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform == m_targetPlayer)
        {
            print("Player col");
            GameManager.Instance.lastCheckpoint = transform;
        }
    }
}
