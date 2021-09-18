using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IOwner
{
    Transform ShootingPoint { get; }
    public void ChangeAttack(BaseMagicalAttack gun);
}
