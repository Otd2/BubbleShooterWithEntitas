using UnityEngine;

public class TopLayerView : View
{
    public Vector2Int GetCoordinate()
    {
        return _linkedEntity.coordinate.value;
    }
}