using Entitas;
using System.Collections.Generic;

public class BounceTriggerSystem : ReactiveSystem<GameEntity> {
    private Contexts _contexts;

	public BounceTriggerSystem (Contexts contexts) : base(contexts.game) {
        _contexts = contexts;
	}

	protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) {
		return context.CreateCollector(GameMatcher.NewCreated);
	}

	protected override bool Filter(GameEntity entity)
	{
		return true;
	}

	protected override void Execute(List<GameEntity> entities)
	{
		var neighbours = BubbleNeighbourLogicService.GetNeighbourCoordinates(entities[0].coordinate.value);
		foreach (var neighbour in neighbours)
		{
			var neighbourEntity = _contexts.game.GetEntityWithCoordinate(neighbour);
			if(neighbourEntity is { isMergeFlag: false })
				neighbourEntity.AddBounce(entities[0].position.Value);
		}
	}
}