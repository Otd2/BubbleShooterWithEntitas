using Entitas;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;

[Game]
public class CoordinateComponent : IComponent
{
    [PrimaryEntityIndex] public Vector2Int value;
}