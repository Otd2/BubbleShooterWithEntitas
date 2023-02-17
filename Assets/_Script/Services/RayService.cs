
using System.Collections.Generic;
using UnityEngine;

public class RayService
{
    private readonly int wallLayer;
    private readonly int bubbleLayer;
    private readonly int topLayer;
    private List<Vector3> hitPositions;
    public List<Vector3> HitPositions => hitPositions;

    int wallMask;
    int bubbleMask;
    int topMask;
    private int layerMask; 

    public RayService(int wallLayer, int bubbleLayer)
    {
        this.wallLayer = wallLayer;
        this.bubbleLayer = bubbleLayer;
        this.topLayer = topLayer;
        
        wallMask = 1<<wallLayer;
        bubbleMask = 1<<bubbleLayer;
        topMask = 1 << topLayer;
        
        layerMask = wallMask | bubbleMask;
        hitPositions = new List<Vector3>();

        Contexts.sharedInstance.game.SetTargetCoordinate(new Vector2Int(0, 0));

    }
    
    public static GameEntity CreateTrajectoryView(GameContext context)
    {
        var entity = context.CreateEntity();
        entity.AddTrajectory(null);
        entity.AddAsset("Trajectory");
        entity.AddPosition(new Vector2(0,-4));
        entity.AddVisible(false);
        return entity;
    }
    
    public bool GetTargetCoordinates(Vector2 origin, Vector2 dir, out Vector2Int hitCoord)
    {
        hitPositions.Clear();
        hitCoord = Vector2Int.zero;
        var shootInfo = ShootARay(origin, dir,wallLayer);
        var bounceCount = 0;
        var originTemp = origin;
        hitPositions.Add(originTemp);
        //TODO : REFACTOR THIS LOGIC
        if (shootInfo.collider != null)
        {
            
            while (IsWallHit(shootInfo.collider))
            {
                if (bounceCount >= Contexts.sharedInstance.config.gameConfig.value.MaxAllowedBounce)
                {
                    return false;
                }
                hitPositions.Add(shootInfo.point);
                var reflectedDir = Vector2.Reflect(dir, shootInfo.normal);
                dir = reflectedDir;
                Vector2 normal = shootInfo.normal;
                originTemp = shootInfo.point + normal * 0.01f;
                shootInfo = ShootARay(originTemp,  dir.normalized, wallLayer);
                bounceCount++;
            }
            
            shootInfo = ShootARay(originTemp,  dir, bubbleLayer);
            if (IsBubbleHit(shootInfo.collider))
            {
                hitPositions.Add(shootInfo.point);
                var hitCoordinate = shootInfo.collider.GetComponent<BubbleView>().GetCoordinate();
                var shootDif = shootInfo.collider.transform.position - (Vector3)shootInfo.point;
                hitCoord = GetSnappedPreviewCoordinate(hitCoordinate, shootDif);
                if (Contexts.sharedInstance.game.GetEntityWithCoordinate(hitCoord) != null)
                    return false;

                return true;
            }
        }
        return false;
    }

    private Vector2Int GetSnappedPreviewCoordinate(Vector2Int coordinate, Vector3 shootPositionDiff)
    {
        Vector2Int result = new Vector2Int(0, 0);
        if (shootPositionDiff.y > 0 && shootPositionDiff.x > 0)
        {
            result = BubbleNeighbourLogicService.GetNeighborCoordinate(coordinate, Direction.SW);
        }
        else if (shootPositionDiff.y > 0 && shootPositionDiff.x < 0)
        {
            result = BubbleNeighbourLogicService.GetNeighborCoordinate(coordinate, Direction.SE);
        }
        else if (shootPositionDiff.y < 0 && shootPositionDiff.x > 0)
        {
            result = BubbleNeighbourLogicService.GetNeighborCoordinate(coordinate, Direction.W);
        }
        else if (shootPositionDiff.y < 0 && shootPositionDiff.x < 0)
        {
            result = BubbleNeighbourLogicService.GetNeighborCoordinate(coordinate, Direction.E);
        }
        
        /*if (shootPositionDiff.x > 0)
            x = -2;
        else
            x = 2;
        */
        return result;
    }

    private bool IsWallHit(Collider2D collider)
    {
        return collider != null && collider.gameObject.layer == wallLayer;
    }

    private bool IsBubbleHit(Collider2D collider)
    {
        return collider != null && collider.gameObject.layer == bubbleLayer;
    }

    private RaycastHit2D ShootARay(Vector2 origin, Vector2 dir, LayerMask mask)
    {
        return Physics2D.Raycast(origin, dir, 1000, ~mask);
    }
    
    
}