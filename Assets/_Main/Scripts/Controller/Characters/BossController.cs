using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DetectTargetArea))]
public class BossController : MonoBehaviour
{
    [SerializeField] private string _name;
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
    }

    void Start()
    {
        HUDManager.instance.BossFightHud.AssingBoss(this);
        ActivateBossHUD(false);
        //TODO: Activate boss fight mode, UI, no escape. reset when players die.
    }

    private void Update()
    {
        CheckArea();

        if (canAttack && !isDead)
        {
            print("visible");
            ActivateBossHUD(true);
        }
        else
        {
            print("no visible");
            ActivateBossHUD(false);
        }
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
    }
}
