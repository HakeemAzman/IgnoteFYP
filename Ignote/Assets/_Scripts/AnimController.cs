using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimController : MonoBehaviour
{
    public Animator anim;
    public Animator cameraAnim;
    public GameObject text;
    public GameObject startRainfall, buttons;

    // Start is called before the first frame update
    void Start()
    {
        anim.gameObject.GetComponent<Animator>();
        cameraAnim.gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        cameraPan();
    }

    public void cameraPan()
    {
        if(Input.GetButtonDown("Stay"))
        {
            StartCoroutine(MainMenu());
        }
    }

    IEnumerator MainMenu()
    {
        startRainfall.SetActive(true);
        anim.SetBool("canStart", true);
        cameraAnim.SetBool("canPan", true);
        text.SetActive(false);

        yield return new WaitForSeconds(2.5f);

        buttons.SetActive(true);
    }
}
