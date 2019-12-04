using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    public Animator planeAnim;

    public Image fadeImage;
    public GameObject buttons;
    public GameObject creditPage;
    public GameObject back;
    public GameObject startGameButton;

    bool hasColor = false;

    private void Start()
    {
        Time.timeScale = 1;
    }

    private void Update()
    {
        if (hasColor)
            fadeImage.color = Color.Lerp(fadeImage.color, Color.black, 3 * Time.deltaTime);
    }

    public void BeginGame()
    {
        StartCoroutine(StartGame());
        EventSystem.current.GetComponent<EventSystem>().SetSelectedGameObject(startGameButton);
    }

    public void Credits()
    {
        buttons.SetActive(false);
        creditPage.SetActive(true);
        EventSystem.current.GetComponent<EventSystem>().SetSelectedGameObject(back);
    }

    public void Back()
    {
        creditPage.SetActive(false);
        buttons.SetActive(true);
        EventSystem.current.GetComponent<EventSystem>().SetSelectedGameObject(startGameButton);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    IEnumerator StartGame()
    {
        hasColor = true;
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("Level 1");
    }
}
