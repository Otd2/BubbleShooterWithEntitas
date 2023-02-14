using UnityEngine;

public class Coordinate
{
    private Vector2Int coord;

    public Vector2Int Value => coord;

    public Coordinate(Vector2Int coord)
    {
        this.coord = coord;
    }
}