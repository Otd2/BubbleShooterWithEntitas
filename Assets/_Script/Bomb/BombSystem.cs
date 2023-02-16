using Entitas;
using System.Collections.Generic;

public class BombSystem : ReactiveSystem<GameEntity> {
    private Contexts _contexts;

	public BombSystem (Contexts contexts) : base(contexts.game) {
        _contexts = contexts;
	}

	protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) {
		return context.CreateCollector(GameMatcher.AllOf(GameMatcher.NewCreated));
	}

	protected override bool Filter(GameEntity entity)
	{
		return entity.value.Number >= 4096;
	}

	protected override void Execute(List<GameEntity> entities)
	{
		var neighbours = BubbleNeighbourLogicService.GetNeighbourCoordinates(entities[0].coordinate.value);
		entities[0].isBomb = true;
		foreach (var neighbourCoord in neighbours)
		{
			var neighbourEntity = _contexts.game.GetEntityWithCoordinate(neighbourCoord);
			if (neighbourEntity is { isBubble: true })
			{
				neighbourEntity.isFall = true;
				neighbourEntity.RemoveCoordinate();
			}
		}

		_contexts.game.isWaitForNextShoot = false;
	}
}