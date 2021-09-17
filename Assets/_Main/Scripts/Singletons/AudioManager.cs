using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SoundClips
{
    MouseClick,
    Win,
    Gameover,
    Shoot,
    Absorb,
    Explosion
}

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("AudioSources")]
    [SerializeField] private AudioSource musicAudioSource;
    [SerializeField] private AudioSource movementAudioSource;
    [SerializeField] private AudioSource sfxAudioSource;
    [SerializeField, Range(0, 1)] private float musicInitialVolumen;

    [Header("Music")]
    [SerializeField] private AudioClip mainMenuMusic;
    [SerializeField] private AudioClip levelMusic;
    [SerializeField] private AudioClip victorySound;
    [SerializeField] private AudioClip gameOverSound;

    [Header("SFX Sounds")]
    [SerializeField] private AudioClip clickSound;
    [SerializeField] private AudioClip checkPointSound;
    [SerializeField] private AudioClip deadSound;
    [SerializeField] private AudioClip rewardSound;
    [SerializeField] private AudioClip powerUpSound;

    [Header("Player Sounds")]
    [SerializeField] private AudioClip shootSound;
    [SerializeField] private AudioClip attackSound;
    [SerializeField] private AudioClip reloadAmmoLeftSound;
    [SerializeField] private AudioClip negativeSound;
    [SerializeField] private AudioClip damageSound;

    [Header("Enemy Patrol Sounds")]
    [SerializeField] private AudioClip attackEnemyPatrolSound;
    [SerializeField] private AudioClip hitEnemyPatrolSound;
    [SerializeField] private AudioClip deathEnemyPatrolSound;

    [Header("Enemy Static Sounds")]
    [SerializeField] private AudioClip attackEnemyStaticSound;
    [SerializeField] private AudioClip hitEnemyStaticSound;
    [SerializeField] private AudioClip deathEnemyStaticSound;

    [Header("Enemy Fly Sounds")]
    [SerializeField] private AudioClip attackEnemyFlySound;
    [SerializeField] private AudioClip hitEnemyFlySound;
    [SerializeField] private AudioClip deathEnemyFlySound;

    [Header("Boss Sounds")]
    [SerializeField] private AudioClip attackBossSound;
    [SerializeField] private AudioClip hitBossSound;
    [SerializeField] private AudioClip deathBossSound;

    public void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        Initialize();
        SubscribeEvents();
    }

    private void Initialize()
    {
        musicAudioSource.volume = musicInitialVolumen;
        musicAudioSource.clip = levelMusic;
        musicAudioSource.Play();
    }

    private void SubscribeEvents()
    {
        //TODO: Subscribirse al input o a los eventos de todos.
    }

    public void PlaySound(SoundClips soundClip)
    {
        switch (soundClip)
        {
            case SoundClips.MouseClick:
                sfxAudioSource.volume = 1f;
                sfxAudioSource.PlayOneShot(deadSound);
                break;
            case SoundClips.Win:
                sfxAudioSource.volume = 1f;
                sfxAudioSource.PlayOneShot(rewardSound);
                break;
            case SoundClips.Gameover:
                sfxAudioSource.volume = 1f;
                sfxAudioSource.PlayOneShot(clickSound);
                break;
            case SoundClips.Shoot:
                sfxAudioSource.volume = 1f;
                sfxAudioSource.PlayOneShot(victorySound);
                break;
            case SoundClips.Absorb:
                sfxAudioSource.volume = 1f;
                sfxAudioSource.PlayOneShot(gameOverSound);
                break;
            case SoundClips.Explosion:
                sfxAudioSource.volume = 1f;
                sfxAudioSource.PlayOneShot(powerUpSound);
                break;
            default:
                break;
        }
    }
}