using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplicationQuitCommand : ICommand
{
    // Start is called before the first frame update
    public ApplicationQuitCommand()
    {
        
    }

    // Update is called once per frame
    public void Do()
    {
        Application.Quit();
    }
}
