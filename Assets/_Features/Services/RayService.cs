
using System.Collections.Generic;
using UnityEngine;

public class RayService
{
    private readonly int wallLayer;
    private readonly int bubbleLayer;
    private readonly int topLayer;
    private List<Vector3> hitPositions;
    public List<Vector3> HitPositions => hitPositions;

    public RayService(int wallLayer, int bubbleLayer)
    {
        this.wallLayer = wallLayer;
        this.bubbleLayer = bubbleLayer;
        
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
        
        //THIS LOGIC NEEDS TO BE SIMPLIFIED
        if (shootInfo.collider != null)
        {
            while (IsWallHit(shootInfo.collider))
            {
                if (bounceCount >= Contexts.sharedInstance.config.gameConfig.value.MaxAllowedBounce)
                {
                    //More than max allowed bounce
                    return false;
                }
                
                //Reflecting the ray and shooting a ray from touch point
                hitPositions.Add(shootInfo.point);
                var reflectedDir = Vector2.Reflect(dir, shootInfo.normal);
                dir = reflectedDir;
                Vector2 normal = shootInfo.normal;
                
                //This hack is needed to cast the new ray from the touch point
                originTemp = shootInfo.point + normal * 0.01f; 
                
                shootInfo = ShootARay(originTemp,  dir.normalized, wallLayer);
                bounceCount++;
            }
            
            shootInfo = ShootARay(originTemp,  dir, bubbleLayer);
            if (IsBubbleHit(shootInfo.collider))
            {
                //bubble hit
                hitPositions.Add(shootInfo.point);
                var hitCoordinate = shootInfo.collider.GetComponent<BubbleView>().GetCoordinate();
                var shootDif = shootInfo.collider.transform.position - (Vector3)shootInfo.point;
                hitCoord = GetSnappedPreviewCoordinate(hitCoordinate, shootDif);

                if (Contexts.sharedInstance.game.GetEntityWithCoordinate(hitCoord) != null)
                {
                    //If hits to a deactivated bubble
                    return false;
                }
                    
                // Successfully created the trajectory points
                return true;
            }
        }
        return false;
    }

    //Calculating the coordinates for the Preview Bubble
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