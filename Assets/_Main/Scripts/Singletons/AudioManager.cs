using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SoundClips
{
    MouseClick,
    Win,
    Gameover,
    MagicalAttack,
    PhysicalAttack,
    Dead,
    Damage,
    Negative,
    ReloadMana
}

public enum EnemySoundClips
{
    FlyAttack,
    FlyDamage,
    FlyDead,
    StaticAttack,
    StaticDamage,
    StaticDead,
    PatrolAttack,
    PatrolDamage,
    PatrolDead
}

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("AudioSources")]
    [SerializeField] private AudioSource musicAudioSource;
    [SerializeField] private AudioSource playerAudioSource;
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
    [SerializeField] private AudioClip rewardSound;
    [SerializeField] private AudioClip powerUpSound;

    [Header("Player Sounds")]
    [SerializeField] private AudioClip magicalAttackSound;
    [SerializeField] private AudioClip physicalAttackSound;
    [SerializeField] private AudioClip reloadManaSound;
    [SerializeField] private AudioClip negativeSound;
    [SerializeField] private AudioClip damageSound;
    [SerializeField] private AudioClip deathSound;

    [Header("Enemy Patrol Sounds")]
    [SerializeField] private AudioClip enemyPatrolAttackSound;
    [SerializeField] private AudioClip enemyPatrolDamageSound;
    [SerializeField] private AudioClip enemyPatrolDeathSound;

    [Header("Enemy Static Sounds")]
    [SerializeField] private AudioClip enemyStaticAttackSound;
    [SerializeField] private AudioClip enemyStaticDamageSound;
    [SerializeField] private AudioClip enemyStaticDeathSound;

    [Header("Enemy Fly Sounds")]
    [SerializeField] private AudioClip enemyFlyAttackSound;
    [SerializeField] private AudioClip enemyFlyDamageSound;
    [SerializeField] private AudioClip enemyFlyDeathSound;

    [Header("Boss Sounds")]
    [SerializeField] private AudioClip bossAttackSound;
    [SerializeField] private AudioClip bossDamageSound;
    [SerializeField] private AudioClip bossDeathSound;

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
        //TODO: Subscribirse a la victoria, game over.
    }

    public void PlayPlayerSound(SoundClips soundClip)
    {
        playerAudioSource.volume = 1f;
        switch (soundClip)
        {
            case SoundClips.MouseClick:
                playerAudioSource.PlayOneShot(clickSound);
                break;
            case SoundClips.Win:
                playerAudioSource.PlayOneShot(victorySound);
                break;
            case SoundClips.Gameover:
                playerAudioSource.PlayOneShot(gameOverSound);
                break;
            case SoundClips.MagicalAttack:
                playerAudioSource.PlayOneShot(magicalAttackSound);
                break;
            case SoundClips.PhysicalAttack:
                playerAudioSource.PlayOneShot(physicalAttackSound);
                break;
            case SoundClips.Dead:
                playerAudioSource.PlayOneShot(deathSound);
                break;
            case SoundClips.Negative:
                playerAudioSource.PlayOneShot(negativeSound);
                break;
            case SoundClips.ReloadMana:
                playerAudioSource.PlayOneShot(reloadManaSound);
                break;
            case SoundClips.Damage:
                playerAudioSource.PlayOneShot(reloadManaSound);
                break;
        }
    }

    public void PlayEnemySound(EnemySoundClips soundClip)
    {
        sfxAudioSource.volume = 1f;
        switch (soundClip)
        {
            case EnemySoundClips.FlyAttack:
                sfxAudioSource.PlayOneShot(enemyFlyAttackSound);
                break;
            case EnemySoundClips.FlyDamage:
                sfxAudioSource.PlayOneShot(enemyFlyDamageSound);
                break;
            case EnemySoundClips.FlyDead:
                sfxAudioSource.PlayOneShot(enemyFlyDeathSound);
                break;
            case EnemySoundClips.StaticAttack:
                sfxAudioSource.PlayOneShot(enemyStaticAttackSound);
                break;
            case EnemySoundClips.StaticDamage:
                sfxAudioSource.PlayOneShot(enemyStaticDamageSound);
                break;
            case EnemySoundClips.StaticDead:
                sfxAudioSource.PlayOneShot(enemyStaticDeathSound);
                break;
            case EnemySoundClips.PatrolAttack:
                sfxAudioSource.PlayOneShot(enemyPatrolAttackSound);
                break;
            case EnemySoundClips.PatrolDamage:
                sfxAudioSource.PlayOneShot(enemyPatrolDamageSound);
                break;
            case EnemySoundClips.PatrolDead:
                sfxAudioSource.PlayOneShot(enemyPatrolDeathSound);
                break;
        }
    }
}