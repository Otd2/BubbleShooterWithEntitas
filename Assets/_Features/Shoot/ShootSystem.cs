using Entitas;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ShootSystem : ReactiveSystem<GameEntity> {
    private Contexts _contexts;

    public ShootSystem (Contexts contexts) : base(contexts.game) {
        _contexts = contexts;
	}

	protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) {
		return context.CreateCollector(GameMatcher.Shoot);
	}

	protected override bool Filter(GameEntity entity)
	{
		return true;
	}

	protected override void Execute(List<GameEntity> entities)
	{
		_contexts.game.isWaitForNextShoot = true;
		
		var trajectoryComp = _contexts.game.trajectory.hitPoints;
		var fixedTrajectory = trajectoryComp; 
		
		//Fixing the last point of the trajectory to snap to the target bubble
		fixedTrajectory[^1] = 
			BubbleNeighbourLogicService.FromCoordToWorldPos(_contexts.game.targetCoordinate.value);
		
		var shootBubble = entities[0];
		//Adds path follower to shoot bubble
		shootBubble.AddFollowPath(fixedTrajectory.ToArray(), _contexts.config.gameConfig.value.ShootSpeed);
		
	}
}