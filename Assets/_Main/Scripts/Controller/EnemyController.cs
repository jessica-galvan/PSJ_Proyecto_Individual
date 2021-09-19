using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UIBarController))]
public class EnemyController : Actor
{
    protected UIBarController lifeBar;
    [Header("Attack Settings")]
    [SerializeField] private int bodyDamage = 5;
    public bool facingRight = false;
    public bool canAttack = false;

    [Header("Prefabs Settings")]
    [SerializeField] private GameObject canvas = null;

    [Header("Audio Sources")]
    [SerializeField] private AudioSource damageSound = null;

    protected override void Start()
    {
        base.Start();
        lifeBar = GetComponent<UIBarController>();
        _rigidBody = GetComponent<Rigidbody2D>();
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

    protected override void Die()
    {
        base.Die();
        //TODO: Apagar collider? agregar death animation, etc. etc. olvidarse de los prefabs. Configurar el drop post muerte.
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
