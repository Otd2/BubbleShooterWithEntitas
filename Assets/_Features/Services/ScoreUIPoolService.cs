using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Pool;

public class ScoreUIPoolService: MonoBehaviour, IScoreUIPoolService
{    
    // Collection checks will throw errors if we try to release an item that is already in the pool.
    public bool collectionChecks = true;
    public int maxPoolSize = 20;
    public TextMeshPro poolPrefab;
    
    private Transform parent;
    IObjectPool<TextMeshPro> m_Pool;

    public IObjectPool<TextMeshPro> Pool
    {
        get
        {
            if (m_Pool == null)
            {

                m_Pool = new ObjectPool<TextMeshPro>(CreatePooledItem, OnTakeFromPool, OnReturnedToPool,
                    OnDestroyPoolObject, collectionChecks, 10, maxPoolSize);
            }

            return m_Pool;
        }
    }

    public void ScoreEffect(Vector3 position, int score)
    {
        var text = Pool.Get();
        text.text = "" + BubbleNeighbourLogicService.FormatNumber(score);
        text.transform.position = position;
        text.color = Color.white;
        text.transform.localScale = Vector3.zero;
        text.transform.DOScale(Vector3.one, 0.5f);
        text.DOFade(0, 1f).SetDelay(0.5f).SetEase(Ease.InCubic);
        text.transform.DOMoveY(text.transform.position.y + 1f, 1.5f)
            .OnComplete(()=>Pool.Release(text));
    }

    public void Init()
    {
        GameObject particlesContainer = new GameObject("ScoreUIEffects");
        parent = particlesContainer.transform;
        for (int i = 0; i < maxPoolSize; i++)
        {
            CreatePooledItem();
        }
    }
    
    TextMeshPro CreatePooledItem()
    {
        var go = Instantiate(poolPrefab.gameObject, parent);
        var textMesh = go.GetComponent<TextMeshPro>();
        textMesh.color = Color.clear;
        
        return textMesh;
    }

    // Called when an item is returned to the pool using Release
    void OnReturnedToPool(TextMeshPro system)
    {
        system.gameObject.SetActive(false);
    }

    // Called when an item is taken from the pool using Get
    void OnTakeFromPool(TextMeshPro system)
    {
        system.gameObject.SetActive(true);
    }

    // If the pool capacity is reached then any items returned will be destroyed.
    // We can control what the destroy behavior does, here we destroy the GameObject.
    void OnDestroyPoolObject(TextMeshPro system)
    {
        Destroy(system.gameObject);
    }
}