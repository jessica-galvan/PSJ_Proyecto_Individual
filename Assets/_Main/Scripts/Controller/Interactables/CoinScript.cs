using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour,IInteractable
{
    [SerializeField] protected InteractableStats _interactableStats;

    void Start()
    {
        GetComponent<InteractableController>().interactable = this;
    }

    public void Interact(PlayerController character)
    {
        character.AddCoins(_interactableStats.Coin);
        Destroy(gameObject);
    }
}
