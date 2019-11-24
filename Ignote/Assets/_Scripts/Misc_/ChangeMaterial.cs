using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMaterial : MonoBehaviour
{

    [SerializeField] Material transparentMat;
    [SerializeField] GameObject[] walls;

    Color clr;

    private void Awake()
    {
        transparentMat.color = new Color(185, 165, 128, 255);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            clr = transparentMat.color;

            clr.a -= 1f * Time.deltaTime;

            if (clr.a < 0f)
                clr.a = 0f;

            for(int i = 0; i < walls.Length; i++)
                walls[i].GetComponent<Renderer>().material = transparentMat;
        }
    }

}
