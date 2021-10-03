using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private List<ICommand> _events = new List<ICommand>();

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

    private void Update()
    {
        if(_events.Count > 0)
        {
            for (int i = _events.Count - 1; i >= 0; i--) //EVENT QUEUE
            {
                _events[i].Do();
                _events.RemoveAt(i);
            }
        }
    }

    public void Pause(bool value)
    {
        IsGameFreeze = value;
        SetCursorActive(value);
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

    public void AddEvent(ICommand command)
    {
        _events.Add(command);
    }

    public void SetCursorActive(bool value)
    {
        if (value)
            Cursor.lockState = CursorLockMode.None;
        else
            Cursor.lockState = CursorLockMode.Locked;
    }
}
