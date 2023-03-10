//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentContextApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class InputContext {

    public InputEntity touchPositionEntity { get { return GetGroup(InputMatcher.TouchPosition).GetSingleEntity(); } }
    public TouchPositionComponent touchPosition { get { return touchPositionEntity.touchPosition; } }
    public bool hasTouchPosition { get { return touchPositionEntity != null; } }

    public InputEntity SetTouchPosition(UnityEngine.Vector2 newTouchPos) {
        if (hasTouchPosition) {
            throw new Entitas.EntitasException("Could not set TouchPosition!\n" + this + " already has an entity with TouchPositionComponent!",
                "You should check if the context already has a touchPositionEntity before setting it or use context.ReplaceTouchPosition().");
        }
        var entity = CreateEntity();
        entity.AddTouchPosition(newTouchPos);
        return entity;
    }

    public void ReplaceTouchPosition(UnityEngine.Vector2 newTouchPos) {
        var entity = touchPositionEntity;
        if (entity == null) {
            entity = SetTouchPosition(newTouchPos);
        } else {
            entity.ReplaceTouchPosition(newTouchPos);
        }
    }

    public void RemoveTouchPosition() {
        touchPositionEntity.Destroy();
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
public partial class InputEntity {

    public TouchPositionComponent touchPosition { get { return (TouchPositionComponent)GetComponent(InputComponentsLookup.TouchPosition); } }
    public bool hasTouchPosition { get { return HasComponent(InputComponentsLookup.TouchPosition); } }

    public void AddTouchPosition(UnityEngine.Vector2 newTouchPos) {
        var index = InputComponentsLookup.TouchPosition;
        var component = (TouchPositionComponent)CreateComponent(index, typeof(TouchPositionComponent));
        component.touchPos = newTouchPos;
        AddComponent(index, component);
    }

    public void ReplaceTouchPosition(UnityEngine.Vector2 newTouchPos) {
        var index = InputComponentsLookup.TouchPosition;
        var component = (TouchPositionComponent)CreateComponent(index, typeof(TouchPositionComponent));
        component.touchPos = newTouchPos;
        ReplaceComponent(index, component);
    }

    public void RemoveTouchPosition() {
        RemoveComponent(InputComponentsLookup.TouchPosition);
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
public sealed partial class InputMatcher {

    static Entitas.IMatcher<InputEntity> _matcherTouchPosition;

    public static Entitas.IMatcher<InputEntity> TouchPosition {
        get {
            if (_matcherTouchPosition == null) {
                var matcher = (Entitas.Matcher<InputEntity>)Entitas.Matcher<InputEntity>.AllOf(InputComponentsLookup.TouchPosition);
                matcher.componentNames = InputComponentsLookup.componentNames;
                _matcherTouchPosition = matcher;
            }

            return _matcherTouchPosition;
        }
    }
}
