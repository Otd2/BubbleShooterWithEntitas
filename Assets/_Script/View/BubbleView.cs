using DG.Tweening;
using Entitas;
using Entitas.Unity;
using TMPro;
using UnityEngine;

public class BubbleView : View, IValueListener, IInteractableListener, IFallListener
    , IBounceListener, IBombListener, IShootingBubbleListener, IFollowPathListener
{
    public SpriteRenderer Sprite;
    public TextMeshPro Number;
    public float DestroyDuration=0.5f;
    public Collider2D collider;
    private Rigidbody2D _rigidbody;
    private Transform frame;

    public override void Link(IEntity entity)
    {
        base.Link(entity);
        collider = GetComponent<CircleCollider2D>();
        _rigidbody = GetComponent<Rigidbody2D>();
        frame = transform.Find("Frame");
        Sprite = GetComponent<SpriteRenderer>();
        if(_rigidbody != null)
            _rigidbody.bodyType = RigidbodyType2D.Kinematic;
        _linkedEntity.AddValueListener(this);
        _linkedEntity.AddInteractableListener(this);
        _linkedEntity.AddFallListener(this);
        _linkedEntity.AddBounceListener(this);
        _linkedEntity.AddBombListener(this);
        _linkedEntity.AddShootingBubbleListener(this);
        _linkedEntity.AddFollowPathListener(this);
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
        Number.text = "" + FormatNumber(number);
        Sprite.color = Contexts.sharedInstance.config.pieceColorsConfig.value.GetColorWithNumber(number);
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
        _rigidbody.AddForce(new Vector2(Random.Range(-1f, 1f) * 5f, 1f), ForceMode2D.Impulse);
        collider.enabled = false;
    }

    public void OnBounce(GameEntity entity, Vector2 from)
    {
        var direction = transform.position - (Vector3)from;
        transform.DOMove(transform.position + direction.normalized * 0.15f, 0.16f)
            .SetEase(Ease.OutCubic)
            .SetLoops(2, LoopType.Yoyo);
        entity.RemoveBounce();
    }

    public void OnBomb(GameEntity entity)
    {
        Debug.Log("BOMB");
        //PARTICLE
        entity.isDestroyed = true;
    }
    
    static string FormatNumber(int num)
    {

        if (num >= 1000)
            return (num / 1000).ToString("0") + "K";
        else 
            return num.ToString();
    }

    public void OnShootingBubble(GameEntity entity, int shootIndex)
    {
        transform.DOKill();
        if (shootIndex == 0)
        {
            transform.DOMoveX(0, 0.3f);
            transform.DOScale(Vector3.one, 0.3f);
        }
        else if(shootIndex == 1)
        {
            transform.localScale = Vector3.one * 0.7f;
        }

        collider.enabled = false;
    }

    public void OnFollowPath(GameEntity entity, Vector3[] path, float seconds)
    {
        transform.DOKill();
        transform.DOPath(path, seconds).SetEase(Ease.Linear)
            .OnComplete(()=>entity.isPathCompleted = true);
    }
}