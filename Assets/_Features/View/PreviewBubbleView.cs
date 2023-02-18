using System.Collections;
using DG.Tweening;
using Entitas;
using UnityEngine;

public class PreviewBubbleView : View, IValueListener, IVisibleListener
{
    public SpriteRenderer Sprite;

    public override void Link(IEntity entity)
    {
        Sprite = GetComponent<SpriteRenderer>();
        base.Link(entity);
        _linkedEntity.AddValueListener(this);
        _linkedEntity.AddVisibleListener(this);
        
    }

    public override void OnPosition(GameEntity entity, Vector2 value)
    {
        transform.DOKill();
        base.OnPosition(entity, value);
        transform.localScale = Vector3.zero;
        transform.DOScale(Vector3.one, 0.12f);
    }

    public void OnValue(GameEntity entity, int number)
    {
        var config = Contexts.sharedInstance.config.pieceColorsConfig.value;
        var color = config.GetColorWithNumber(number);
        Sprite.color = new Color(color.r, color.g, color.b, 0.5f);
    }

    public void OnVisible(GameEntity entity, bool isVisible)
    {
        Sprite.enabled = isVisible;
    }
}