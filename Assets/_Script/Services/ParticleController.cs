﻿
using UnityEngine;
using UnityEngine.Pool;
[RequireComponent(typeof(ParticleSystem))]
public class ParticleController : MonoBehaviour
{
    
    public ParticleSystem system;
    public IObjectPool<ParticleSystem> pool;

    void Start()
    {
        system = GetComponent<ParticleSystem>();
        var main = system.main;
        main.stopAction = ParticleSystemStopAction.Callback;
    }

    void OnParticleSystemStopped()
    {
        // Return to the pool
        pool.Release(system);
    }
}
