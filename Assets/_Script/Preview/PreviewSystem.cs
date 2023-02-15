using Entitas;
using System.Collections.Generic;
using UnityEngine;

public class PreviewSystem : ReactiveSystem<GameEntity>, IInitializeSystem {
    private Contexts _contexts;
    private GameEntity previewEntity;

	public PreviewSystem (Contexts contexts) : base(contexts.game) {
        _contexts = contexts;
	}

	protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) {
		return context.CreateCollector(GameMatcher.TargetCoordinate);
	}

	protected override bool Filter(GameEntity entity)
	{
		return true;
	}

	protected override void Execute(List<GameEntity> entities) {
		var coord = entities.SingleEntity().targetCoordinate.value;
		previewEntity.ReplacePosition(BubbleNeighbourLogicService.FromCoordToWorldPos(coord));
	}

	public void Initialize()
	{
		previewEntity = Contexts.sharedInstance.game.CreatePreviewBubble();
	}
}