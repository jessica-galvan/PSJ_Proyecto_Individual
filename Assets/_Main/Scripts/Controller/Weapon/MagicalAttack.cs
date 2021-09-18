using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicalAttack : MonoBehaviour
{
    [SerializeField] private AttackStats _gunStats;
    private bool canMove;
    private float timer;
    public bool CanReturn { get; set; }
    public void Initialize(Transform firePoint, bool boolean)
    {
        transform.position = firePoint.position;
        transform.rotation = firePoint.rotation;
        canMove = boolean;
        timer = _gunStats.LifeMagicalAttack;
    }

    void Update()
    {
        if (canMove)
        {
            transform.position += transform.right * _gunStats.Speed * Time.deltaTime;
        }
            
        timer -= Time.deltaTime;

        if (timer <= 0)
            OnDestroy();
    }

    public void OnDestroy()
    {
        CanReturn = true;
    }
    //TODO: Agregar collider y que cuando hace un collision tambien haga un OnDestroy.
}



   
