using Entitas;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;

[Game, Unique]
public class AimComponent : IComponent
{
    public Vector2 origin;
    public Vector2 normalizedDir;
}