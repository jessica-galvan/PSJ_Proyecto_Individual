using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionArea : MonoBehaviour
{
    private EnemyController enemyController;

    private void Start()
    {
        enemyController = transform.parent.GetComponent<EnemyController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController player = collision.GetComponent<PlayerController>();
        if (player != null)
            enemyController.TargetDetected(true, player);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        PlayerController player = collision.GetComponent<PlayerController>();
        if (player != null)
            enemyController.TargetDetected(false);
    }
}
