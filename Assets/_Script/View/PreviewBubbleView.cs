using DG.Tweening;
using Entitas;
using UnityEngine;

public class PreviewBubbleView : View, IValueListener
{
    public SpriteRenderer Sprite;
    public float DestroyDuration;

    public override void Link(IEntity entity)
    {
        base.Link(entity);
        _linkedEntity.AddValueListener(this);
    }

    public override void OnPosition(GameEntity entity, Vector2 value)
    {
        transform.DOKill();
    }
    
    public void OnValue(GameEntity entity, int number)
    {
        //TODO: COLOR CHANGE
        /*
        var config = Contexts.sharedInstance.config.pieceColorsConfig.value;
        Sprite.color = config.Colors[_linkedEntity.piece.Type];
        */
    }

    protected override void OnDestroyView()
    {
        base.OnDestroyView();
       /* var color = Sprite.color;
        color.a = 0f;
        Sprite.material.DOColor(color, DestroyDuration);
        gameObject.transform
            .DOScale(Vector3.one * 1.5f, DestroyDuration)
            .OnComplete(base.OnDestroy);
            */
    }
    
}