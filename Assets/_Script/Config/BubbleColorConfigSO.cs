using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Bubble Pops/Bubble Colors Config")]
public class BubbleColorConfigSO : ScriptableObject, IPieceColorsConfig
{
    [SerializeField] public Color[] _colors;
    public Color[] Colors => _colors;
    public Color GetColorWithNumber(int number)
    {
        return _colors[(int)Math.Log(number,2)];
    }
    
}