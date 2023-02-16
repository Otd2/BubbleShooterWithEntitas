//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.EventSystemGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed class BounceEventSystem : Entitas.ReactiveSystem<GameEntity> {

    readonly System.Collections.Generic.List<IBounceListener> _listenerBuffer;

    public BounceEventSystem(Contexts contexts) : base(contexts.game) {
        _listenerBuffer = new System.Collections.Generic.List<IBounceListener>();
    }

    protected override Entitas.ICollector<GameEntity> GetTrigger(Entitas.IContext<GameEntity> context) {
        return Entitas.CollectorContextExtension.CreateCollector(
            context, Entitas.TriggerOnEventMatcherExtension.Added(GameMatcher.Bounce)
        );
    }

    protected override bool Filter(GameEntity entity) {
        return entity.hasBounce && entity.hasBounceListener;
    }

    protected override void Execute(System.Collections.Generic.List<GameEntity> entities) {
        foreach (var e in entities) {
            var component = e.bounce;
            _listenerBuffer.Clear();
            _listenerBuffer.AddRange(e.bounceListener.value);
            foreach (var listener in _listenerBuffer) {
                listener.OnBounce(e, component.from);
            }
        }
    }
}
