using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    private Animator _animator;
    
    public bool IsGameOverActive { get; private set; }

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void SetGameOver(bool value)
    {
        _animator.SetBool("IsDead", value);
        IsGameOverActive = value;
        gameObject.SetActive(value);
    }

    private void EndAnimation()
    {
        LevelManager.instance.RestartLastCheckpoint();
    }
}
