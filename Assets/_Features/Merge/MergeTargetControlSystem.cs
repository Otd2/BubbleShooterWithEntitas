using Entitas;
using System.Collections.Generic;
using UnityEngine;

public class MergeTargetControlSystem : ReactiveSystem<GameEntity> {
    private Contexts _contexts;

	public MergeTargetControlSystem (Contexts contexts) : base(contexts.game) {
        _contexts = contexts;
	}

	protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) {
		return context.CreateCollector(GameMatcher.MergeFlag);
	}

	protected override bool Filter(GameEntity entity)
	{
		return !entity.isDestroyed;
	}

	protected override void Execute(List<GameEntity> entities)
	{
		int totalMergeCount = entities.Count;
		int value = entities[0].value.Number << (totalMergeCount-1);

		bool isPossibleFollowingMerge = IsPossibleFollowingMerge(entities, value, out var targetCoordinate);
		
		if (isPossibleFollowingMerge)
		{
			SetMergeEntity(targetCoordinate, value);
		}
		else
		{
			SetMergeTargetRandomly(totalMergeCount, entities, value);
		}
	}

	private bool IsPossibleFollowingMerge(List<GameEntity> entities, int value, out Vector2Int targetCoordinate)
	{
		foreach (var e in entities)
		{
			var allNeighbours = BubbleNeighbourLogicService.GetNeighbourCoordinates(e.coordinate.value);
			foreach (var neighbour in allNeighbours)
			{
				var neighbourEntity = _contexts.game.GetEntityWithCoordinate(neighbour);
				if (neighbourEntity is { isMergeFlag: false, isBubble:true })
				{
					if (neighbourEntity.value.Number == value)
					{
						targetCoordinate = e.coordinate.value;
						return true;
					}
				}
			}
		}
		targetCoordinate = new Vector2Int(-100,-100);
		return false;
	}

	private void SetMergeTargetRandomly(int totalMergeCount, List<GameEntity> mergingEntities, int value)
	{
		var randomIndex = Random.Range(0, totalMergeCount - 1);
		var random = mergingEntities[randomIndex];
		SetMergeEntity(random.coordinate.value, value);
	}

	private void SetMergeEntity(Vector2Int coordinate, int value)
	{
		var mergeEntity = _contexts.game.CreateEntity();
		mergeEntity.AddMerge(coordinate, value);
		mergeEntity.AddTimer(_contexts.config.gameConfig.value.MergeAndPopIntervalTime);
		mergeEntity.isTimerComplete = false;
	}
}