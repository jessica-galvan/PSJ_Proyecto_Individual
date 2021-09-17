using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [Header("AllMenus Settings")]
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject helpMenu;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private AudioSource musicLevel = null;
    [SerializeField] private float lowerVolume = 1f;

    [Header("PauseMenu Settings")]
    [SerializeField] private Button buttonResume;
    [SerializeField] private Button buttonMainMenu;
    [SerializeField] private Button buttonHelp;
    [SerializeField] private Button buttonQuit;

    [Header("Help Settings")]
    [SerializeField] private Button goBackButton;

    [Header("Victory & GameOver")]
    [SerializeField] private Button returnMenuButton;

    //Extras
    private bool isActive;
    private bool mainMenuActive;

    void Start()
    {
        GoBack();
        ExitMenu();
        ButtonsListeners();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isActive)
            {
                Pause();
            }
            else
            {
                if (!mainMenuActive)
                {
                    GoBack();
                } else
                {
                    ExitMenu();
                }  
            } 
        }
    }

    private void ButtonsListeners()
    {
        buttonResume.onClick.AddListener(OnClickResumeHandler);
        buttonHelp.onClick.AddListener(OnClickHelpHandler);
        buttonQuit.onClick.AddListener(OnClickQuitHandler);
        goBackButton.onClick.AddListener(OnClickGoBackHandler);
        buttonMainMenu.onClick.AddListener(OnClickMenuHandler);
        returnMenuButton.onClick.AddListener(OnClickMenuHandler);
    }

    private void Pause()
    {
        Time.timeScale = 0;
        gameManager.IsGameFreeze = true;
        isActive = true;
        mainMenuActive = true;
        pauseMenu.SetActive(true);
        musicLevel.volume -= lowerVolume;
    }

    private void GoBack()
    {
        mainMenuActive = true;
        helpMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    private void ExitMenu()
    {
        Time.timeScale = 1;
        gameManager.IsGameFreeze = false;
        isActive = false;
        mainMenuActive = false;
        helpMenu.SetActive(false);
        mainMenu.SetActive(true);
        pauseMenu.SetActive(false);
        musicLevel.volume += lowerVolume;
    }

    private void OnClickHelpHandler()
    {
        mainMenu.SetActive(false);
        helpMenu.SetActive(true);
        mainMenuActive = false;
    }

    private void OnClickResumeHandler()
    {
        ExitMenu();
    }

    private void OnClickMenuHandler()
    {
        SceneManager.LoadScene("MainMenu");
    }

    private void OnClickGoBackHandler()
    {
        GoBack();
    }

    private void OnClickQuitHandler()
    {
        Application.Quit();
        Debug.Log("Se cierra el juego");
    }
}
