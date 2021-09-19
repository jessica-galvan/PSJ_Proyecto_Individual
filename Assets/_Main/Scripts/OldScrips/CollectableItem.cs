using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableItem : MonoBehaviour
{
    [SerializeField] private GameObject plumaLuz;
    [SerializeField] private float destroyTime = 1f;
    private AudioSource sound;
    private bool canPickup;
    private bool canDestroy;
    private float destroyTimer;

    void Start()
    {
        sound = GetComponent<AudioSource>();
        canPickup = true;
    }

    private void Update()
    {
        if(canDestroy && Time.time > destroyTimer)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController player = collision.GetComponent<PlayerController>();
        if (player != null && canPickup)
        {
            canPickup = false;
            player.PickUpCollectable(1);
            plumaLuz.SetActive(false);
            sound.Play();
            GetComponent<Collider2D>().enabled = false;
            GetComponent<SpriteRenderer>().enabled = false;
            canDestroy = true;
            destroyTimer = destroyTime + Time.time;
        }
    }
}
