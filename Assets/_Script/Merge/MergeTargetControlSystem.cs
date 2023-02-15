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
		Debug.Log("MERGE OF " + totalMergeCount + " BUBBLES WITH " + entities[0].value.Number +" IS : " + value);
		Vector2Int targetCoordinate = new Vector2Int(-100,-100);
		bool isMergeTargetCalculated = false;
		foreach (var e in entities)
		{
			var allNeighbours = BubbleNeighbourLogicService.GetNeighbourCoordinates(e.coordinate.value);
			foreach (var neighbour in allNeighbours)
			{
				var neighbourEntity = _contexts.game.GetEntityWithCoordinate(neighbour);
				if (neighbourEntity is { isMergeFlag: false })
				{
					if (neighbourEntity.value.Number == value)
					{
						targetCoordinate = e.coordinate.value;
						isMergeTargetCalculated = true;
					}
				}
			}
		}

		if (isMergeTargetCalculated)
		{
			Debug.Log("TARGET TO  : " + targetCoordinate);
			var mergeEntity = _contexts.game.CreateEntity();
			mergeEntity.AddMerge(targetCoordinate, value);
			mergeEntity.AddTimer(_contexts.config.gameConfig.value.MergeAndPopIntervalTime);
			mergeEntity.isTimerComplete = false;
		}
		else
		{
			var randomIndex = Random.Range(0, totalMergeCount - 1);
			var random = entities[randomIndex];
			var mergeEntity = _contexts.game.CreateEntity();
			mergeEntity.AddMerge(random.coordinate.value, value);
			mergeEntity.AddTimer(_contexts.config.gameConfig.value.MergeAndPopIntervalTime);
			mergeEntity.isTimerComplete = false;
			
		}
	}
}