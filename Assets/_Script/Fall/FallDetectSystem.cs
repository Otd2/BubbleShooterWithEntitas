using Entitas;
using UnityEngine;
/*
public class FallDetectSystem : IExecuteSystem  {
	private Contexts _contexts;

    public FallDetectSystem(Contexts contexts) {
    	_contexts = contexts;
    }

	public void Execute() {
		var bubbles = _contexts.game.GetEntities
			(GameMatcher.AllOf(GameMatcher.Bubble, GameMatcher.Coordinate));
		_contexts.ClearVisitedStack();
		Debug.Log("TOP ENTITES COUNT = " + bubbles.Length);
		foreach (var e in bubbles)
		{
			if(e.coordinate.value.y == 0)
				_contexts.DFS(e);
		}
	}
}*/

using Entitas;
using System.Collections.Generic;
using UnityEngine;

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
		var bubbles = _contexts.game.GetEntities
			(GameMatcher.AllOf(GameMatcher.Bubble, GameMatcher.Coordinate).NoneOf(GameMatcher.Fall));
		_contexts.ClearVisitedStack();
		foreach (var e in bubbles)
		{
			if(e.coordinate.value.y == 0)
				_contexts.DFS(e);
		}
	}
}