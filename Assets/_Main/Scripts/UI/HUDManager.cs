using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDManager : MonoBehaviour
{
    [SerializeField] private LifeBarController lifeBar;
    private PlayerController player;

    void Start()
    {
        //player = GameManager.instance.Player;
        //player.LifeController.OnTakeDamage += UpdateLife;
        //player.LifeController.OnHeal += UpdateLife;
    }

    public void UpdateLife(int currentLife)
    {
        lifeBar.UpdateLifeBar(currentLife, player.LifeController.MaxLife);
    }
}
