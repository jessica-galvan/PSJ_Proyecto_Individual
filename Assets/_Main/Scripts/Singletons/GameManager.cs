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

    public void Pause(bool value)
    {
        IsGameFreeze = value;
        if (value)
        {
            Time.timeScale = 0;
            //TODO: lower music
        }
        else
        {
            Time.timeScale = 1;
            //TODO: subir musica
        }         
    }
}
