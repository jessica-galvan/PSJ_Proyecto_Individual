using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    [SerializeField] private UIBarController manaBar;
    [SerializeField] private GameObject score;
    [SerializeField] private Text scoreText;
    [SerializeField] private GameObject victoryScreen;
    [SerializeField] private GameOver gameOverScreen;
    private bool isCollectableVisible;

    public static HUDManager instance;
    public GameOver GameOverScreen => gameOverScreen;
    public GameObject VictoryScreen => victoryScreen;

    public void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        IsScoreVisible(false);
        victoryScreen.SetActive(false);
    }
    private void Start()
    {
        LevelManager.instance.OnPlayerAssing += PlayerAssing;
    }

    public void UpdateMana(int currentMana, int maxMana)
    {
        manaBar.UpdateLifeBar(currentMana, maxMana);
    }

    public void UpdateScore(int newscore)
    {
        scoreText.text = $"x{newscore.ToString()}";
        if (!isCollectableVisible)
            IsScoreVisible(true);
    }

    public void IsScoreVisible(bool value)
    {
        isCollectableVisible = value;
        score.SetActive(value);
    }

    public void IsParticleSystemVisible(bool value)
    {
        manaBar.PlayParticles(value);
    }

    public void PlayerAssing()
    {
        LevelManager.instance.Player.MagicController.UpdateMana += UpdateMana;
        LevelManager.instance.OnPlayerAssing -= PlayerAssing;
    }
}
