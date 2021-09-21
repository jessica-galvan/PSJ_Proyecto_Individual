using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadKillJump : MonoBehaviour
{
    private LifeController life = null;
    
    void Start()
    {
       life = transform.parent.GetComponent<LifeController>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerController player = collision.gameObject.GetComponent<PlayerController>();
        if (player != null && player.CanHeadKill())
        {
            //life.Die();
        }
    }
}
