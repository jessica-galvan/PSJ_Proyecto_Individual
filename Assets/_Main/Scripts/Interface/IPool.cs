using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPool<T>
{
    void Store(T item);
    T GetInstance();
    int IsAvailable();
}
