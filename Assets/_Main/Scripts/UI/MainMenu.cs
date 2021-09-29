using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [Header("AllMenus Settings")]
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject helpMenu;
    [SerializeField] private GameObject creditsMenu;

    [Header("MainMenu Settings")]
    [SerializeField] private Button buttonPlay;
    [SerializeField] private Button buttonHelp;
    [SerializeField] private Button buttonCredits;
    [SerializeField] private Button buttonQuit;
    [SerializeField] private string gameSceneName = "Level";

    [Header("Help Settings")]
    [SerializeField] private Button buttonHelpGoBack;

    [Header("Credits Settings")]
    [SerializeField] private Button buttonCreditsGoBack;

    private bool mainMenuCheck;
    private ApplicationQuitCommand quitCommand;
    private LoadSceneCommand loadCommand;

    void Awake()
    {
        buttonPlay.onClick.AddListener(OnClickPlayHandler);
        buttonHelp.onClick.AddListener(OnClickHelpHandler);
        buttonCredits.onClick.AddListener(OnClickCreditsHandler);
        buttonQuit.onClick.AddListener(OnClickQuitHandler);
        buttonHelpGoBack.onClick.AddListener(OnClickGoBackHandler);
        buttonCreditsGoBack.onClick.AddListener(OnClickGoBackHandler);
        OnClickGoBackHandler(); //Siempre inicia con el menu principal, es un seteo por si algo queda mal acomodado en el Scene
    }

    private void Start()
    {
        GameManager.instance.SetCursorActive(true);
        InputController.instance.OnPause += OnEscape;
    }

    private void OnEscape()
    {
        if (!mainMenuCheck)
            OnClickGoBackHandler();
    }

    private void OnClickPlayHandler()
    {
        //SceneManager.LoadScene(gameSceneName);
        loadCommand = new LoadSceneCommand(gameSceneName);
        GameManager.instance.AddEvent(loadCommand);
    }

    private void OnClickHelpHandler()
    {
        mainMenu.SetActive(false);
        helpMenu.SetActive(true);
        creditsMenu.SetActive(false);
        mainMenuCheck = false;
    }

    private void OnClickCreditsHandler()
    {
        mainMenu.SetActive(false);
        helpMenu.SetActive(false);
        creditsMenu.SetActive(true);
        mainMenuCheck = false;
    }

    private void OnClickGoBackHandler()
    {
        mainMenu.SetActive(true);
        helpMenu.SetActive(false);
        creditsMenu.SetActive(false);
        mainMenuCheck = true;
    }

    private void OnClickQuitHandler()
    {
        //Application.Quit();
        quitCommand = new ApplicationQuitCommand();
        GameManager.instance.AddEvent(quitCommand);
        Debug.Log("Cerramos el juego");
    }
}
