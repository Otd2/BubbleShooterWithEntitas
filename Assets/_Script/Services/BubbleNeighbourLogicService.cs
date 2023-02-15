using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class BubbleNeighbourLogicService
{
    private static float HorizontalSpacing;
    private static float VerticalSpacing;
    private static float CellSize = 0.5f;
    
    public static readonly Dictionary<Direction, Vector2Int> DirectionVectors 
        = new Dictionary<Direction, Vector2Int>()
    {
        { Direction.E, new Vector2Int(2, 0) },
        { Direction.NE, new Vector2Int(1, -1) },
        { Direction.NW, new Vector2Int(-1, -1) },
        { Direction.W, new Vector2Int(-2, 0) },
        { Direction.SW, new Vector2Int(-1, 1) },
        { Direction.SE, new Vector2Int(1, 1) },
    };

    public static void Init()
    {
        CellSize = 1 / Mathf.Sqrt(3f);
        HorizontalSpacing = Mathf.Sqrt(3f) * CellSize;
        VerticalSpacing = (3f / 2f) * CellSize;
    }

    public static Vector2Int GetNeighborCoordinate(Vector2Int coord, Direction direction)
    {
        return coord + DirectionVectors[direction];
    }

    public static Vector3 FromCoordToWorldPos(Vector2Int coord)
    {
        return new Vector3(HorizontalSpacing * (coord.x / 2f),
            -coord.y * VerticalSpacing) + new Vector3(-2.75f, 6,0);
    }

    public static List<Vector2Int> GetNeighbourCoordinates(Vector2Int center)
    {
        List<Vector2Int> neighbourCoords = new List<Vector2Int>();
        foreach (var directionVector in DirectionVectors.Values)
        {
            neighbourCoords.Add(directionVector + center);
        }

        return neighbourCoords;
    }

}

public enum Direction
{
    E = 0,
    NE = 1,
    NW = 2,
    W = 3,
    SW = 4,
    SE = 5
}
