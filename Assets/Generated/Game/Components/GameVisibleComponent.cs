//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public VisibleComponent visible { get { return (VisibleComponent)GetComponent(GameComponentsLookup.Visible); } }
    public bool hasVisible { get { return HasComponent(GameComponentsLookup.Visible); } }

    public void AddVisible(bool newIsVisible) {
        var index = GameComponentsLookup.Visible;
        var component = (VisibleComponent)CreateComponent(index, typeof(VisibleComponent));
        component.isVisible = newIsVisible;
        AddComponent(index, component);
    }

    public void ReplaceVisible(bool newIsVisible) {
        var index = GameComponentsLookup.Visible;
        var component = (VisibleComponent)CreateComponent(index, typeof(VisibleComponent));
        component.isVisible = newIsVisible;
        ReplaceComponent(index, component);
    }

    public void RemoveVisible() {
        RemoveComponent(GameComponentsLookup.Visible);
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

    static Entitas.IMatcher<GameEntity> _matcherVisible;

    public static Entitas.IMatcher<GameEntity> Visible {
        get {
            if (_matcherVisible == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.Visible);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherVisible = matcher;
            }

            return _matcherVisible;
        }
    }
}
