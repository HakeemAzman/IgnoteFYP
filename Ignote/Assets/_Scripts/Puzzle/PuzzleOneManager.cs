using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleOneManager : MonoBehaviour
{
    [SerializeField] GameObject cubeA, cubeB, gate, robotCage, cageObstacle,GateAnimator;

    [SerializeField] AudioClip gateOpeningSound, heavyGateOpeningSound, pressurePlateSound; 
    AudioSource audioS;

    public Companion_Commands cc;

    public GameObject interactText;
    
    private void Start()
    {
        audioS = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PressurePlate"))
        {
            audioS.PlayOneShot(pressurePlateSound,0.5f);
            audioS.PlayOneShot(heavyGateOpeningSound, 0.3f);
        }

        if ((other.gameObject.name == "ObstacleChain") && Input.GetButtonDown("Stay"))
        {
            StartCoroutine("LiftDown");
        }

        if (other.gameObject.tag == "ScriptEnable") 
        {
            cc.gameObject.GetComponent<Companion_Commands>().enabled = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "ChainRobot")
            interactText.SetActive(true);

        if(other.CompareTag("PressurePlate"))
        {
            gate.GetComponent<Animator>().Play("GateOpen");
        }

        if ((other.gameObject.name == "ChainRobot") && Input.GetButtonDown("Stay"))
        {
            robotCage.GetComponent<Animator>().Play("RobotCageOpen");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "ChainRobot")
            interactText.SetActive(false);
    }
}
