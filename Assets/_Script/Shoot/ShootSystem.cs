using Entitas;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ShootSystem : ReactiveSystem<InputEntity> {
    private Contexts _contexts;

    private GameObject dummyGameObject;

	public ShootSystem (Contexts contexts) : base(contexts.input) {
        _contexts = contexts;
        dummyGameObject = new GameObject("DOTweenDummyGameObject");
	}

	protected override ICollector<InputEntity> GetTrigger(IContext<InputEntity> context) {
		return context.CreateCollector(InputMatcher.Released);
	}

	protected override bool Filter(InputEntity entity) {
        return entity.isReleased && !_contexts.game.isWaitForNextShoot ;
	}

	protected override void Execute(List<InputEntity> entities)
	{
		_contexts.game.isWaitForNextShoot = true;
		var shootBubble = BubbleContextExtension.CreateBubbleForShoot(_contexts.game, 2, _contexts.game.aim.origin);
		var trajectoryComp = _contexts.game.trajectory.hitPoints;
		dummyGameObject.transform.position = _contexts.game.aim.origin;
		trajectoryComp[^1] =
			BubbleNeighbourLogicService.FromCoordToWorldPos(_contexts.game.targetCoordinate.value);
		
		
		dummyGameObject.transform.DOPath(trajectoryComp.ToArray(), 0.5f)
			.SetEase(Ease.Linear)
			.OnUpdate(() =>
		{
			shootBubble.ReplacePosition(dummyGameObject.transform.position);
			
		}).OnComplete(() =>
		{
			//TODO : SPAWN NEW OBJECT AND THEN START MERGE
			shootBubble.isDestroyed = true;
			var newBubble = _contexts.game.CreateBoardBubble(shootBubble.value.Number, _contexts.game.targetCoordinate.value);
			newBubble.isNewCreated = true;
			newBubble.isFirstShotBubble = true;
		});
		entities[0].isReleased = false;
	}
}