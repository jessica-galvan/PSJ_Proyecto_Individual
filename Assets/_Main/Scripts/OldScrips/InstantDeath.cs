using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantDeath : MonoBehaviour
{
    [SerializeField] int damage = 1;
    private GameManager1 gameManager;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager1>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController1 player = collision.GetComponent<PlayerController1>();
        LifeController1 life = collision.GetComponent<LifeController1>();
        if(life != null)
        {
            life.TakeDamage(damage);

            if (player) 
            {
                if (life.GetCurrentLife() > 0)
                {
                    player.PlayerActive(false);
                    player.SetCurrentPosition(gameManager.GetCurrentCheckpoint());
                    player.PlayerActive(true);
                    life.Respawn(life.GetCurrentLife());
                }
            } else 
            {
                Destroy(gameObject);
            }
        }
    }
}
