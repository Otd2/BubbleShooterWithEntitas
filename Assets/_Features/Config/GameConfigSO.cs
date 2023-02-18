using UnityEngine;

[CreateAssetMenu(menuName = "Bubble Pops/Game Config")]
public class GameConfigSO : ScriptableObject, IGameConfig
{
    [SerializeField] private Vector2Int boardSize;
    [SerializeField] private float mergeAndPopIntervalTime;
    [SerializeField] private int maxAllowedBounce;
    [SerializeField] private Vector3 startPosition;
    [SerializeField] private float shootSpeed;

    public Vector2Int BoardSize => boardSize;
    public float MergeAndPopIntervalTime => mergeAndPopIntervalTime;
    public int MaxAllowedBounce => maxAllowedBounce;
    public Vector3 StartPosition => startPosition;
    public float ShootSpeed => shootSpeed;
}