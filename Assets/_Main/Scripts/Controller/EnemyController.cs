using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UIBarController))]
public class EnemyController : Actor
{
    [Header("Prefabs Settings")]
    [SerializeField] protected GameObject canvas = null;
    [SerializeField] protected GameObject[] reward = new GameObject[2];
    [SerializeField] protected Collider2D detectionZone;

    protected UIBarController lifeBar;
    protected bool facingRight;
    protected bool canAttack;
    protected PlayerController player;

    protected override void Start()
    {
        base.Start();
        lifeBar = GetComponent<UIBarController>();
        LifeController.UpdateLifeBar += UpdateLifeBar;
        LevelManager.instance.AddEnemyToList(this);
        LevelManager.instance.OnPlayerRespawn += OnPlayerRespawnListener;
    }
    
    protected void UpdateLifeBar(int currentLife, int maxLife)
    {
        lifeBar.UpdateLifeBar(currentLife, maxLife);
    }

    public void BackFlip()
    {
        transform.Rotate(0f, 180f, 0f);
        canvas.transform.Rotate(0f, 180f, 0f);
        facingRight = !facingRight;
    }

    protected virtual void OnPlayerRespawnListener()
    {

    }

    protected override void DieAnimation()
    {
        base.DieAnimation();
        //TODO: Apagar collider? agregar death animation, etc. etc. olvidarse de los prefabs. Configurar el drop post muerte.
    }

    protected override void DeathAnimationOver()
    {
        base.DeathAnimationOver();
        Instantiate(reward[Random.Range(0, reward.Length)], transform.position, transform.rotation);
        Destroy(gameObject);
    }

    public void TargetDetected(bool value, PlayerController player = null)
    {
        canAttack = value;
        this.player = player;
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
