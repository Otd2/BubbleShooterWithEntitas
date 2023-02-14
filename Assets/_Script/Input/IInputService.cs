using UnityEngine;

namespace _Script.Input
{
    public interface IInputService {
        Vector2 InputPosition {get;}
        bool IsHolding {get;}
        bool IsReleased {get;}
    }
}