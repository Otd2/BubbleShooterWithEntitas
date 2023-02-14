using UnityEngine;

[CreateAssetMenu(menuName = "Bubble Pops/Bubble Colors Config")]
public class BubbleColorConfigSO : ScriptableObject, IPieceColorsConfig
{
    [SerializeField] public Color[] _colors;
    public Color[] Colors => _colors;
}