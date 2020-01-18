using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Dialog : MonoBehaviour
{
    public Text textDisplay;
    [TextArea (7,10)]
    public string[]sentences;
    private int index;
    public float typingSpeed;
    public GameObject dialogHolder;
    public float timer;
    void Start()
    {
        StartCoroutine(Type());
    }

    void Update()
    {
        if(timer <= 3)
        {
            timer -= Time.deltaTime * 1;
        }
        if(timer <= 0)
        {
            StartCoroutine(NextSentence());
        }

        if(index +1 == sentences.Length)
        {
            Destroy(dialogHolder);
        }
    }
    


    IEnumerator Type()
    {
        foreach(char letter in sentences[index].ToCharArray())
            {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }
    
    IEnumerator NextSentence()
    {
        timer = 3;
        yield return new WaitForSeconds(1);
        NextDialog();
    }

    public void NextDialog()
    {
        if(index < sentences.Length -1)
        {
            index++;
            textDisplay.text = "";
            StartCoroutine(Type());
        }
        else
        {
            textDisplay.text = "";
        }
    }
}
