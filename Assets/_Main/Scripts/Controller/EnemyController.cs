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

    public bool FacingRight { get; protected set; }
    public bool CanAttack { get; protected set; }

    protected override void Start()
    {
        base.Start();
        lifeBar = GetComponent<UIBarController>();
        LifeController.UpdateLifeBar += UpdateLifeBar;
        LevelManager.instance.AddEnemyToList(this);
        LevelManager.instance.OnPlayerRespawn += OnPlayerRespawnListener;
        TargetDetected(false);
    }
    
    protected void UpdateLifeBar(int currentLife, int maxLife)
    {
        lifeBar.UpdateLifeBar(currentLife, maxLife);
    }

    public void BackFlip()
    {
        transform.Rotate(0f, 180f, 0f);
        canvas.transform.Rotate(0f, 180f, 0f);
        FacingRight = !FacingRight;
    }

    protected override void DieAnimation()
    {
        base.DieAnimation();
        gameObject.GetComponent<Collider2D>().enabled = false;
    }

    protected override void DeathAnimationOver()
    {
        base.DeathAnimationOver();
        RewardDrop();
        Destroy(gameObject);
    }

    protected void RewardDrop()
    {
        var item = InteractablesManager.instance.GetItem((InteractableType)Random.Range(0, 2));
        item.transform.position = transform.position;
    }

    public void TargetDetected(bool value, PlayerController player = null)
    {
        CanAttack = value;
        this.player = player;
    }

    protected virtual void OnPlayerRespawnListener()
    {

    }

    /*private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerController player = collision.gameObject.GetComponent<PlayerController>();
        LifeController life = collision.gameObject.GetComponent<LifeController>();
        if (life != null && !player.CanHeadKill())
        {
            life.TakeDamage(bodyDamage);
        }
    }*/

}
