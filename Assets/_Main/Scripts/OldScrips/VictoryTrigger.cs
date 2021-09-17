using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryTrigger : MonoBehaviour
{
    [SerializeField] private GameManager gameManager = null;
    private bool canCheckpoint = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController1 player = collision.GetComponent<PlayerController1>();
        if (player != null)
        {
            gameManager.Victory();
            Time.timeScale = 0;

            if (canCheckpoint)
            {
                canCheckpoint = false;
                //gameManager.ChangeSpawnPosition(transform.position);
            }     
        }
    }
}
