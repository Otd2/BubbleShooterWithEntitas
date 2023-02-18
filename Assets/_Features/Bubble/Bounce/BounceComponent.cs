using Entitas;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;

[Game, Event(EventTarget.Self)]
public class BounceComponent : IComponent
{
    public Vector2 from;
}