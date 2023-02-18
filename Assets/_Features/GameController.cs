using _Script.Input;
using Entitas;
using UnityEngine;

public class GameController
{
    readonly Systems _systems;

    public GameController(Contexts contexts, IGameConfig gameConfig, IPieceColorsConfig colorConfig,
        Transform[] shootOrigin, IInputService inputService, IParticlesService particlesService,
        IScoreUIPoolService scoreUIPoolService)
    {
        Application.targetFrameRate = 60;

        contexts.config.SetGameConfig(gameConfig);
        contexts.config.SetPieceColorsConfig(colorConfig);
        particlesService.Init();
        scoreUIPoolService.Init();
        _systems = new GameSystems(contexts, inputService, shootOrigin, particlesService, scoreUIPoolService);
    }

    public void Initialize()
    {
        _systems.Initialize();
    }

    public void Execute()
    {
        _systems.Execute();
        _systems.Cleanup();
    }
    
    public void OnDestroy() {
        _systems.TearDown();
    }
}