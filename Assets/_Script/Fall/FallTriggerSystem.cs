using Entitas;
using System.Collections.Generic;

public class FallTriggerSystem : ReactiveSystem<GameEntity> {
    private Contexts _contexts;

	public FallTriggerSystem (Contexts contexts) : base(contexts.game) {
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
		var bubbles = _contexts.game.GetEntities(GameMatcher.Bubble);
		foreach (var e in bubbles)
		{
			if(!e.isVisitedNode)
				e.isFall = true;
		}
	}
}