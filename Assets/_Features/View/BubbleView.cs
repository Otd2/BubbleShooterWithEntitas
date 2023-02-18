using System;
using DG.Tweening;
using Entitas;
using Entitas.Unity;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class BubbleView : View, IValueListener, IInteractableListener, IFallListener
    , IBounceListener, IBombListener, IShootingBubbleListener, IFollowPathListener
    , IMapInitBubbleListener
{
    [SerializeField] private TextMeshPro Number;
    
    private SpriteRenderer _spriteRenderer;
    private Collider2D _collider;
    private Rigidbody2D _rigidbody;
    private Transform frame;
    private Tweener moveTween;

    public override void Link(IEntity entity)
    {
        base.Link(entity);
        
        //Find dependencies on the gameObject
        _collider = GetComponent<CircleCollider2D>();
        _rigidbody = GetComponent<Rigidbody2D>();
        frame = transform.Find("Frame");
        _spriteRenderer = GetComponent<SpriteRenderer>();
        
        //Stop physics
        if (_rigidbody != null)
            _rigidbody.bodyType = RigidbodyType2D.Kinematic;
        
        //Link listeners
        _linkedEntity.AddValueListener(this);
        _linkedEntity.AddInteractableListener(this);
        _linkedEntity.AddFallListener(this);
        _linkedEntity.AddBounceListener(this);
        _linkedEntity.AddBombListener(this);
        _linkedEntity.AddShootingBubbleListener(this);
        _linkedEntity.AddFollowPathListener(this);
        _linkedEntity.AddMapInitBubbleListener(this);
    }

    public override void OnPosition(GameEntity entity, Vector2 value)
    {
        moveTween?.Kill();

        if(!_linkedEntity.isFall)
            base.OnPosition(entity, value);
    }

    public void OnValue(GameEntity entity, int number)
    {
        Number.text = "" + BubbleNeighbourLogicService.FormatNumber(number);
        _spriteRenderer.color = Contexts.sharedInstance.config.pieceColorsConfig.value.GetColorWithNumber(number);
        frame.gameObject.SetActive(number >= 1024);
    }

    public Vector2Int GetCoordinate()
    {
        return _linkedEntity.coordinate.value;
    }

    protected override void OnDestroyView()
    {
        transform.DOKill();
        base.OnDestroyView();
    }

    public void OnInteractable(GameEntity entity)
    {
        _collider.enabled = entity.isInteractable;
    }

    public void OnFall(GameEntity entity)
    {
        moveTween?.Kill();
        _rigidbody.bodyType = RigidbodyType2D.Dynamic;
        _rigidbody.AddForce(new Vector2(Random.Range(-1f, 1f) * 2f, 1f), ForceMode2D.Impulse);
        _collider.enabled = false;
    }

    public void OnBounce(GameEntity entity, Vector2 from)
    {
        var direction = transform.position - (Vector3)from;
        moveTween = transform.DOMove(transform.position + direction.normalized * 0.15f, 0.16f)
            .SetEase(Ease.OutCubic)
            .SetLoops(2, LoopType.Yoyo);
        entity.RemoveBounce();
    }

    public void OnBomb(GameEntity entity)
    {
        entity.isDestroyed = true;
    }

    //On shooting bubble index changed
    public void OnShootingBubble(GameEntity entity, int shootIndex)
    {
        moveTween.Kill();
        if (shootIndex == 0)
        {
            moveTween = transform.DOMoveX(0, 0.3f);
            transform.DOScale(Vector3.one, 0.3f);
        }
        else if (shootIndex == 1)
        {
            transform.localScale = Vector3.one * 0.7f;
        }

        _collider.enabled = false;
    }

    public void OnFollowPath(GameEntity entity, Vector3[] path, float speed)
    {
        moveTween.Kill();
        var totalDistance = GetTotalDistance(path);
        var seconds = totalDistance / speed;
        moveTween = transform.DOPath(path, seconds).SetEase(Ease.Linear)
            .OnComplete(() => entity.isPathCompleted = true);
    }

    private float GetTotalDistance(Vector3[] path)
    {
        float totalDistance = 0;
        for (var index = 1; index < path.Length; index++)
        {
            var oldPoint = path[index-1];
            var point = path[index];
            totalDistance += Vector3.Distance(oldPoint, point);
        }

        return totalDistance;
    }

    public void OnMapInitBubble(GameEntity entity)
    {
        transform.localScale = Vector3.zero;
        transform.DOScale(Vector3.one, Random.Range(0.6f, 1f));
        _linkedEntity.isMapInitBubble = false;
    }

    public void Update()
    {
        if(_linkedEntity.isFall)
            _linkedEntity.ReplacePosition(transform.position);
    }
}