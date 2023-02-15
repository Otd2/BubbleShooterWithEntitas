using Entitas;
using System.Collections.Generic;
using UnityEngine;

public class AimSystem : ReactiveSystem<InputEntity> {
    
	private Contexts _contexts;
	private readonly Vector3 origin;

	public AimSystem (Contexts contexts, Vector3 origin) : base(contexts.input) {
		_contexts = contexts;
		this.origin = origin;
	}

	protected override ICollector<InputEntity> GetTrigger(IContext<InputEntity> context) {
		return context.CreateCollector(InputMatcher.TouchPosition);
	}

	protected override bool Filter(InputEntity entity)
	{
		return true;
	}

	protected override void Execute(List<InputEntity> entities)
	{
		var inputEntity = entities[0];
		var touchPointOnWorld = Camera.main
			.ScreenToWorldPoint(inputEntity.touchPosition.touchPos);
		touchPointOnWorld = new Vector3(touchPointOnWorld.x, touchPointOnWorld.y, origin.z);
		var normalizedDir = (touchPointOnWorld - origin).normalized;
		_contexts.game.ReplaceAim(origin, normalizedDir);
		//Debug.DrawRay(origin, normalizedDir*1000);
	}
}

/*using Entitas;
using UnityEngine;

public class AimSystem : IExecuteSystem  {
	private Contexts _contexts;
	private readonly Vector3 origin;

	public AimSystem(Contexts contexts, Vector3 origin)
	{
		_contexts = contexts;
		this.origin = origin;
	}

	public void Execute()
	{
		var touchPointOnWorld = Camera.main.ScreenToWorldPoint(new Vector3(_contexts.input.touchPosition.touchPos.x, _contexts.input.touchPosition.touchPos.y));
		touchPointOnWorld = new Vector3(touchPointOnWorld.x, touchPointOnWorld.y, origin.z);
		var normalizedDir = (touchPointOnWorld - origin).normalized;
		_contexts.game.ReplaceAim(origin, normalizedDir);
		//Debug.DrawRay(origin, normalizedDir*1000);
	}
}*/