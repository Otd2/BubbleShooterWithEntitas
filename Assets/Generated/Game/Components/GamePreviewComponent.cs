//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentContextApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameContext {

    public GameEntity previewEntity { get { return GetGroup(GameMatcher.Preview).GetSingleEntity(); } }

    public bool isPreview {
        get { return previewEntity != null; }
        set {
            var entity = previewEntity;
            if (value != (entity != null)) {
                if (value) {
                    CreateEntity().isPreview = true;
                } else {
                    entity.Destroy();
                }
            }
        }
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    static readonly PreviewComponent previewComponent = new PreviewComponent();

    public bool isPreview {
        get { return HasComponent(GameComponentsLookup.Preview); }
        set {
            if (value != isPreview) {
                var index = GameComponentsLookup.Preview;
                if (value) {
                    var componentPool = GetComponentPool(index);
                    var component = componentPool.Count > 0
                            ? componentPool.Pop()
                            : previewComponent;

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

    static Entitas.IMatcher<GameEntity> _matcherPreview;

    public static Entitas.IMatcher<GameEntity> Preview {
        get {
            if (_matcherPreview == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.Preview);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherPreview = matcher;
            }

            return _matcherPreview;
        }
    }
}
