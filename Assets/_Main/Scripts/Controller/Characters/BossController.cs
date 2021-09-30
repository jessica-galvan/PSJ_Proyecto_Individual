using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DetectTargetArea))]
public class BossController : MonoBehaviour
{
    [SerializeField] private string _name;
    private DetectTargetArea detectionArea;
    private bool canAttack;

    public EnemyController Enemy { get; private set; }
    public string BossName => _name;

    private void Awake()
    {
        Enemy = GetComponentInChildren<EnemyController>();
        detectionArea = GetComponent<DetectTargetArea>();
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
    }

    public void Reset()
    {
        
    }

    private void CheckArea()
    {
        detectionArea.CheckArea();
        canAttack = detectionArea.DetectTarget();
        ActivateBossHUD(canAttack);
    }

    public void ActivateBossHUD(bool value)
    {
        HUDManager.instance.BossFightHud.SetHUDActive(value);
    }

}
