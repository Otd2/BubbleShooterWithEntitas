//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    static readonly TopLayerComponent topLayerComponent = new TopLayerComponent();

    public bool isTopLayer {
        get { return HasComponent(GameComponentsLookup.TopLayer); }
        set {
            if (value != isTopLayer) {
                var index = GameComponentsLookup.TopLayer;
                if (value) {
                    var componentPool = GetComponentPool(index);
                    var component = componentPool.Count > 0
                            ? componentPool.Pop()
                            : topLayerComponent;

                    AddComponent(index, component);
                } else {
                    RemoveComponent(index);
                }
            }
        }
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentMatcherApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed partial class GameMatcher {

    static Entitas.IMatcher<GameEntity> _matcherTopLayer;

    public static Entitas.IMatcher<GameEntity> TopLayer {
        get {
            if (_matcherTopLayer == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.TopLayer);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherTopLayer = matcher;
            }

            return _matcherTopLayer;
        }
    }
}