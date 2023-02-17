using Entitas;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ShootSystem : ReactiveSystem<GameEntity> {
    private Contexts _contexts;

    private GameObject dummyGameObject;

	public ShootSystem (Contexts contexts) : base(contexts.game) {
        _contexts = contexts;
        dummyGameObject = new GameObject("DOTweenDummyGameObject");
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
		dummyGameObject.transform.position = _contexts.game.aim.origin;
		var fixedTrajectory = trajectoryComp; 
		fixedTrajectory[^1] = 
			BubbleNeighbourLogicService.FromCoordToWorldPos(_contexts.game.targetCoordinate.value);
		var shootBubble = entities[0];
		shootBubble.AddFollowPath(fixedTrajectory.ToArray(), 0.35f);
		
		
		/*dummyGameObject.transform.DOPath(trajectoryComp.ToArray(), 0.5f)
			.SetEase(Ease.Linear)
			.OnUpdate(() =>
		{
			shootBubble.ReplacePosition(dummyGameObject.transform.position);
			
		}).OnComplete(() =>
		{
			//TODO : SPAWN NEW OBJECT AND THEN START MERGE
			shootBubble.isDestroyed = true;
			var newBubble = _contexts.game.CreateBoardBubble
				(shootBubble.value.Number, _contexts.game.targetCoordinate.value);
			newBubble.isNewCreated = true;
			newBubble.isFirstShotBubble = true;
		});
		*/
		
	}
}