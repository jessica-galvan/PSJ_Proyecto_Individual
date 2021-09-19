using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public PlayerController Player { get; private set; }

    //PLAYER POSITION
    private Vector2 playerSpawnPosition;
    private Vector2 playerCurrentCheckpoint;

    //GAME CONDITIONS

    private GameObject victoryScreen = null;
    private GameOver gameOverEffect;
    private int enemyCounter;

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
    }

    public void Start()
    {
        victoryScreen = HUDManager.instance.VictoryScreen;
        gameOverEffect = HUDManager.instance.GameOverScreen;
        AudioManager.instance.EnviromentMusic(EnviromentSoundClip.LevelMusic);
    }

    private void Update()
    {
        //if (GameManager.instance.IsGameFreeze && gameOverScreen.activeInHierarchy && restartCooldown < Time.deltaTime)
        //{
        //    RestartLastCheckpoint();
        //}
    }

    public void AssingCharacter(PlayerController newCharacter)
    {
        this.Player = newCharacter;
        Player.OnDie += GameOver;
        playerSpawnPosition = Player.transform.position;
        playerCurrentCheckpoint = playerSpawnPosition;
        OnPlayerAssing?.Invoke();
    }

    private void CheckGameConditions()
    {
        if (enemyCounter == 0 && !gameOverEffect.IsGameOverActive)
        {
            Victory();
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
            HUDManager.instance.IsParticleSystemVisible(false);
            gameOverEffect.SetGameOver(true);
            //restartCooldown = Time.deltaTime + restartTimer;
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
        gameOverEffect.SetGameOver(false);
        HUDManager.instance.IsParticleSystemVisible(true);
        OnPlayerRespawn?.Invoke();
        //playerCurrentCheckpoint.y += 1; //para que tenga un offset de cuando vuelve, pero 1 en int es muuy grande la caida
        Player.SetCurrentPosition(playerCurrentCheckpoint);
        Player.LifeController.Respawn();
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
