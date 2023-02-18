using Entitas;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;

[Game, Unique]
public class TargetCoordinateComponent : IComponent
{
    public Vector2Int value;
}