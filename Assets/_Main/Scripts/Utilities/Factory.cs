using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class Factory<T> where T : MonoBehaviour
{
    public T Create(T prefab)
    {
        return GameObject.Instantiate(prefab);
    }
}
