using System.Collections;
using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class TrajectoryView : View, ITrajectoryListener, IVisibleListener
{
    private LineRenderer lineRenderer;
    public override void Link(IEntity entity)
    {
        base.Link(entity);
        lineRenderer = gameObject.GetComponent<LineRenderer>();
        _linkedEntity.AddTrajectoryListener(this);
        _linkedEntity.AddVisibleListener(this);
        lineRenderer.enabled = false;

        /*if (_linkedEntity.piece.Type >= 0)
        {
            var config = Contexts.sharedInstance.config.pieceColorsConfig.value;
            Sprite.color = config.Colors[_linkedEntity.piece.Type];
        }*/
    }

    public void OnTrajectory(GameEntity entity, List<Vector3> hitPoints)
    {
        lineRenderer.positionCount = hitPoints.Count;
        lineRenderer.SetPositions(hitPoints.ToArray());
    }

    public void OnVisible(GameEntity entity)
    {
        lineRenderer.enabled = entity.isVisible;
    }
}
