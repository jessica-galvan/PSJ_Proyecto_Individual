using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractableController : MonoBehaviour, IPooleable
{
    [SerializeField] protected InteractableStats _interactableStats;
    protected PlayerController player;

    public bool CanReturn { get; set; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        player = collision.gameObject.GetComponent<PlayerController>();
        if (player != null)
        {
            Interact();
        }
    }

    protected abstract void Interact();

    protected virtual void Destroy()
    {
        Destroy(gameObject);
    }
}
