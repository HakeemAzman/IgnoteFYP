using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompanionManager : MonoBehaviour
{
    public bool enableCompanion;
    public bool enablePlayer;
    public Companion_Commands cc;
    public CompanionScript cs;
    public CompanionMovement cm;
    public PlayerMovement pm;
    public GameObject playerChar;
    // Start is called before the first frame update
    void Start()
    {
        enableCompanion = false;
        enablePlayer = false;

    }

    // Update is called once per frame
    void Update()
    {
       if(enablePlayer)
        {
            //StartCoroutine(CompDelay());
            cc.gameObject.GetComponent<Companion_Commands>().enabled = true ;
            pm.gameObject.GetComponent<PlayerMovement>().enabled = true;
            //cs.gameObject.GetComponent<CompanionScript>().enabled = true;
            cm.gameObject.GetComponent<CompanionMovement>().enabled = false;
            
        }

       if(enableCompanion)
        {
            cc.Stay = false;
            cs.gameObject.GetComponent<CompanionScript>().enabled = false;
            cm.gameObject.GetComponent<CompanionMovement>().enabled = true;
            pm.gameObject.GetComponent<PlayerMovement>().enabled = false;
            cc.gameObject.GetComponent<Companion_Commands>().enabled = false;
            StartCoroutine(playerDelay());
        }
    }

    IEnumerator CompDelay()
    {
        yield return new WaitForSeconds(1f);
        cs.gameObject.GetComponent<CompanionScript>().enabled = true;
    }

    IEnumerator playerDelay()
    {
        yield return new WaitForSeconds(1f);
        cs.gameObject.GetComponent<CompanionScript>().enabled = false;
    }
}
