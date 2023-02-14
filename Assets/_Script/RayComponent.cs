using System.Collections.Generic;
using Entitas;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;

[Game, Unique]
public class RayComponent : IComponent
{
    public List<Vector2> hitPoints;
}