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
		_contexts.game.CreateTopLayerBubble(topLayerCoord);
		
		//CreateTopLayer
		for (int i = 1; i < boardSize.x; i++)
		{
			topLayerCoord = BubbleNeighbourLogicService.
				GetNeighborCoordinate(topLayerCoord, Direction.E);
			
			_contexts.game.CreateTopLayerBubble(topLayerCoord);
		}
		
		for (int y = 0; y < boardSize.y; y++)
		{
			var oldCoordinate = new Vector2Int(y%2, y);
			var bubble = _contexts.game.CreateRandomBubble(oldCoordinate);
			bubble.isMapInitBubble = true;
			for (int x = 1; x < boardSize.x; x++)
			{
				var rightCoord = BubbleNeighbourLogicService.GetNeighborCoordinate(oldCoordinate, Direction.E);
				oldCoordinate = rightCoord;
				bubble = _contexts.game.CreateRandomBubble(oldCoordinate);
				bubble.isMapInitBubble = true;
			}
		}
		
		
	}		
}