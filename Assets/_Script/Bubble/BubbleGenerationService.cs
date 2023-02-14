using UnityEngine;

public static class BubbleContextExtension
{
    public static GameEntity CreateRandomBubble(this GameContext context, Coordinate coordinate)
    {
        var randomNumber = 1 << Random.Range(1, 10);
        return CreateBoardBubble(context, randomNumber, coordinate);
    }
    
    public static GameEntity CreateBoardBubble
        (this GameContext context, int number, Coordinate coordinate)
    {
        var entity = context.CreateEntity();
        entity.isBubble = true;
        entity.AddValue(number);
        entity.AddCoordinate(coordinate);
        entity.AddAsset("Bubble");
        entity.AddPosition(BubbleNeighbourLogicService.FromCoordToWorldPos(coordinate.Value));
        return entity;
    }
    
    public static GameEntity CreatePreviewBubble
        (this GameContext context)
    {
        var entity = context.CreateEntity();
        var firstCoord = new Coordinate(coord: Vector2Int.zero);
        entity.AddCoordinate(firstCoord);
        entity.AddAsset("Preview");
        entity.AddPosition(BubbleNeighbourLogicService.FromCoordToWorldPos(firstCoord.Value));
        return entity;
    }
    
    public static GameEntity CreateBubbleForShoot
        (this GameContext context, int number, Vector3 startPos)
    {
        var entity = context.CreateEntity();
        entity.AddValue(number);
        entity.isShootingBubble = true;
        entity.AddAsset("Bubble");
        entity.AddPosition(startPos);
        return entity;
    }
    
}