using Entitas;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MergeControlSystem : ReactiveSystem<GameEntity> {
    private Contexts _contexts;
    private List<Vector2Int> searchedValues;
    private bool isMerge = false;

	public MergeControlSystem (Contexts contexts) : base(contexts.game) {
        _contexts = contexts;
	}

	protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) {
		return context.CreateCollector(GameMatcher.NewCreated);
	}

	protected override bool Filter(GameEntity entity)
	{
		return entity.isBubble && !entity.isShoot && entity.value.Number != 4096;
	}

	protected override void Execute(List<GameEntity> entities)
	{
		var newCreated = entities[0];
		
		isMerge = false;
		searchedValues = new List<Vector2Int>();
		if(newCreated.hasCoordinate)
			SearchForMerge(newCreated.coordinate.value, newCreated.value.Number);
		
		newCreated.isNewCreated = false;
		_contexts.game.isWaitForNextShoot = isMerge;
	}

	//Searches neighbours and flags the bubbles with the target value
	//When we find a bubble with same value, we search its neighbours recursively
	void SearchForMerge(Vector2Int coord, int number)
	{
		var neighbourCoordinates = BubbleNeighbourLogicService
			.GetNeighbourCoordinates(coord);
		
		foreach (var coordinate in neighbourCoordinates)
		{
			var tempEntity = _contexts.game.GetEntityWithCoordinate(coordinate);
			if (tempEntity is { hasValue: true, isMergeFlag: false })
			{
				if (!searchedValues.Contains(coordinate))
				{
					if (tempEntity.value.Number == number)
					{
						tempEntity.isMergeFlag = true;
						tempEntity.AddMergeStartPosition(tempEntity.position.Value);
						isMerge = true;
						searchedValues.Add(coordinate);
						SearchForMerge(coordinate, number);
						
					}
					searchedValues.Add(coordinate);
				}
			}
		}
	}
	
}