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

    GameController _gameController;

    void Awake() => _gameController =
        new GameController(Contexts.sharedInstance, 
            gameConfigSo, colorConfigSo, 
            shootBubblePositions, inputService,
            particlesService);

    void Start() => _gameController.Initialize();
    void Update() => _gameController.Execute();
    void OnDestroy() => _gameController.OnDestroy();
}

