using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floater : MonoBehaviour
{
    [SerializeField] private float amplitud = 0.1f;
    [SerializeField] private float speed = 1f;
    private Vector3 spawnPosition;
    private GameManager1 gameManager = null;

    void Start()
    {
        spawnPosition = transform.position;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager1>();
    }

    void Update()
    {
        if (!gameManager.IsGameFreeze)
        {
            Vector2 currentPosition;
            currentPosition.x = spawnPosition.x;
            currentPosition.y = spawnPosition.y + amplitud * Mathf.Sin(speed * Time.time);
            transform.position = currentPosition;
        }
    }
}
