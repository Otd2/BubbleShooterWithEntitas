using Entitas;
using System.Collections.Generic;

public class PathCompletedSystem : ReactiveSystem<GameEntity> {
    private Contexts _contexts;

	public PathCompletedSystem (Contexts contexts) : base(contexts.game) {
        _contexts = contexts;
	}

	protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) {
		return context.CreateCollector(GameMatcher.PathCompleted);
	}

	protected override bool Filter(GameEntity entity)
	{
		return true;
	}

	protected override void Execute(List<GameEntity> entities)
	{
		var e = entities.SingleEntity();
		
		e.isDestroyed = true;
		var newBubble = _contexts.game.CreateBoardBubble
			(e.value.Number, _contexts.game.targetCoordinate.value);
		newBubble.isNewCreated = true;
		newBubble.isFirstShotBubble = true;
	}
}