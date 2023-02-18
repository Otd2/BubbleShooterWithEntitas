using Entitas;
using UnityEngine;

[Game]
public class MergeComponent : IComponent
{
    public Vector2Int target;
    public int targetNumber;
}