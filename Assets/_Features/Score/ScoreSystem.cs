using Entitas;
using System.Collections.Generic;
using UnityEngine;

public class ScoreSystem : ReactiveSystem<GameEntity>, IInitializeSystem {
    private Contexts _contexts;
    private readonly IScoreUIPoolService particlesService;

    public ScoreSystem (Contexts contexts, IScoreUIPoolService particlesService) : base(contexts.game)
    {
	    _contexts = contexts;
	    this.particlesService = particlesService;
    }

	protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) {
		return context.CreateCollector(GameMatcher.AllOf(GameMatcher.ScoreGained, GameMatcher.Position));
	}

	protected override bool Filter(GameEntity entity)
	{
		return true;
	}

	protected override void Execute(List<GameEntity> entities)
	{
		var oldScore = _contexts.game.totalScore.Score;
		foreach (var entity in entities)
		{
			_contexts.game.ReplaceTotalScore(oldScore + entity.scoreGained.Score);	
			particlesService.ScoreEffect(entity.position.Value, entity.scoreGained.Score);
			entity.Destroy();
		}
	}

	public void Initialize()
	{
		_contexts.game.SetTotalScore(0);
		_contexts.game.totalScoreEntity.AddAsset("ScoreView");
		_contexts.game.totalScoreEntity.AddPosition(Vector2.zero);
	}
}