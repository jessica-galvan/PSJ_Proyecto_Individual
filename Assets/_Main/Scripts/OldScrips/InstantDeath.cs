using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantDeath : MonoBehaviour
{
    [SerializeField] int damage = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController player = collision.GetComponent<PlayerController>();
        if (player != null)
        {
            player.LifeController.TakeDamage(damage);
            player.LifeController.Respawn();
            //player.SetCurrentPosition(gameManager.GetCurrentCheckpoint());
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
