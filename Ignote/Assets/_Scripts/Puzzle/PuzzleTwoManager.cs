using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PuzzleTwoManager : MonoBehaviour
{
    [SerializeField] GameObject camPuzzle2;

    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "CamTrigP2Enter")
        {
            camPuzzle2.GetComponent<Animator>().Play("ZoomOutPuz2");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.name == "CamTrigP2Enter")
        {
            camPuzzle2.GetComponent<Animator>().Play("ZoomInPuz2");
        }
    }
}
