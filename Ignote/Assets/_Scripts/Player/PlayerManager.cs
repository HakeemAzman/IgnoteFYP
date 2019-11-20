using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    #region
    public static PlayerManager instance;

    private void Awake()
    {
        instance = this;
        player = GameObject.FindGameObjectWithTag("Player");
        companion = GameObject.FindGameObjectWithTag("Companion");
    }

    private void Update()
    {
        if(player == null | companion == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            companion = GameObject.FindGameObjectWithTag("Companion");
        }
    }

    #endregion

    public GameObject player;
    public GameObject companion;
    
}
