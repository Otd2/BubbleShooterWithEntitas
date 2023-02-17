//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.EventSystemGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed class ShootingBubbleEventSystem : Entitas.ReactiveSystem<GameEntity> {

    readonly System.Collections.Generic.List<IShootingBubbleListener> _listenerBuffer;

    public ShootingBubbleEventSystem(Contexts contexts) : base(contexts.game) {
        _listenerBuffer = new System.Collections.Generic.List<IShootingBubbleListener>();
    }

    protected override Entitas.ICollector<GameEntity> GetTrigger(Entitas.IContext<GameEntity> context) {
        return Entitas.CollectorContextExtension.CreateCollector(
            context, Entitas.TriggerOnEventMatcherExtension.Added(GameMatcher.ShootingBubble)
        );
    }

    protected override bool Filter(GameEntity entity) {
        return entity.hasShootingBubble && entity.hasShootingBubbleListener;
    }

    protected override void Execute(System.Collections.Generic.List<GameEntity> entities) {
        foreach (var e in entities) {
            var component = e.shootingBubble;
            _listenerBuffer.Clear();
            _listenerBuffer.AddRange(e.shootingBubbleListener.value);
            foreach (var listener in _listenerBuffer) {
                listener.OnShootingBubble(e, component.shootIndex);
            }
        }
    }
}