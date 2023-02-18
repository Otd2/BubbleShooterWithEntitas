using _Script.Input;
using UnityEngine;

public class GameSystems : Feature
{
    public GameSystems(Contexts contexts, IInputService inputService, 
        Transform[] shootOrigins, IParticlesService particlesService,
        IScoreUIPoolService scoreUIPoolService)
    {
        RayService rayService = new RayService(6,7);

        //INPUT
        Add(new EmitInputSystem(contexts, inputService));
        
        //AIM
        Add(new AimSystem(contexts, shootOrigins[0].position));
        Add(new TrajectorySystem(contexts, rayService));
        
        //PREVIEW
        Add(new PreviewSystem(contexts));
        Add(new PreviewColorSystem(contexts));
        
        //MAP CREATION
        Add(new MapInitSystem(contexts));
        
        //SHOOT
        Add(new ShootBubbleCreatorSystem(contexts, shootOrigins));
        Add(new ShootTriggerSystem(contexts));
        Add(new TimerSystem(contexts));
        Add(new ShootSystem(contexts));
        Add(new PathCompletedSystem(contexts));
        
        //BOUNCE
        Add(new BounceTriggerSystem(contexts));
        
        //MERGE
        Add(new MergeControlSystem(contexts));
        Add(new MergeTargetControlSystem(contexts));
        Add(new MergeTriggerSystem(contexts));
        Add(new ParticlesSystem(contexts, particlesService));
        Add(new MergeCompletedSystem(contexts));
        
        //SCORE
        Add(new ScoreSystem(contexts, scoreUIPoolService));
        
        //BOMB
        Add(new BombSystem(contexts, particlesService));
        
        //VIEW
        Add(new AddViewSystem(contexts));
        
        //FALL
        Add(new FallDetectSystem(contexts));
        Add(new FallTriggerSystem(contexts));
        Add(new FallItemDestroyerSystem(contexts, particlesService));
        
        //GENERATED
        Add(new GameEventSystems(contexts));
    }
}