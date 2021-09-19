using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPooleable
{
    public bool CanReturn { get; set; }
}
