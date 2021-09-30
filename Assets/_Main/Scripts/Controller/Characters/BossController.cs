using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyController))]
[RequireComponent(typeof(DetectTargetArea))]
public class BossController : MonoBehaviour
{
    [SerializeField] private string _name;
    private DetectTargetArea detectArea;

    public EnemyController Enemy { get; private set; }
    public string Name => _name;

    private void Awake()
    {
        Enemy = GetComponent<EnemyController>();
        detectArea = GetComponent<DetectTargetArea>();

    }

    void Start()
    {
        HUDManager.instance.BossFightHud.AssingBoss(this);
        //TODO: Activate boss fight mode, UI, no escape. reset when players die.
    }

    public void ActivateBossHUD(bool value)
    {

    }

    public void Reset()
    {
        
    }
}
