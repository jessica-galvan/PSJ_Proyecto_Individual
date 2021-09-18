using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UIBarController))]
public class EnemyController : Actor
{    
    [Header("Attack Settings")]
    [SerializeField] private int bodyDamage = 5;
    public bool facingRight = false;
    public bool canAttack = false;

    [Header("Prefabs Settings")]
    [SerializeField] private GameObject canvas = null;

    [Header("Audio Sources")]
    [SerializeField] private AudioSource shootingSound = null;
    [SerializeField] private AudioSource damageSound = null;

    protected override void Start()
    {
        base.Start();
        _rigidBody = GetComponent<Rigidbody2D>();
        LevelManager.instance.AddEnemyToList(this);
        LevelManager.instance.OnPlayerRespawn += OnPlayerRespawnListener;
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
