using System.Collections.Generic;
using UnityEngine;

public static class BubbleContextExtension
{
    public static GameEntity CreateRandomBubble(this GameContext context, Vector2Int coordinate)
    {
        var randomNumber = 1 << Random.Range(1, 8);
        return CreateBoardBubble(context, randomNumber, coordinate);
    }
    public static GameEntity CreateBoardBubble
        (this GameContext context, int number, Vector2Int coordinate)
    {
        var entity = context.CreateEntity();
        entity.isBubble = true;
        entity.AddValue(number);
        entity.AddCoordinate(coordinate);
        entity.AddAsset("Bubble");
        entity.AddPosition(BubbleNeighbourLogicService.FromCoordToWorldPos(coordinate));
        return entity;
    }
    
    public static GameEntity CreateTopLayerBubble
        (this GameContext context, Vector2Int coordinate)
    {
        var entity = context.CreateEntity();
        entity.AddCoordinate(coordinate);
        entity.AddAsset("TopLayerBubble");
        entity.AddPosition(BubbleNeighbourLogicService.FromCoordToWorldPos(coordinate));
        entity.isTopLayer = true;
        return entity;
    }
    
    public static GameEntity CreatePreviewBubble
        (this GameContext context)
    {
        var entity = context.CreateEntity();
        entity.AddAsset("Preview");
        entity.AddPosition(new Vector2(-100,-100));
        return entity;
    }
    
    public static GameEntity CreateBubbleForShoot
        (this GameContext context, int number, Vector3 startPos)
    {
        var entity = context.CreateEntity();
        
        //var randomNumber = 1 << Random.Range(1, 8);
        entity.AddValue(number);
        entity.isShootingBubble = true;
        entity.AddAsset("Bubble");
        entity.AddPosition(startPos);
        return entity;
    }
    
    /*
    public static List<GameEntity> GetNeighboursWithNumber
        (this GameContext context, int number, Vector2Int center)
    {

        var allBubbles = context.GetEntities(GameMatcher.Coordinate);
        
        var neighbours = BubbleNeighbourLogicService.GetNeighbourCoordinates(center);


        foreach (var neighbour in neighbours)
        {
            
        }
    }
    */
    
}