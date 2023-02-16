using DG.Tweening;
using Entitas;
using TMPro;
using UnityEngine;

public class BubbleView : View, IValueListener, IInteractableListener, IFallListener, IBounceListener
{
    public SpriteRenderer Sprite;
    public TextMeshPro Number;
    public float DestroyDuration=0.5f;
    public Collider2D collider;
    private Rigidbody2D _rigidbody;

    public override void Link(IEntity entity)
    {
        base.Link(entity);
        collider = GetComponent<CircleCollider2D>();
        _rigidbody = GetComponent<Rigidbody2D>();
        if(_rigidbody != null)
            _rigidbody.bodyType = RigidbodyType2D.Kinematic;
        _linkedEntity.AddValueListener(this);
        _linkedEntity.AddInteractableListener(this);
        _linkedEntity.AddFallListener(this);
        _linkedEntity.AddBounceListener(this);
        
        /*if (_linkedEntity.piece.Type >= 0)
        {
            var config = Contexts.sharedInstance.config.pieceColorsConfig.value;
            Sprite.color = config.Colors[_linkedEntity.piece.Type];
        }*/
    }

    public override void OnPosition(GameEntity entity, Vector2 value)
    {
        transform.DOKill();
        base.OnPosition(entity, value);
        
        //transform.DOKill();
        /*var isTopRow = value.y == Contexts.sharedInstance.game.board.Size.y - 1;
        if (isTopRow)
            transform.localPosition = new Vector3(value.x, value.y + 1);
            */
        //transform.DOMove(new Vector3(value.x, value.y, 0f), 0.3f);
    }
    
    public void OnValue(GameEntity entity, int number)
    {
        Number.text = "" + number;
    }

    public Vector2Int GetCoordinate()
    {
        return _linkedEntity.coordinate.value;
    }

    protected override void OnDestroyView()
    {
        transform.DOKill();
        Debug.Log("CALLED BUBBLE DESTROY");
        base.OnDestroyView();
        /* var color = Sprite.color;
         color.a = 0f;
         Sprite.material.DOColor(color, 0.1f);
         gameObject.transform
             .DOScale(Vector3.one * 1.5f, 0.1f)
             .OnComplete(base.OnDestroy);*/
    }

    public void OnInteractable(GameEntity entity)
    {
        collider.enabled = entity.isInteractable;
    }

    public void OnFall(GameEntity entity)
    {
        transform.DOKill();
        _rigidbody.bodyType = RigidbodyType2D.Dynamic;
        _rigidbody.AddForce(new Vector2(Random.Range(-1f, 1f) * 5f, 1f));
    }

    public void OnBounce(GameEntity entity, Vector2 from)
    {
        var direction = transform.position - (Vector3)from;
        transform.DOMove(transform.position + direction.normalized * 0.1f, 0.085f)
            .SetEase(Ease.OutQuart)
            .SetLoops(2, LoopType.Yoyo);
        entity.RemoveBounce();
    }
}