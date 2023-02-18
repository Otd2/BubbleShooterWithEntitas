using Entitas.CodeGeneration.Attributes;
using UnityEngine;

[Config, Unique, ComponentName("GameConfig")]
public interface IGameConfig
{
    Vector2Int BoardSize { get; }
    float MergeAndPopIntervalTime { get; }
    int MaxAllowedBounce  { get; }
    Vector3 StartPosition  { get; }
    float ShootSpeed { get; }

}