using Entitas;
using System.Collections.Generic;

public class PreviewColorSystem : ReactiveSystem<GameEntity> {
    private Contexts _contexts;

	public PreviewColorSystem (Contexts contexts) : base(contexts.game) {
        _contexts = contexts;
	}

	protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) {
		return context.CreateCollector(GameMatcher.ShootingBubble);
	}

	protected override bool Filter(GameEntity entity)
	{
		return entity.shootingBubble.shootIndex == 0;
	}

	protected override void Execute(List<GameEntity> entities)
	{
		_contexts.game.previewEntity.ReplaceValue(entities.SingleEntity().value.Number);
	}
}