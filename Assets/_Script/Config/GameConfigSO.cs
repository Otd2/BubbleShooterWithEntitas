using UnityEngine;

[CreateAssetMenu(menuName = "Bubble Pops/Game Config")]
public class GameConfigSO : ScriptableObject, IGameConfig
{
    [SerializeField] private Vector2Int boardSize;
    [SerializeField] private float bubbleShootSpeed;
    [SerializeField] private float mergeAndPopIntervalTime;
    [SerializeField] private int maxAllowedBounce;
    [SerializeField] private int deepestPointOnScreen;
    [SerializeField] private int shallowestPointOnScreen;

    public Vector2Int BoardSize => boardSize;
    public float BubbleShootSpeed => bubbleShootSpeed;
    public float MergeAndPopIntervalTime => mergeAndPopIntervalTime;
    public int MaxAllowedBounce => maxAllowedBounce;
    public int DeepestPointOnScreen => deepestPointOnScreen;
    public int ShallowestPointOnScreen => shallowestPointOnScreen;
}