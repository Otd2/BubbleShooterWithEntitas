using Entitas;
using UnityEngine;


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