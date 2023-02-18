using Entitas;
using System.Collections.Generic;
using UnityEngine;

public class FallItemDestroyerSystem : ReactiveSystem<GameEntity> {
    private Contexts _contexts;
    private readonly IParticlesService particleService;

    public FallItemDestroyerSystem (Contexts contexts, IParticlesService particleService) : base(contexts.game)
    {
	    _contexts = contexts;
	    this.particleService = particleService;
    }

	protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) {
		return context.CreateCollector(GameMatcher.AllOf(GameMatcher.Fall, GameMatcher.Position).NoneOf(GameMatcher.Destroyed));
	}

	protected override bool Filter(GameEntity entity)
	{
		return entity.position.Value.y < -5.5f;
	}

	protected override void Execute(List<GameEntity> entities) {
		foreach (var e in entities)
		{
			var color = _contexts.config.pieceColorsConfig.value.GetColorWithNumber(e.value.Number);
			particleService.PopParticle(e.position.Value, color);
			
			_contexts.game.AddScore(e.value.Number,e.position.Value);
			
			e.isDestroyed = true;
		}
	}
	

}