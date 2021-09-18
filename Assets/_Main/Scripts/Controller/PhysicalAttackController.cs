using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalAttackController : MonoBehaviour
{
    [Header("Attack Phisical Settings")]
    [SerializeField] private Transform attackPoint;
    [SerializeField] private LayerMask enemyDetectionList;
    [SerializeField] private float attackRadius = 1f;
    [SerializeField] private int damage = 5;
    [SerializeField] private float slashCooldown = 1f;
    private float slashCooldownTimer = 0f;
    private bool isAttacking;



    void Start()
    {
        
    }

    void Update()
    {
        
    }
    public void Attack()
    {
        isAttacking = true;
        //animatorController.SetTrigger("IsPhisicalAttacking");
        //attackSound.Play();
        Collider2D collider = Physics2D.OverlapCircle((Vector2)attackPoint.position, attackRadius, enemyDetectionList);
        if (collider != null)
        {
            LifeController1 life = collider.gameObject.GetComponent<LifeController1>();
            if (life != null)
            {
                Debug.Log("Daño al enemigo");
                life.TakeDamage(damage);
                //RechargeMana(1);
            }
        }
        isAttacking = false;
        slashCooldownTimer = slashCooldown + Time.time;  //Comienza el attack cooldown
    }

}
