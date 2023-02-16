using System.Collections.Generic;
using Entitas;
using UnityEngine;

public static class DFSHelper
{
    static Stack<GameEntity> stack = new Stack<GameEntity>();
    
    public static void ClearVisitedStack(this Contexts contexts)
    {
        stack.Clear();
        var visitedNodes = contexts.game.GetEntities(GameMatcher.VisitedNode);
        foreach (var visitedNode in visitedNodes)
        {
            visitedNode.isVisitedNode = false;
        }
    }
    public static void DFS(this Contexts contexts, GameEntity startNode) {
        if (startNode == null) {
            return;
        }

        stack.Push(startNode);

        while (stack.Count > 0) {
            GameEntity node = stack.Pop();

            if (!node.isVisitedNode) {
                node.isVisitedNode = true;
                foreach (Vector2Int neighbor in BubbleNeighbourLogicService.GetNeighbourCoordinates(node.coordinate.value))
                {
                    var neighbourEntity = contexts.game.GetEntityWithCoordinate(neighbor);
                    if (neighbourEntity is { isVisitedNode: false }) {
                        stack.Push(neighbourEntity);
                    }
                }
            }
        }
    }
}