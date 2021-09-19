using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : InteractableController
{
    [SerializeField] private Sprite[] flowerImages = new Sprite[2];
    private SpriteRenderer currentSprite = null;
    
    void Start()
    {
        currentSprite = GetComponent<SpriteRenderer>();
        currentSprite.sprite = flowerImages[0];
    }

    protected override void Interact()
    {
        if (currentSprite.sprite != flowerImages[1])
        {
            currentSprite.sprite = flowerImages[1];
            AudioManager.instance.PlayPlayerSound(PlayerSoundClips.CheckPoint);
            LevelManager.instance.ChangeSpawnPosition(transform.position);
        }
    }
}
