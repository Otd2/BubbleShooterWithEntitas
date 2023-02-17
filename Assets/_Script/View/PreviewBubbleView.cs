using System.Collections;
using DG.Tweening;
using Entitas;
using UnityEngine;

public class PreviewBubbleView : View, IValueListener, IVisibleListener
{
    public SpriteRenderer Sprite;
    public float DestroyDuration;

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
        Debug.Log("OnPositionChange : " + value);
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

    public void OnVisible(GameEntity entity, bool isVisible)
    {
        Debug.Log("IS PREVIEW VISIBLE : " + isVisible);
        Sprite.enabled = isVisible;
    }
}