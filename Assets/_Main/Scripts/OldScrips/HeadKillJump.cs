using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadKillJump : MonoBehaviour
{
    [SerializeField] private bool canBeKilled = true;
    private LifeController1 life = null;
    
    void Start()
    {
       life = transform.parent.GetComponent<LifeController1>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerController1 player = collision.gameObject.GetComponent<PlayerController1>();
        if (player.CanHeadKill())
        {
            life.Die();
        }
    }
}
