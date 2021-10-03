using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UIBarController))]
public class EnemyController : Actor
{
    [Header("Prefabs Settings")]
    [SerializeField] protected GameObject canvas = null;

    protected UIBarController lifeBar;
    protected bool canShoot;
    protected PlayerController player;
    protected float cooldownTimer;

    public bool IsBoss { get; set; }
    public bool FacingRight { get; protected set; }
    public bool CanAttack { get; protected set; }

    public Action OnDeathAnimation;

    protected override void Start()
    {
        base.Start();
        lifeBar = GetComponent<UIBarController>();
        lifeBar.SetBarVisible(false);
        LifeController.UpdateLifeBar += UpdateLifeBar;
        LevelManager.instance.AddEnemyToList(this);
        TargetDetected(false);
    }
    
    protected void UpdateLifeBar(int currentLife, int maxLife)
    {
        lifeBar.UpdateLifeBar(currentLife, maxLife);
        if (!lifeBar.IsVisible && !IsBoss)
            lifeBar.SetBarVisible(true);
    }

    public void BackFlip()
    {
        transform.Rotate(0f, 180f, 0f);
        canvas.transform.Rotate(0f, 180f, 0f);
        FacingRight = !FacingRight;
    }

    protected override void OnDeath()
    {
        base.OnDeath();
        OnDie?.Invoke();
        lifeBar.SetBarVisible(false);
        gameObject.GetComponent<Collider2D>().enabled = false;
    }

    protected override void DeathAnimationOver()
    {
        base.DeathAnimationOver();
        OnDeathAnimation?.Invoke();
        if(!IsBoss)
            RewardDrop();
        Destroy(gameObject);
    }

    protected void RewardDrop()
    {
        var random = UnityEngine.Random.Range(0, 2);

        switch (random)
        {
            case 0:
                var item = PoolManager.instance.GetItem(PooleableType.Heal);
                item.transform.position = transform.position;
                break;
            default:
                item = PoolManager.instance.GetItem(PooleableType.Mana);
                item.transform.position = transform.position;
                break;
        }
    }

    public void TargetDetected(bool value, PlayerController player = null)
    {
        CanAttack = value;
        this.player = player;
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    PlayerController player = collision.gameObject.GetComponent<PlayerController>();
    //    if (player != null && !player.CanHeadKill()&& !player.PhysicalAttackController.IsAttacking)
    //        player.LifeController.TakeDamage(_attackStats.PhysicalDamage);
    //}
}
