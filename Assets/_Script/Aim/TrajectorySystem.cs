using Entitas;
using System.Collections.Generic;
using UnityEngine;

public class TrajectorySystem : ReactiveSystem<GameEntity>, IInitializeSystem {
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
}