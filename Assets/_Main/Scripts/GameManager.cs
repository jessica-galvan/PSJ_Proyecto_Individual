using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerController1 player = null;
    [SerializeField] private GameObject victoryScreen = null;
    [SerializeField] private GameObject gameOverScreen = null;
    [SerializeField] private float restartTimer = 2f;
    [SerializeField] private int lifesRespawn = 2;
    [SerializeField] private int collectable;

    public static GameManager instance;

    public bool IsGameFreeze { get; set; }
    public PlayerController1 Player { get; private set; }

    //Extras
    private bool gameOver = false;
    private bool victory = false;
    private float restartCooldown = 0f;
    private int enemyCounterLevel;
    private int enemyCounter;
    private Vector2 playerSpawnPosition;
    private Vector2 playerCurrentCheckpoint;
    private Animator gameOverAnimator;
    public UnityEvent OnChangeCurrentEnemies = new UnityEvent();
    public UnityEvent OnChangeCollectable = new UnityEvent();
    public UnityEvent OnPlayerRespawn = new UnityEvent();

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

    void Start()
    {
        player.lifeController.OnDie.AddListener(OnPlayerDieListener);
        playerSpawnPosition = player.GetComponent<Transform>().position;
        playerCurrentCheckpoint = playerSpawnPosition;
        victoryScreen.SetActive(false);
        gameOverScreen.SetActive(false);
        gameOverAnimator = gameOverScreen.GetComponent<Animator>();
        enemyCounter = enemyCounterLevel;
        IsGameFreeze = false;
        victory = false;
    }

    void Update()
    {
        if(enemyCounter == 0 && !gameOver)
        {
            Victory();
        }

        if(IsGameFreeze && gameOver && restartCooldown < Time.time)
        {
            RestartLastCheckpoint();
        }
    }

    //public void AssingCharacter(PlayerController newCharacter)
    //{
    //    this.Player = newCharacter;
    //    Player.LifeController.OnDie += GameOver;
    //}

    public void Pause(bool value)
    {
        IsGameFreeze = value;
        if (value)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }

    public bool CheckIfTheyAreEnemies()
    {
        bool response = false;
        if(enemyCounter > 0)
        {
            response = true;
        }
        return response;
    }

    public void Victory()
    {
        if (!victory)
        {
            victory = true;
            IsGameFreeze = true;
            victoryScreen.SetActive(true);
        }
    }

    public void GameOver()
    {
        //character.LifeController.OnDie -= GameOver;
        //TODO: Change Scene, respawn, whatever.
        gameOver = true;
        IsGameFreeze = true;
        gameOverScreen.SetActive(true);
        gameOverAnimator.SetBool("isDead", true);
        restartCooldown = Time.time + restartTimer;
    }

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
        gameOver = false;
        IsGameFreeze = false;
        OnPlayerRespawn.Invoke();
        //playerCurrentCheckpoint.y += 1; //para que tenga un offset de cuando vuelve, pero 1 en int es muuy grande la caida
        player.SetCurrentPosition(playerCurrentCheckpoint);
        player.PlayerActive(true);
        player.lifeController.Respawn(lifesRespawn);
        gameOverScreen.SetActive(false);
        gameOverAnimator.SetBool("isDead", false);
    }

    private void OnPlayerDieListener()
    {
        GameOver();
    }

    public void addOneInEnemyCounter()
    {
        enemyCounterLevel++;
    }

    public void takeOneEnemy()
    {
        enemyCounter--;
        OnChangeCurrentEnemies.Invoke();
    }

    public int GetEnemiesAmount()
    {
        return enemyCounter;
    }

    public void PickUpCollectable()
    {
        collectable++;
        OnChangeCollectable.Invoke();
    }
}
