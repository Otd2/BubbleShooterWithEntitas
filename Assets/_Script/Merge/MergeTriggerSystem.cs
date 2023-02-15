using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class MergeTriggerSystem : ReactiveSystem<GameEntity> {
    private Contexts _contexts;
    private List<Vector2> startPos; 

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
		var bubblesWillBeMerged = _contexts.game.GetEntities
			(GameMatcher.AllOf(GameMatcher.MergeFlag, GameMatcher.MergeStartPosition));
		startPos = new List<Vector2>();
		foreach (var bubble in bubblesWillBeMerged)
		{
			if (bubble.hasCoordinate)
			{
				bubble.RemoveCoordinate();
			}
			var newPos = Vector2.Lerp(bubble.mergeStartPosition.value,
				BubbleNeighbourLogicService.FromCoordToWorldPos(entities[0].merge.target),
				1 - ((entities[0].timer.time) / _contexts.config.gameConfig.value.MergeAndPopIntervalTime));
			
			bubble.ReplacePosition(newPos);
		}
	}
}