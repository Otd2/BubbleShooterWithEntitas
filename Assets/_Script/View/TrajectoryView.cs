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

        /*if (_linkedEntity.piece.Type >= 0)
        {
            var config = Contexts.sharedInstance.config.pieceColorsConfig.value;
            Sprite.color = config.Colors[_linkedEntity.piece.Type];
        }*/
    }

    public void OnTrajectory(GameEntity entity, List<Vector3> hitPoints)
    {
        if (lineRenderer == null) return;
        if (hitPoints == null) return;
        lineRenderer.positionCount = hitPoints.Count;
        lineRenderer.SetPositions(hitPoints.ToArray());
    }

    public void OnVisible(GameEntity entity, bool isVisible)
    {
        if (lineRenderer.enabled != isVisible)
            lineRenderer.enabled = isVisible;
    }
}
