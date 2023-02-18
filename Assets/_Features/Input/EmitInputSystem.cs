using _Script.Input;
using Entitas;
using UnityEngine;


    
public class EmitInputSystem : IInitializeSystem, IExecuteSystem {    
    Contexts _contexts;
    IInputService _inputService; 
    InputEntity _inputEntity;

    public EmitInputSystem (Contexts contexts, IInputService inputService) {
        _contexts = contexts;
        _inputService= inputService;
    }

    public void Initialize() {     
        _contexts.input.isInputManager = true;
        _inputEntity = _contexts.input.inputManagerEntity;
        _inputEntity.AddTouchPosition(Vector2.negativeInfinity);
    }

    public void Execute () {
        _inputEntity.ReplaceTouchPosition(_inputService.InputPosition);
        _inputEntity.isTouching = _inputService.IsHolding;
        _inputEntity.isReleased = _inputService.IsReleased;
    }
}