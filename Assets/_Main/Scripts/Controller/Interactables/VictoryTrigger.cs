using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryTrigger : MonoBehaviour
{
    //private bool hasChecked;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController player = collision.GetComponent<PlayerController>();
        if (player != null)
        {
            LevelManager.instance.Victory();

            //if (!hasChecked)
            //{
            //    hasChecked = true;
            //    //gameManager.ChangeSpawnPosition(transform.position);
            //}
        }
    }
}
