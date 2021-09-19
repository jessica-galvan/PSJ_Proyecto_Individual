using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floater : MonoBehaviour
{
    [SerializeField] private float amplitud = 0.1f;
    [SerializeField] private float speed = 1f;
    private Vector2 spawnPosition;
    private Vector2 currentPosition;

    void Start()
    {
        spawnPosition = transform.position;
    }

    void Update()
    {
        if (!GameManager.instance.IsGameFreeze)
        {
            currentPosition.x = spawnPosition.x;
            currentPosition.y = spawnPosition.y + amplitud * Mathf.Sin(speed * Time.time);
            transform.position = currentPosition;
        }
    }
}
