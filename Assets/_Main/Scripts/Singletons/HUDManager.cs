using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    [SerializeField] private UIBarController manaBar;
    [SerializeField] private GameObject score;
    [SerializeField] private Text scoreText;

    public static HUDManager instance;

    private bool isCollectableVisible;

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
}
