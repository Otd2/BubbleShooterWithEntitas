using Entitas;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MergeControlSystem : ReactiveSystem<GameEntity> {
    private Contexts _contexts;
    private List<Vector2Int> searchedValues;
    private bool isMerge = false;
    private GameEntity target;

	public MergeControlSystem (Contexts contexts) : base(contexts.game) {
        _contexts = contexts;
	}

	protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) {
		return context.CreateCollector(GameMatcher.NewCreated);
	}

	protected override bool Filter(GameEntity entity)
	{
		return entity.isBubble && !entity.isShootingBubble;
	}

	protected override void Execute(List<GameEntity> entities)
	{
		var newCreated = entities[0];
		//TODO: 1. LOOK FOR SAME VALUED NEIGHBOURS WILL BE MERGED
		//TODO: 2. SEARCH FOR BEST POSSIBLE NEIGHBOUR WITH THE TARGET MERGE VALUE (IF NOT MORE THAN 2048)
		//TODO: 3. ADD MERGE COMPONENT TO THOSE BUBBLES WITH TARGET COORDINATE


		/*foreach (var neighbourCoordinate in allBubbles)
		{
			
		}*/
		isMerge = false;
		searchedValues = new List<Vector2Int>();
		SearchForMerge(newCreated.coordinate.value, newCreated.value.Number);
		newCreated.isNewCreated = false;
		if (!isMerge)
		{
			_contexts.game.isWaitForNextShoot = false;
		}
		else
		{
			_contexts.game.isWaitForNextShoot = true;
		}
	}

	void SearchForMerge(Vector2Int coord, int number)
	{
		//Debug.Log("Searching for : " + number + " around : " + coord.x + ", " + coord.y);
		var neighbourCoordinates = BubbleNeighbourLogicService
			.GetNeighbourCoordinates(coord);
		
		//Debug.Log("neighbourCount = " + neighbourCoordinates.Count);

		foreach (var coordinate in neighbourCoordinates)
		{
			//Debug.Log("Searching = " + + coordinate.x + ", " + coordinate.y);
			var tempEntity = _contexts.game.GetEntityWithCoordinate(coordinate);
			if (tempEntity is { hasValue: true, isMergeFlag: false })
			{
				
				//Debug.Log("Has game entity at " + coordinate.x + ", " + coordinate.y);
				if (!searchedValues.Contains(coordinate))
				{
					if (tempEntity.value.Number == number)
					{
						//Debug.Log("Marked at " + coordinate.x + ", " + coordinate.y);
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