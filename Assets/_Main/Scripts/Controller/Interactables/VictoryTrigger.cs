using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryTrigger : MonoBehaviour
{
    private bool canCheckpoint = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController player = collision.GetComponent<PlayerController>();
        if (player != null)
        {
            LevelManager.instance.Victory();
            Time.timeScale = 0;

            if (canCheckpoint)
            {
                canCheckpoint = false;
                //gameManager.ChangeSpawnPosition(transform.position);
            }     
        }
    }
}
