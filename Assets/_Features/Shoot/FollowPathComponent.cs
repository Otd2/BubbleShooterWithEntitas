using Entitas;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;

[Game, Event(EventTarget.Self)]
public class FollowPathComponent : IComponent
{
    public Vector3[] path;
    public float speed;
}