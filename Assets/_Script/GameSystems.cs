using _Script.Input;
using UnityEngine;

public class GameSystems : Feature
{
    public GameSystems(Contexts contexts, IInputService inputService, Transform shootOrigin)
    {
        RayService rayService = new RayService(6,7);

        Add(new EmitInputSystem(contexts, inputService));
        Add(new AimSystem(contexts, shootOrigin.position));
        Add(new TrajectorySystem(contexts.game, rayService));
        Add(new PreviewSystem(contexts));

        Add(new MapInitSystem(contexts));

        Add(new ShootSystem(contexts));

        Add(new AddViewSystem(contexts));
        
        // Events (Generated)
        //Add(new InputEventSystems(contexts));
        Add(new GameEventSystems(contexts));
        //Add(new GameStateEventSystems(contexts));
    }
}