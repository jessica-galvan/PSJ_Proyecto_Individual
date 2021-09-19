using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public static InputController instance;

    #region KeyCodes
    private string horizontalAxis = "Horizontal";
    private KeyCode jump = KeyCode.Space;
    private KeyCode shoot = KeyCode.Mouse0;
    private KeyCode physicalAttack = KeyCode.Mouse1;
    private KeyCode dash = KeyCode.LeftShift;
    private KeyCode pause = KeyCode.Escape;
    private KeyCode sprint = KeyCode.LeftShift;
    #endregion

    #region Events
    public Action OnPause;
    public Action OnShoot;
    public Action OnPhysicalAttack;
    public Action OnDash;
    public Action OnJump;
    public Action OnSprint;
    public Action<float> OnMove;
    #endregion

    #region Unity
    private void Awake()
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
        CheckPause();

        if (!GameManager.instance.IsGameFreeze)
        {
            CheckMovement();
            CheckJump();
            CheckShoot();
            CheckSprint();
            CheckPhysicalAttack();
            //CheckDash();

            if (Input.GetKeyDown(KeyCode.K))
                LevelManager.instance.Player.LifeController.TakeDamage(4);
        }
    }
    #endregion

    #region Private
    private void CheckMovement()
    {
        float horizontal = Input.GetAxisRaw(horizontalAxis);
        OnMove?.Invoke(horizontal);
    }
    private void CheckShoot()
    {
        if (Input.GetKeyDown(shoot))
            OnShoot?.Invoke();
    }

    private void CheckPhysicalAttack()
    {
        if (Input.GetKeyDown(physicalAttack))
            OnPhysicalAttack?.Invoke();
    }

    private void CheckJump()
    {
        if (Input.GetKeyDown(jump))
            OnJump?.Invoke();
    }
    private void CheckDash()
    {
        if (Input.GetKeyDown(dash))
            OnDash?.Invoke();
    }

    private void CheckPause()
    {
        if (Input.GetKeyDown(pause))
            OnPause?.Invoke();
    }

    private void CheckSprint()
    {
        if (Input.GetKeyDown(sprint))
            OnSprint?.Invoke();
    }
    #endregion
}
