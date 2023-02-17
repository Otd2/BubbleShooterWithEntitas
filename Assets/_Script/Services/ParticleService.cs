
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class ParticleService : MonoBehaviour, IParticlesService
{
    // Collection checks will throw errors if we try to release an item that is already in the pool.
    public bool collectionChecks = true;
    public int maxPoolSize = 20;
    public ParticleSystem poolPrefab;
    public ParticleSystem bombPrefab;

    IObjectPool<ParticleSystem> m_Pool;

    public IObjectPool<ParticleSystem> Pool
    {
        get
        {
            if (m_Pool == null)
            {

                m_Pool = new ObjectPool<ParticleSystem>(CreatePooledItem, OnTakeFromPool, OnReturnedToPool,
                    OnDestroyPoolObject, collectionChecks, 10, maxPoolSize);
            }
            return m_Pool;
        }
    }

    public void BompParticle(Vector3 position)
    {
        bombPrefab.gameObject.SetActive(true);
        bombPrefab.transform.position = position;
        bombPrefab.Play();
    }

    public void Init()
    {
        for (int i = 0; i < maxPoolSize; i++)
        {
            CreatePooledItem();
        }
    }

    ParticleSystem CreatePooledItem()
    {
        var go = Instantiate(poolPrefab.gameObject);
        var ps = go.GetComponent<ParticleSystem>();
        ps.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
    
        // This is used to return ParticleSystems to the pool when they have stopped.
        var returnToPool = go.GetComponent<ParticleController>();
        returnToPool.pool = Pool;

        return ps;
    }

    // Called when an item is returned to the pool using Release
    void OnReturnedToPool(ParticleSystem system)
    {
        system.gameObject.SetActive(false);
    }

    // Called when an item is taken from the pool using Get
    void OnTakeFromPool(ParticleSystem system)
    {
        system.gameObject.SetActive(true);
    }

    // If the pool capacity is reached then any items returned will be destroyed.
    // We can control what the destroy behavior does, here we destroy the GameObject.
    void OnDestroyPoolObject(ParticleSystem system)
    {
        Destroy(system.gameObject);
    }

    public void PopParticle(Vector3 position, Color color)
    {
        var particle = Pool.Get();
        var main = particle.main;
        main.startColor = color;
        particle.transform.position = position;
        particle.Play();
    }
}