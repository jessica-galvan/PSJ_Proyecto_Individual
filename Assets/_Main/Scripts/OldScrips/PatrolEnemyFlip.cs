using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolEnemyFlip : MonoBehaviour
{
    private bool isPatrol;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isPatrol)
        {
            EnemyPatrolController enemy = collision.GetComponent<EnemyPatrolController>();
            if (enemy != null)
            {
                enemy.BackFlip();
            }
        } else
        {
            EnemyFly enemy = collision.GetComponent<EnemyFly>();
            if (enemy != null)
            {
                enemy.BackFlip();
            }
        }
        

    }

    public void SetIsPatrol(bool response)
    {
        isPatrol = response;
    }
}
