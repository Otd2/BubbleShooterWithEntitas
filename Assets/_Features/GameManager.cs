using System;
using System.Collections;
using System.Collections.Generic;
using _Script.Input;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private LeanTouchInputService inputService;
    [SerializeField] private Transform[] shootBubblePositions;
    [SerializeField] private GameConfigSO gameConfigSo;
    [SerializeField] private BubbleColorConfigSO colorConfigSo;
    [SerializeField] private ParticleService particlesService;
    [SerializeField] private ScoreUIPoolService scoreUIPoolService;

    GameController _gameController;
    private GameState gameState = GameState.FirstOpen;

    void Awake() => _gameController =
        new GameController(Contexts.sharedInstance, 
            gameConfigSo, colorConfigSo, 
            shootBubblePositions, inputService,
            particlesService, scoreUIPoolService);

    IEnumerator Start()
    {
        //A little bit time after pressing play
        yield return new WaitForSeconds(0.5f);
        gameState = GameState.Start;
        _gameController.Initialize();
    } 
    void Update()
    {
        if(gameState == GameState.Start)
            _gameController.Execute();
    }

    void OnDestroy()
    {
        if(gameState == GameState.Start)
            _gameController.OnDestroy();
    }
}

public enum GameState
{
    FirstOpen,
    Start
}

