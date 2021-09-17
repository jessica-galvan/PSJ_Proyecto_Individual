using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //SINGLETON
    public static GameManager instance;

    //PROPIEDADES
    public bool IsGameFreeze { get; private set; }
    public PlayerController Player { get; private set; }
    public string CurrentLevel { get; private set; }

    //EVENTS
    public Action OnPause;

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

    public void AssingCharacter(PlayerController newCharacter)
    {
        this.Player = newCharacter;
        Player.LifeController.OnDie += GameOver;
    }
    public void GameOver()
    {
        //character.LifeController.OnDie -= GameOver;
        //TODO: Change Scene, respawn, whatever.
    }

    public void Pause(bool value)
    {
        IsGameFreeze = value;
        if (value)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }
}
