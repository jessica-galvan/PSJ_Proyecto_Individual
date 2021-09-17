using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    [SerializeField] private List<ParticleSystem> particleSystems = new List<ParticleSystem>();

    private void Start()
    {
        Stop();
    }

    public void Play()
    {
        foreach (var particle in particleSystems)
        {
            particle.Play();
        }
    }

    public void Stop()
    {
        foreach (var particle in particleSystems)
        {
            particle.Stop();
        }
    }
}
