using Entitas;
using System.Collections.Generic;

public class ParticlesSystem : ReactiveSystem<GameEntity> {
    private Contexts _contexts;
    private readonly IParticlesService particlesService;

    public ParticlesSystem (Contexts contexts, IParticlesService particlesService) : base(contexts.game)
    {
	    _contexts = contexts;
	    this.particlesService = particlesService;
    }

	protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) {
		return context.CreateCollector(GameMatcher.MergeFlag);
	}

	protected override bool Filter(GameEntity entity)
	{
		return true;
	}

	protected override void Execute(List<GameEntity> entities) {
		foreach (var e in entities)
		{
			var color = _contexts.config.pieceColorsConfig.value.GetColorWithNumber(e.value.Number);
			particlesService.PopParticle(e.position.Value, color);
		}
	}
}