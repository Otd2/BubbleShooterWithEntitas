using Entitas;
using System.Collections.Generic;
using UnityEngine;

public class TrajectorySystem : ReactiveSystem<GameEntity>, IInitializeSystem {
    private GameContext _contexts;
    private readonly RayService _rayService;

    public TrajectorySystem (GameContext contexts, RayService rayService) : base(contexts)
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
		return !_contexts.isShootTrigger;
	}

	protected override void Execute(List<GameEntity> entities)
	{
		Vector2 origin = Vector2.one;
		foreach (var e in entities)
		{
			origin = e.aim.origin;
			_rayService.ShootRay(e.aim.origin, e.aim.normalizedDir);
			_contexts.ReplaceTrajectory(_rayService.HitPositions);
		}

		foreach (var point in _contexts.trajectory.hitPoints)
		{
			Debug.DrawLine(origin, point);
			origin = point;
		}
	}

	public void Initialize()
	{
		RayService.CreateTrajectoryView(_contexts);
	}
}