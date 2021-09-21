using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPooleable
{
    PooleableType Type { get; }

    GameObject gameObject {get; }

    Transform transform { get; }
}
