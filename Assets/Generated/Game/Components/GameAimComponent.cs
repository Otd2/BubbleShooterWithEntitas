//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentContextApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameContext {

    public GameEntity aimEntity { get { return GetGroup(GameMatcher.Aim).GetSingleEntity(); } }
    public AimComponent aim { get { return aimEntity.aim; } }
    public bool hasAim { get { return aimEntity != null; } }

    public GameEntity SetAim(UnityEngine.Vector2 newOrigin, UnityEngine.Vector2 newNormalizedDir) {
        if (hasAim) {
            throw new Entitas.EntitasException("Could not set Aim!\n" + this + " already has an entity with AimComponent!",
                "You should check if the context already has a aimEntity before setting it or use context.ReplaceAim().");
        }
        var entity = CreateEntity();
        entity.AddAim(newOrigin, newNormalizedDir);
        return entity;
    }

    public void ReplaceAim(UnityEngine.Vector2 newOrigin, UnityEngine.Vector2 newNormalizedDir) {
        var entity = aimEntity;
        if (entity == null) {
            entity = SetAim(newOrigin, newNormalizedDir);
        } else {
            entity.ReplaceAim(newOrigin, newNormalizedDir);
        }
    }

    public void RemoveAim() {
        aimEntity.Destroy();
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

    public AimComponent aim { get { return (AimComponent)GetComponent(GameComponentsLookup.Aim); } }
    public bool hasAim { get { return HasComponent(GameComponentsLookup.Aim); } }

    public void AddAim(UnityEngine.Vector2 newOrigin, UnityEngine.Vector2 newNormalizedDir) {
        var index = GameComponentsLookup.Aim;
        var component = (AimComponent)CreateComponent(index, typeof(AimComponent));
        component.origin = newOrigin;
        component.normalizedDir = newNormalizedDir;
        AddComponent(index, component);
    }

    public void ReplaceAim(UnityEngine.Vector2 newOrigin, UnityEngine.Vector2 newNormalizedDir) {
        var index = GameComponentsLookup.Aim;
        var component = (AimComponent)CreateComponent(index, typeof(AimComponent));
        component.origin = newOrigin;
        component.normalizedDir = newNormalizedDir;
        ReplaceComponent(index, component);
    }

    public void RemoveAim() {
        RemoveComponent(GameComponentsLookup.Aim);
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

    static Entitas.IMatcher<GameEntity> _matcherAim;

    public static Entitas.IMatcher<GameEntity> Aim {
        get {
            if (_matcherAim == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.Aim);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherAim = matcher;
            }

            return _matcherAim;
        }
    }
}
