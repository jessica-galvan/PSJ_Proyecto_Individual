using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class ScoreManager : MonoBehaviour
{
    //SINGLETON
    public static ScoreManager instance;

    //Score
    private int score;
    public int Score => score;

    public Action<int> OnScore;

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
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            AddScore(1);
    }

    public void AddScore(int newscore)
    {
        score += newscore;
        OnScore?.Invoke(score);
    }
}
