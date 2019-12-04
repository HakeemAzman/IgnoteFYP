using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Transform lastCheckpoint;

    private AudioSource musicSource;
    private AudioSource SFXSource;

    public int enemy;

    [SerializeField] private GameObject audioSource;
    //[SerializeField] private GameObject SFXsource;
    [SerializeField] private Slider sliderMusic;
    [SerializeField] private Slider sliderSFX;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject optionsPanel;

    public GameObject resumeBtn;

    public GameObject animGate2;
    public GameObject animGate3;
    public GameObject animGate4;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        musicSource = audioSource.GetComponent<AudioSource>();
        //SFXSource = SFXsource.GetComponent<AudioSource>();
        sliderMusic.value = PlayerPrefs.GetFloat("MusicVolume", musicSource.volume);
        //sliderSFX.value = PlayerPrefs.GetFloat("SFXVolume", SFXSource.volume);

        pausePanel.SetActive(false);
        optionsPanel.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetButtonDown("Pause") && !pausePanel.activeInHierarchy)
        {
            PauseGame();
            EventSystem.current.GetComponent<EventSystem>().SetSelectedGameObject(resumeBtn);
        }

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        enemy = enemies.Length;

        if (enemy == 4)
            animGate2.GetComponent<Animator>().Play("GateOpenPuzzle2");

        if (enemy == 7)
            animGate3.GetComponent<Animator>().Play("Gate3Up");

        if (enemy == 5)
            animGate4.GetComponent<Animator>().Play("Gate4Open");
    }

    public void Restart()
    {
        Scene thisScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(thisScene.name);
        Time.timeScale = 1;
    }

    private void PauseGame()
    {
        Cursor.visible = true;
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
        PlayerPrefs.SetFloat("MusicVolume", audioSource.GetComponent<AudioSource>().volume);
    }

    public void GameSFXVolume()
    {
       // PlayerPrefs.SetFloat("SFXVolume", SFXsource.GetComponent<AudioSource>().volume);
    }
}
