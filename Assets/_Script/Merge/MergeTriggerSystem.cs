using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class MergeTriggerSystem : ReactiveSystem<GameEntity> {
    private Contexts _contexts;

	public MergeTriggerSystem (Contexts contexts) : base(contexts.game) {
        _contexts = contexts;
	}

	protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) {
		return context.CreateCollector(GameMatcher.AllOf(GameMatcher.Merge, GameMatcher.Timer));
	}

	protected override bool Filter(GameEntity entity)
	{
		return !entity.isTimerComplete;
	}

	protected override void Execute(List<GameEntity> entities)
	{
		var bubblesWillBeMerged = _contexts.game.GetEntities(GameMatcher.MergeFlag);
		foreach (var bubble in bubblesWillBeMerged)
		{
			if (bubble.hasCoordinate)
			{
				bubble.RemoveCoordinate();
			}
			var newPos = Vector2.Lerp(bubble.position.Value,
				BubbleNeighbourLogicService.FromCoordToWorldPos(entities[0].merge.target),
				1f-entities[0].timer.time);
			
			bubble.ReplacePosition(newPos);
		}
	}
}