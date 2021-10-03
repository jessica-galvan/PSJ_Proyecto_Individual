using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DetectTargetArea))]
public class BossController : MonoBehaviour
{
    [SerializeField] private string _name;
    [SerializeField] private Transform collectableSpawnPoint;
    [SerializeField] private GameObject collectablePrefab;
    [SerializeField] private GameObject exit;
    private DetectTargetArea detectionArea;
    private bool canAttack;
    private bool isDead;

    public EnemyController Enemy { get; private set; }
    public string BossName => _name;

    private void Awake()
    {
        detectionArea = GetComponent<DetectTargetArea>();
        Enemy = GetComponentInChildren<EnemyController>();
        Enemy.IsBoss = true;
        Enemy.OnDie += OnDie;
        exit.SetActive(false);
    }

    void Start()
    {
        HUDManager.instance.BossFightHud.AssingBoss(this);
        LevelManager.instance.OnPlayerRespawn += ResetStats;
        ActivateBossHUD(false);
    }

    private void Update()
    {
        CheckArea();

        if (canAttack && !isDead)
            ActivateBossHUD(true);
        else
            ActivateBossHUD(false);
    }

    private void CheckArea()
    {
        detectionArea.CheckArea();
        canAttack = detectionArea.DetectTarget();
    }

    public void ActivateBossHUD(bool value)
    {
        HUDManager.instance.BossFightHud.SetHUDActive(value);
    }

    private void OnDie()
    {
        isDead = true;
        Enemy.OnDie -= OnDie;
        LevelManager.instance.OnPlayerRespawn -= ResetStats;
        exit.SetActive(false);
        var collectable = Instantiate(collectablePrefab, collectableSpawnPoint);
        collectable.transform.position = collectableSpawnPoint.position;
    }

    private void ResetStats()
    {
        Enemy.LifeController.Heal(Enemy.LifeController.MaxLife);
    }
}
