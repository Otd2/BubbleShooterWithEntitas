using Entitas;
using UnityEngine;

public class MapInitSystem : IInitializeSystem  {
	private Contexts _contexts;

    public MapInitSystem(Contexts contexts) {
    	_contexts = contexts;
    }

	public void Initialize() {
		var entity = _contexts.game.CreateEntity();
		var boardSize = _contexts.config.gameConfig.value.BoardSize;
		entity.AddMapSize(boardSize);
		BubbleNeighbourLogicService.Init();
		for (int y = 0; y < boardSize.y; y++)
		{
			var oldCoordinate = new Coordinate(new Vector2Int(y%2, y));
			_contexts.game.CreateRandomBubble(oldCoordinate);
			for (int x = 1; x < boardSize.x; x++)
			{
				var rightCoord = BubbleNeighbourLogicService.GetNeighborCoordinate(oldCoordinate.Value, Direction.E);
				oldCoordinate = new Coordinate(rightCoord);
				_contexts.game.CreateRandomBubble(oldCoordinate);
			}
		}
	}		
}