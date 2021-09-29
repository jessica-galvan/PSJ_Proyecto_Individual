using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : BaseInteractable
{
    private float timer;
    private bool canDamage;

    private void Update()
    {
        timer -= Time.deltaTime;
        if (canDamage && timer <= 0)
        {
            player.LifeController.TakeDamage(_interactableStats.Damage);
            timer = _interactableStats.DamageTimer;
        }
    }
    public override void Interact()
    {
        canDamage = true;
    }

    public void OnTriggerExit2D(Collider2D collision) //When they leave...
    {
        if (player == collision.GetComponent<PlayerController>())
        {
            player = null;
            canDamage = false;
            timer = 0;
        }
    }

}
