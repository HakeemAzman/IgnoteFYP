using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Transform lastCheckpoint;

    private AudioSource source;

    [SerializeField] private GameObject audioSource;
    [SerializeField] private Slider sliderMusic;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject optionsPanel;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        source = audioSource.GetComponent<AudioSource>();
        sliderMusic.value = PlayerPrefs.GetFloat("Volume", source.volume);

        pausePanel.SetActive(false);
        optionsPanel.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!pausePanel.activeInHierarchy)
            {
                PauseGame();
            }
            else if(optionsPanel.activeInHierarchy)
            {
                Done();
            }
            else if (pausePanel.activeInHierarchy)
            {
                ContinueGame();
            }
        }
    }

    public void Restart()
    {
        Scene thisScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(thisScene.name);
        Time.timeScale = 1;
    }

    private void PauseGame()
    {
        Time.timeScale = 0;
        pausePanel.SetActive(true);
        //Disable scripts that still work while timescale is set to 0
    }
    public void ContinueGame()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
        //enable the scripts again
    }

    public void Options()
    {
        //options panel comes out
        optionsPanel.SetActive(true);
    }

    public void Done()
    {
        optionsPanel.SetActive(false);
    }

    public void LeaveGame()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void GameMusicVolume()
    {
        PlayerPrefs.SetFloat("Volume", audioSource.GetComponent<AudioSource>().volume);
    }
}
