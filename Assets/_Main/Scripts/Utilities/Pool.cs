using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool<T> : IPool<T> where T : MonoBehaviour
{
    // Prefab
    private T _prefab;

    // Lists
    private List<T> _inUse = new List<T>();
    private List<T> _available = new List<T>();

    public bool IsEmpty => (_available.Count <= 0);

    public Pool(T prefab)
    {
        _prefab = prefab;
    }

    public T CreateInstance()
    {
        //var instance = GameObject.Instantiate(_prefab);
        var instance = (T) AssetsFactory.instance.Create(_prefab);
        _inUse.Add(instance);
        return instance;
    }

    public T GetInstance()
    {
        if (!IsEmpty)
        {
            T instance = _available[0];
            _available.Remove(instance);
            _inUse.Add(instance);
            instance.gameObject.SetActive(true);
            return instance;
        }
        else
        {
            return CreateInstance();
        }
    }

    public void Store(T instance)
    {
        _available.Add(instance);
        instance.gameObject.SetActive(false);
        if (_inUse.Contains(instance))
            _inUse.Remove(instance);
    }

}