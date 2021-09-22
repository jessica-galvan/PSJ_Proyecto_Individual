using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneCommand : ICommand
{
    private string _scene;
    public LoadSceneCommand(string scene)
    {
        _scene = scene;
    }

    public void Do()
    {
        SceneManager.LoadScene(_scene);
    }
}
