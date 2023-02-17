using Entitas;
using System.Collections.Generic;
using UnityEngine;

public class ShootBubbleCreatorSystem : ReactiveSystem<GameEntity>, IInitializeSystem {
    private Contexts _contexts;
    private readonly Transform[] shootOrigins;

    public ShootBubbleCreatorSystem (Contexts contexts, Transform[] shootOrigins) : base(contexts.game)
    {
	    _contexts = contexts;
	    this.shootOrigins = shootOrigins;
    }

	protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) {
		return context.CreateCollector(GameMatcher.Shoot);
	}

	protected override bool Filter(GameEntity entity) {
        return true;
	}

	protected override void Execute(List<GameEntity> entities)
	{
		entities[0].RemoveShootingBubble();

		var shootBubble = _contexts.game.GetEntities(GameMatcher.ShootingBubble);
		shootBubble[0].ReplaceShootingBubble(0);
		CreateShootBubble(1);
	}

	public void Initialize()
	{
		for (var index = 0; index < shootOrigins.Length; index++)
		{
			CreateShootBubble(index);
		}
	}

	private void CreateShootBubble(int index)
	{
		var shootOrigin = shootOrigins[index];
		_contexts.game.CreateBubbleForShoot(index, shootOrigin.position);
	}
}