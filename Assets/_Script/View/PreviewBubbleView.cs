﻿using DG.Tweening;
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
        /*if (_linkedEntity.piece.Type >= 0)
        {
            var config = Contexts.sharedInstance.config.pieceColorsConfig.value;
            Sprite.color = config.Colors[_linkedEntity.piece.Type];
        }*/
    }

    public override void OnPosition(GameEntity entity, Vector2 value)
    {
        transform.DOKill();
        /*var isTopRow = value.y == Contexts.sharedInstance.game.board.Size.y - 1;
        if (isTopRow)
            transform.localPosition = new Vector3(value.x, value.y + 1);
            */

        //transform.DOMove(new Vector3(value.x, value.y, 0f), 0.3f);
    }
    
    public void OnValue(GameEntity entity, int number)
    {
        
    }

    protected override void OnDestroy()
    {
        var color = Sprite.color;
        color.a = 0f;
        Sprite.material.DOColor(color, DestroyDuration);
        gameObject.transform
            .DOScale(Vector3.one * 1.5f, DestroyDuration)
            .OnComplete(base.OnDestroy);
    }
    
}