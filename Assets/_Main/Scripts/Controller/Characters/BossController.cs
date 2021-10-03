using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    [SerializeField] private string _name;
    [SerializeField] private Transform collectableSpawnPoint;
    [SerializeField] private GameObject collectablePrefab;
    [SerializeField] private GameObject exit;
    private bool isDead;
    private bool isInBattle;

    public EnemyController Enemy { get; private set; }
    public string BossName => _name;

    private void Awake()
    {
        Enemy = GetComponentInChildren<EnemyController>();
        Enemy.IsBoss = true;
        Enemy.OnDeathAnimation += OnDie;
        exit.SetActive(false);
    }

    void Start()
    {
        HUDManager.instance.BossFightHud.AssingBoss(this);
        LevelManager.instance.OnPlayerRespawn += ResetStats;
        ActivateBossHUD(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponent<PlayerController>();
        if (player != null && !isDead && !isInBattle && Enemy != null)
            ActivateBossHUD(true);
    }

    public void ActivateBossHUD(bool value)
    {
        isInBattle = value;
        HUDManager.instance.BossFightHud.SetHUDActive(value);
    }

    private void OnDie()
    {
        ActivateBossHUD(false);
        isDead = true;
        Enemy.OnDeathAnimation -= OnDie;
        LevelManager.instance.OnPlayerRespawn -= ResetStats;
        exit.SetActive(true);
        var collectable = Instantiate(collectablePrefab, collectableSpawnPoint);
        collectable.transform.position = collectableSpawnPoint.position;
    }

    private void ResetStats()
    {
        ActivateBossHUD(false);
        Enemy.LifeController.Heal(Enemy.LifeController.MaxLife);
    }
}
