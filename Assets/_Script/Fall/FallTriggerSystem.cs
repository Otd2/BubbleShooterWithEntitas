using Entitas;
using System.Collections.Generic;

public class FallTriggerSystem : ReactiveSystem<GameEntity> {
    private Contexts _contexts;

	public FallTriggerSystem (Contexts contexts) : base(contexts.game) {
        _contexts = contexts;
	}

	protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) {
		return context.CreateCollector(GameMatcher.VisitedNode.AddedOrRemoved());
	}

	protected override bool Filter(GameEntity entity)
	{
		return !entity.isVisitedNode;
	}

	protected override void Execute(List<GameEntity> entities) {
		foreach (var e in entities)
		{
			e.isFall = true;
		}
	}
}