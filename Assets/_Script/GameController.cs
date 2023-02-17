using System;
using _Script.Input;
using Entitas;
using UnityEngine;
using Random = System.Random;

/**
 *
 * The GameController creates and manages all systems in Match One
 *
 */

public class GameController
{
    readonly Systems _systems;

    public GameController(Contexts contexts, IGameConfig gameConfig, IPieceColorsConfig colorConfig,
        Transform[] shootOrigin, IInputService inputService, IParticlesService particlesService)
    {
        var random = new Random(DateTime.UtcNow.Millisecond);
        Application.targetFrameRate = 60;
        UnityEngine.Random.InitState(random.Next());
        //Rand.game = new Rand(random.Next());

        contexts.config.SetGameConfig(gameConfig);
        contexts.config.SetPieceColorsConfig(colorConfig);
        particlesService.Init();
        // This is the heart of Match One:
        // All logic is contained in all the sub systems of GameSystems
        _systems = new GameSystems(contexts, inputService, shootOrigin, particlesService);
    }

    public void Initialize()
    {
        // This calls Initialize() on all sub systems
        _systems.Initialize();
    }

    public void Execute()
    {
        // This calls Execute() and Cleanup() on all sub systems
        _systems.Execute();
        _systems.Cleanup();
    }
    
    public void OnDestroy() {
        _systems.TearDown();
    }
}