using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScore : MonoBehaviour
{
    [SerializeField] private Text scoreText;

    void Start()
    {
        ScoreManager.instance.OnScore += UpdateScore;
    }

    public void UpdateScore(int newScore)
    {
        scoreText.text = newScore.ToString();
    }

}
