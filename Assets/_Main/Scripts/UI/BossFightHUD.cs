using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(UIBarController))]
public class BossFightHUD : MonoBehaviour
{
    [SerializeField] private GameObject bossUI;
    [SerializeField] private Text text;
    private UIBarController lifeBar;
    private BossController _boss;

    private void Awake()
    {
        lifeBar = GetComponent<UIBarController>();
    }

    public void AssingBoss(BossController boss)
    {
        _boss = boss;
        text.text = _boss.BossName;
        _boss.Enemy.LifeController.UpdateLifeBar += UpdateLife;
        _boss.Enemy.LifeController.OnDie += OnDie;
    }

    public void UpdateLife(int currentLife, int maxLife)
    {
        lifeBar.UpdateLifeBar(currentLife, maxLife);
    }

    public void OnDie()
    {
        _boss.Enemy.LifeController.UpdateLifeBar -= UpdateLife;
        _boss.Enemy.LifeController.OnDie -= OnDie;
        _boss = null;
    }

    public void SetHUDActive(bool value)
    {
        bossUI.SetActive(value);
    }
}
