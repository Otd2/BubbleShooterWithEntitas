using Entitas;
using UnityEngine;

public class MapInitSystem : IInitializeSystem  {
	private Contexts _contexts;

    public MapInitSystem(Contexts contexts) {
    	_contexts = contexts;
    }

    private Vector2Int topLayerStartCoordinate = new Vector2Int(1, -1);

	public void Initialize() {
		var entity = _contexts.game.CreateEntity();
		var boardSize = _contexts.config.gameConfig.value.BoardSize;
		entity.AddMapSize(boardSize);
		BubbleNeighbourLogicService.Init();

		var topLayerCoord = topLayerStartCoordinate;
		var topLayerEntity = _contexts.game.CreateTopLayerBubble(topLayerCoord);
		//CreateTopLayer
		for (int i = 1; i < boardSize.x; i++)
		{
			topLayerCoord = BubbleNeighbourLogicService.
				GetNeighborCoordinate(topLayerCoord, Direction.E);
			topLayerEntity = _contexts.game.CreateTopLayerBubble(topLayerCoord);
		}
		
		for (int y = 0; y < 1; y++)
		{
			var oldCoordinate = new Vector2Int(y%2, y);
			_contexts.game.CreateRandomBubble(oldCoordinate);
			for (int x = 1; x < boardSize.x; x++)
			{
				var rightCoord = BubbleNeighbourLogicService.GetNeighborCoordinate(oldCoordinate, Direction.E);
				oldCoordinate = rightCoord;
				_contexts.game.CreateRandomBubble(oldCoordinate);
			}
		}
		
		
	}		
}