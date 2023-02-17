using Entitas;
using System.Collections.Generic;
using UnityEngine;

using Entitas;

public class TrajectorySystem : IExecuteSystem, IInitializeSystem {
	private Contexts _contexts;
	private readonly RayService _rayService;

	public TrajectorySystem (Contexts contexts, RayService rayService)
	{
		_contexts = contexts;
		_rayService = rayService;
	}

	public void Execute()
	{
		var aimEntity = _contexts.game.GetEntities(GameMatcher.Aim)[0];
		var targetCoordinateEntity = _contexts.game.targetCoordinateEntity;
		var trajectoryEntity = _contexts.game.trajectoryEntity;
		var previewEntity = _contexts.game.previewEntity;
		
		if (!_contexts.input.touchPositionEntity.isTouching)
		{
			if (trajectoryEntity.visible.isVisible)
			{
				trajectoryEntity.ReplaceVisible(false);
				previewEntity.ReplaceVisible(false);
			}
			if (_contexts.game.isWaitForNextShoot)
			{
				targetCoordinateEntity.isValidTarget = false;
			}
			return;
		}
		
		
		var oldCoordinate = targetCoordinateEntity.targetCoordinate.value;
		Vector2Int newCoordinate = oldCoordinate;
		if (_rayService.GetTargetCoordinates(aimEntity.aim.origin,
			    aimEntity.aim.normalizedDir, out newCoordinate))
		{
			if (newCoordinate != oldCoordinate)
			{
				targetCoordinateEntity.ReplaceTargetCoordinate(newCoordinate);
			}
		}
		else
		{
			targetCoordinateEntity.isValidTarget = false;
			trajectoryEntity.ReplaceVisible(false);
			previewEntity.ReplaceVisible(false);
			return;
		}

		targetCoordinateEntity.isValidTarget = true;
		_contexts.game.ReplaceTrajectory(_rayService.HitPositions);
		
		if (!trajectoryEntity.visible.isVisible)
		{
			trajectoryEntity.ReplaceVisible(true);
			previewEntity.ReplaceVisible(true);
		}

	}

	public void Initialize()
	{
		RayService.CreateTrajectoryView(_contexts.game);
	}
}

/*public class TrajectorySystem : ReactiveSystem<GameEntity>, IInitializeSystem {
    private Contexts _contexts;
    private readonly RayService _rayService;

    public TrajectorySystem (Contexts contexts, RayService rayService) : base(contexts.game)
    {
	    _contexts = contexts;
	    _rayService = rayService;
    }

	protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
	{
		return context.CreateCollector(GameMatcher.Aim);
	}

	protected override bool Filter(GameEntity entity)
	{
		return !_contexts.game.isWaitForNextShoot;
	}

	protected override void Execute(List<GameEntity> entities)
	{
		Vector2 origin = Vector2.one;
		foreach (var e in entities)
		{
			_contexts.game.trajectoryEntity.isVisible = e.isVisible;
			origin = e.aim.origin;
			_rayService.ShootRay(e.aim.origin, e.aim.normalizedDir);
			_contexts.game.ReplaceTrajectory(_rayService.HitPositions);
			_contexts.game.trajectoryEntity.isVisible = _contexts.input.touchPositionEntity.isTouching;

		}

		foreach (var point in _contexts.game.trajectory.hitPoints)
		{
			//Debug.DrawLine(origin, point);
			origin = point;
		}
	}

	public void Initialize()
	{
		RayService.CreateTrajectoryView(_contexts.game);
	}
}*/