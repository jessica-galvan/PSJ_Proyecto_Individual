using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private Sprite[] flowerImages = new Sprite[2];
    [SerializeField] private AudioSource checkSound;
    private SpriteRenderer currentSprite = null;
    private bool canCheckpoint = true;
    
    void Start()
    {
        currentSprite = GetComponent<SpriteRenderer>();
        currentSprite.sprite = flowerImages[0];
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController player = collision.GetComponent<PlayerController>();
        if (player != null && canCheckpoint)
        {
            checkSound.Play();
            canCheckpoint = false;
            LevelManager.instance.ChangeSpawnPosition(transform.position);
            currentSprite.sprite = flowerImages[1];
        }
    }
}
