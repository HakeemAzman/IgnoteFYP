using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public GameObject DialogHolder;
    public GameObject DialogHolder2;
    public CompanionHealth Ch;
    public bool CompanionDialog;
    // Start is called before the first frame update
    void Start()
    {
        Ch.GetComponent<CompanionHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Ch.companionCurrentHealth > 1)
        {
            CompanionDialog = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            DialogHolder.gameObject.SetActive(true);
        }

        if(other.gameObject.tag == "Companion")
        {
            DialogHolder2.gameObject.SetActive(true);
        }
    }
}
