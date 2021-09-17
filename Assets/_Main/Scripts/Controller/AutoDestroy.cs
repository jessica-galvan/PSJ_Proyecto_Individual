using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    [SerializeField] private float lifeTime;
    private float lifeTimeTimer;

    void Start()
    {
        lifeTimeTimer = lifeTime;
    }


    void Update()
    {
        lifeTimeTimer -= Time.deltaTime;
        if (lifeTimeTimer <= 0)
        {
            Destroy(gameObject);
        }
    }
}
