using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    //PLAYER POSITION
    private Vector2 playerSpawnPosition;
    private Vector2 playerCurrentCheckpoint;

    //GAME CONDITIONS
    private GameObject victoryScreen = null;
    private GameOver gameOverEffect;

    //Enemies
    private int enemyCounter;

    //Public & Propierties
    public static LevelManager instance;
    public PlayerController Player { get; private set; }

    //EVENTS
    public Action OnChangeCurrentEnemies;
    public Action OnChangeCollectable;
    public Action OnPlayerRespawn;
    public Action OnPlayerAssing;

    #region Unity
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
    #endregion

    #region Private

    private void CheckGameConditions()
    {
        if (enemyCounter == 0 && !gameOverEffect.IsGameOverActive)
        {
            Victory();
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

    private void OnEnemyDead(EnemyController enemy)
    {
        enemyCounter--;
        CheckGameConditions();
    }
    #endregion

    #region Public
    public void AddEnemyToList(EnemyController newEnemy)
    {
        enemyCounter++;
    }

    public void AssingCharacter(PlayerController newCharacter)
    {
        this.Player = newCharacter;
        Player.OnDie += GameOver;
        playerSpawnPosition = Player.transform.position;
        playerCurrentCheckpoint = playerSpawnPosition;
        OnPlayerAssing?.Invoke();
    }
    public void Victory()
    {
        if (!GameManager.instance.IsGameFreeze)
        {
            GameManager.instance.Pause(true);
            victoryScreen.SetActive(true);
        }
    }
    #endregion

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
        Player.SetCurrentPosition(playerCurrentCheckpoint);
        Player.LifeController.Respawn();
    }
    #endregion
}
