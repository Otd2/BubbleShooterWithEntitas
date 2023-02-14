using Entitas.CodeGeneration.Attributes;
using UnityEngine;

[Config, Unique, ComponentName("GameConfig")]
public interface IGameConfig
{
    Vector2Int BoardSize { get; }
    float BubbleShootSpeed { get; }
    float MergeAndPopIntervalTime { get; }
    int MaxAllowedBounce  { get; }
    
    int DeepestPointOnScreen  { get; }
    
    int ShallowestPointOnScreen  { get; }

}