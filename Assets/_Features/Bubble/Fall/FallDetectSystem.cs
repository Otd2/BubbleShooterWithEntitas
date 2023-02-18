using Entitas;
using System.Collections.Generic;

public class FallDetectSystem : ReactiveSystem<GameEntity> {
    private Contexts _contexts;

	public FallDetectSystem (Contexts contexts) : base(contexts.game) {
        _contexts = contexts;
	}

	protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) {
		return context.CreateCollector(GameMatcher.AllOf(GameMatcher.NewCreated).NoneOf(GameMatcher.ShootingBubble));
	}

	protected override bool Filter(GameEntity entity)
	{
		return true;
	}

	protected override void Execute(List<GameEntity> entities)
	{
		//Get all bubbles
		var bubbles = _contexts.game.GetEntities
			(GameMatcher.AllOf(GameMatcher.Bubble, GameMatcher.Coordinate).NoneOf(GameMatcher.Fall));
		_contexts.ClearVisitedStack();
		
		//Apply Depth First Search Travers from all bubbles-at-top-layer to flag connected bubbles 
		foreach (var e in bubbles)
		{
			if(e.coordinate.value.y == 0)
				_contexts.DFS(e);
		}
	}
}