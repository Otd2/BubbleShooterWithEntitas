﻿using Entitas;
using System.Collections.Generic;
using UnityEngine;

public class MergeCompletedSystem : ReactiveSystem<GameEntity> {
    private Contexts _contexts;

	public MergeCompletedSystem (Contexts contexts) : base(contexts.game) {
        _contexts = contexts;
	}

	protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) {
		return context.CreateCollector(GameMatcher.AllOf(GameMatcher.TimerComplete, GameMatcher.Merge));
	}

	protected override bool Filter(GameEntity entity)
	{
		return entity.isTimerComplete;
	}

	protected override void Execute(List<GameEntity> entities)
	{
		var targets = _contexts.game.GetEntities(GameMatcher.AllOf(GameMatcher.MergeFlag).NoneOf(GameMatcher.Destroyed));
		Debug.Log("MERGE COMPLETED");
		foreach (var e in targets)
		{
			e.isDestroyed = true;
			e.RemoveCoordinate();
		}
	
		
		var newBubble = _contexts.game.CreateBoardBubble(entities.SingleEntity().merge.targetNumber,
			entities.SingleEntity().merge.target);
		newBubble.isNewCreated = true;
		
		entities[0].Destroy();
	}
}