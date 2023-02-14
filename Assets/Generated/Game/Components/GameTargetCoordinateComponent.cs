//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentContextApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameContext {

    public GameEntity targetCoordinateEntity { get { return GetGroup(GameMatcher.TargetCoordinate).GetSingleEntity(); } }
    public TargetCoordinateComponent targetCoordinate { get { return targetCoordinateEntity.targetCoordinate; } }
    public bool hasTargetCoordinate { get { return targetCoordinateEntity != null; } }

    public GameEntity SetTargetCoordinate(UnityEngine.Vector2Int newValue) {
        if (hasTargetCoordinate) {
            throw new Entitas.EntitasException("Could not set TargetCoordinate!\n" + this + " already has an entity with TargetCoordinateComponent!",
                "You should check if the context already has a targetCoordinateEntity before setting it or use context.ReplaceTargetCoordinate().");
        }
        var entity = CreateEntity();
        entity.AddTargetCoordinate(newValue);
        return entity;
    }

    public void ReplaceTargetCoordinate(UnityEngine.Vector2Int newValue) {
        var entity = targetCoordinateEntity;
        if (entity == null) {
            entity = SetTargetCoordinate(newValue);
        } else {
            entity.ReplaceTargetCoordinate(newValue);
        }
    }

    public void RemoveTargetCoordinate() {
        targetCoordinateEntity.Destroy();
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

    public TargetCoordinateComponent targetCoordinate { get { return (TargetCoordinateComponent)GetComponent(GameComponentsLookup.TargetCoordinate); } }
    public bool hasTargetCoordinate { get { return HasComponent(GameComponentsLookup.TargetCoordinate); } }

    public void AddTargetCoordinate(UnityEngine.Vector2Int newValue) {
        var index = GameComponentsLookup.TargetCoordinate;
        var component = (TargetCoordinateComponent)CreateComponent(index, typeof(TargetCoordinateComponent));
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceTargetCoordinate(UnityEngine.Vector2Int newValue) {
        var index = GameComponentsLookup.TargetCoordinate;
        var component = (TargetCoordinateComponent)CreateComponent(index, typeof(TargetCoordinateComponent));
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveTargetCoordinate() {
        RemoveComponent(GameComponentsLookup.TargetCoordinate);
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

    static Entitas.IMatcher<GameEntity> _matcherTargetCoordinate;

    public static Entitas.IMatcher<GameEntity> TargetCoordinate {
        get {
            if (_matcherTargetCoordinate == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.TargetCoordinate);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherTargetCoordinate = matcher;
            }

            return _matcherTargetCoordinate;
        }
    }
}
