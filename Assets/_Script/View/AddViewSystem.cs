using System.Collections.Generic;
using Entitas;
using UnityEngine;
using static GameMatcher;

public sealed class AddViewSystem : ReactiveSystem<GameEntity>
{
    readonly Transform _parent;

    public AddViewSystem(Contexts contexts) : base(contexts.game)
    {
        _parent = new GameObject("Map").transform;
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) =>
        context.CreateCollector(GameMatcher.Asset);

    protected override bool Filter(GameEntity entity) => entity.hasAsset && !entity.hasView;

    protected override void Execute(List<GameEntity> entities)
    {
        Debug.Log(entities.Count);
        foreach (var e in entities)
            e.AddView(InstantiateView(e));
    }

    protected IView InstantiateView(GameEntity entity)
    {
        var prefab = Resources.Load<GameObject>(entity.asset.Value);
        var view = Object.Instantiate(prefab, _parent).GetComponent<IView>();
        view.Link(entity);
        return view;
    }
}