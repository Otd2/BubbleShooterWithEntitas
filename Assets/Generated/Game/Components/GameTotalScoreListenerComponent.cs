//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public TotalScoreListenerComponent totalScoreListener { get { return (TotalScoreListenerComponent)GetComponent(GameComponentsLookup.TotalScoreListener); } }
    public bool hasTotalScoreListener { get { return HasComponent(GameComponentsLookup.TotalScoreListener); } }

    public void AddTotalScoreListener(System.Collections.Generic.List<ITotalScoreListener> newValue) {
        var index = GameComponentsLookup.TotalScoreListener;
        var component = (TotalScoreListenerComponent)CreateComponent(index, typeof(TotalScoreListenerComponent));
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceTotalScoreListener(System.Collections.Generic.List<ITotalScoreListener> newValue) {
        var index = GameComponentsLookup.TotalScoreListener;
        var component = (TotalScoreListenerComponent)CreateComponent(index, typeof(TotalScoreListenerComponent));
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveTotalScoreListener() {
        RemoveComponent(GameComponentsLookup.TotalScoreListener);
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

    static Entitas.IMatcher<GameEntity> _matcherTotalScoreListener;

    public static Entitas.IMatcher<GameEntity> TotalScoreListener {
        get {
            if (_matcherTotalScoreListener == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.TotalScoreListener);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherTotalScoreListener = matcher;
            }

            return _matcherTotalScoreListener;
        }
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.EventEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public void AddTotalScoreListener(ITotalScoreListener value) {
        var listeners = hasTotalScoreListener
            ? totalScoreListener.value
            : new System.Collections.Generic.List<ITotalScoreListener>();
        listeners.Add(value);
        ReplaceTotalScoreListener(listeners);
    }

    public void RemoveTotalScoreListener(ITotalScoreListener value, bool removeComponentWhenEmpty = true) {
        var listeners = totalScoreListener.value;
        listeners.Remove(value);
        if (removeComponentWhenEmpty && listeners.Count == 0) {
            RemoveTotalScoreListener();
        } else {
            ReplaceTotalScoreListener(listeners);
        }
    }
}