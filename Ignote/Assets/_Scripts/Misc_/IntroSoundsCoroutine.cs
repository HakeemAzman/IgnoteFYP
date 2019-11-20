using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroSoundsCoroutine : MonoBehaviour
{
    [SerializeField] AudioClip windSound, introPlaneFlyingSound, hittingSound, parachuteOpenSound, blastSound, thudSound;
    AudioSource audioS;

    // Start is called before the first frame update
    void Start()
    {
        audioS = GetComponent<AudioSource>();
        StartCoroutine(IntroSound());
    }

    IEnumerator IntroSound()
    {
        audioS.PlayOneShot(windSound, 2f);
        audioS.PlayOneShot(introPlaneFlyingSound);
        yield return new WaitForSeconds(6.2f);
        audioS.PlayOneShot(hittingSound, 7f);
        yield return new WaitForSeconds(0.7f);
        audioS.PlayOneShot(parachuteOpenSound, 9f);
        yield return new WaitForSeconds(4.7f);
        audioS.PlayOneShot(blastSound, 8f);
        yield return new WaitForSeconds(11.2f);
        audioS.PlayOneShot(thudSound, 5f);
    }
}
