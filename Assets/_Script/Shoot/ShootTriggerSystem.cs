using Entitas;
using System.Collections.Generic;
using UnityEngine;

public class ShootTriggerSystem : ReactiveSystem<InputEntity> {
	private Contexts _contexts;


	public ShootTriggerSystem (Contexts contexts) : base(contexts.input) {
		_contexts = contexts;
	}

	protected override ICollector<InputEntity> GetTrigger(IContext<InputEntity> context) {
		return context.CreateCollector(InputMatcher.Released);
	}

	protected override bool Filter(InputEntity entity) {
		return entity.isReleased 
		       && !_contexts.game.isWaitForNextShoot 
		       && _contexts.game.targetCoordinateEntity.isValidTarget;
	}

	protected override void Execute(List<InputEntity> entities)
	{
		var shootBubbles = _contexts.game.GetEntities(GameMatcher.ShootingBubble);
		foreach (var shootBubble in shootBubbles)
		{
			if (shootBubble.shootingBubble.shootIndex == 0)
			{
				shootBubble.isShoot = true;
				entities[0].isReleased = false;
			}
		}
	}
}