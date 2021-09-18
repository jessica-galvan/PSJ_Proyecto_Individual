using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionArea : MonoBehaviour
{
    //public Action<bool, Actor> OnTriggerEvent;
    private EnemyController enemyController;

    private void Start()
    {
        enemyController = transform.parent.GetComponent<EnemyController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController player = collision.GetComponent<PlayerController>();
        if (player != null)
            enemyController.canAttack = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        PlayerController player = collision.GetComponent<PlayerController>();
        if (player != null)
            enemyController.canAttack = false;
    }
}
