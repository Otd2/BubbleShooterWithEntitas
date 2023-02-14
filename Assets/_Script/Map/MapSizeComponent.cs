using Entitas;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;

[Game, Unique]
public class MapSizeComponent : IComponent
{
    public Vector2Int size;
}