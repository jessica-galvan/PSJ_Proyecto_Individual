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

    [Header("PauseMenu Settings")]
    [SerializeField] private Button buttonResume;
    [SerializeField] private Button buttonMainMenu;
    [SerializeField] private Button buttonHelp;
    [SerializeField] private Button buttonQuit;

    [Header("Help Settings")]
    [SerializeField] private Button goBackButton;

    [Header("Victory")]
    [SerializeField] private Button returnMenuButton;

    //Extras
    private bool isActive;
    private bool mainMenuActive;

    private ApplicationQuitCommand quitCommand;
    private LoadSceneCommand loadCommand;

    void Start()
    {
        InputController.instance.OnPause += CheckIfPause;
        GoBack();
        ExitMenu();
        ButtonsListeners();
    }

    private void CheckIfPause()
    {
        if (!LevelManager.instance.IsEnding)
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
                }
                else
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
        GameManager.instance.Pause(true);
        isActive = true;
        mainMenuActive = true;
        pauseMenu.SetActive(true);
    }

    private void GoBack()
    {
        mainMenuActive = true;
        helpMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    private void ExitMenu()
    {
        GameManager.instance.Pause(false);
        isActive = false;
        mainMenuActive = false;
        helpMenu.SetActive(false);
        mainMenu.SetActive(true);
        pauseMenu.SetActive(false);
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
        loadCommand = new LoadSceneCommand("MainMenu");
        GameManager.instance.AddEvent(loadCommand);
    }

    private void OnClickGoBackHandler()
    {
        GoBack();
    }

    private void OnClickQuitHandler()
    {
        quitCommand = new ApplicationQuitCommand();
        GameManager.instance.AddEvent(quitCommand);
        Debug.Log("Se cierra el juego");
    }

    private void OnDestroy()
    {
        InputController.instance.OnPause -= CheckIfPause;
    }
}
