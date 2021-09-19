using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject victoryScreen = null;
    [SerializeField] private GameObject gameOverScreen = null;
    [SerializeField] private float restartTimer = 2f;

    public static LevelManager instance;
    public PlayerController Player { get; private set; }

    //PLAYER POSITION
    private Vector2 playerSpawnPosition;
    private Vector2 playerCurrentCheckpoint;

    //GAME CONDITIONS
    private float restartCooldown;
    private int enemyCounter;
    private Animator gameOverAnimator;

    //EVENTS
    public Action OnChangeCurrentEnemies;
    public Action OnChangeCollectable;
    public Action OnPlayerRespawn;
    public Action OnPlayerAssing;

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

        victoryScreen.SetActive(false);
        gameOverScreen.SetActive(false);
        gameOverAnimator = gameOverScreen.GetComponent<Animator>();
    }

    public void AssingCharacter(PlayerController newCharacter)
    {
        this.Player = newCharacter;
        Player.LifeController.OnDie += GameOver;
        playerSpawnPosition = Player.transform.position;
        playerCurrentCheckpoint = playerSpawnPosition;
        OnPlayerAssing?.Invoke();
    }

    private void CheckGameConditions()
    {
        if (enemyCounter == 0 && !gameOverScreen.activeInHierarchy)
        {
            Victory();
        }

        if (GameManager.instance.IsGameFreeze && gameOverScreen.activeInHierarchy && restartCooldown < Time.time)
        {
            RestartLastCheckpoint();
        }
    }

    public void Victory()
    {
        if (!GameManager.instance.IsGameFreeze)
        {
            GameManager.instance.Pause(true);
            victoryScreen.SetActive(true);
        }
    }

    private void GameOver()
    {
        if (!GameManager.instance.IsGameFreeze)
        {
            GameManager.instance.Pause(true);
            gameOverScreen.SetActive(true);
            gameOverAnimator.SetBool("isDead", true);
            restartCooldown = Time.time + restartTimer;
        }
    }

    #region SpawnPosition
    public void ChangeSpawnPosition(Vector2 checkpoint)
    {
        playerCurrentCheckpoint = checkpoint;
    }

    public Vector2 GetCurrentCheckpoint()
    {
        return playerCurrentCheckpoint;
    }

    public void RestartLastCheckpoint()
    {
        GameManager.instance.Pause(false);
        OnPlayerRespawn.Invoke();
        //playerCurrentCheckpoint.y += 1; //para que tenga un offset de cuando vuelve, pero 1 en int es muuy grande la caida
        Player.SetCurrentPosition(playerCurrentCheckpoint);
        Player.LifeController.Respawn();
        gameOverScreen.SetActive(false);
        gameOverAnimator.SetBool("isDead", false);
    }

    #endregion

    #region Enemy Related
    public void AddEnemyToList(EnemyController newEnemy)
    {
        enemyCounter++;
        //newEnemy.lifeController.OnDie += OnEnemyDead;
    }
    private void OnEnemyDead(EnemyController enemy)
    {
        //enemy.lifeController.OnDie -= OnEnemyDead; 
        enemyCounter--;
        CheckGameConditions();
    }

    public bool CheckIfTheyAreEnemies()
    {
        return enemyCounter > 0;
    }
    #endregion  
}
