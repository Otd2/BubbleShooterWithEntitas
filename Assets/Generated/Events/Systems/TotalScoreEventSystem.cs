//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.EventSystemGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed class TotalScoreEventSystem : Entitas.ReactiveSystem<GameEntity> {

    readonly System.Collections.Generic.List<ITotalScoreListener> _listenerBuffer;

    public TotalScoreEventSystem(Contexts contexts) : base(contexts.game) {
        _listenerBuffer = new System.Collections.Generic.List<ITotalScoreListener>();
    }

    protected override Entitas.ICollector<GameEntity> GetTrigger(Entitas.IContext<GameEntity> context) {
        return Entitas.CollectorContextExtension.CreateCollector(
            context, Entitas.TriggerOnEventMatcherExtension.Added(GameMatcher.TotalScore)
        );
    }

    protected override bool Filter(GameEntity entity) {
        return entity.hasTotalScore && entity.hasTotalScoreListener;
    }

    protected override void Execute(System.Collections.Generic.List<GameEntity> entities) {
        foreach (var e in entities) {
            var component = e.totalScore;
            _listenerBuffer.Clear();
            _listenerBuffer.AddRange(e.totalScoreListener.value);
            foreach (var listener in _listenerBuffer) {
                listener.OnTotalScore(e, component.Score);
            }
        }
    }
}