using Entitas;
using UnityEngine;

public class TimerSystem : IExecuteSystem, ICleanupSystem
{
    readonly IGroup<GameEntity> _timers;
    readonly IGroup<GameEntity> _timerCompletes;
    readonly GameContext _context;

    public TimerSystem(Contexts contexts)
    {
        _context = contexts.game;
        _timers = _context.GetGroup(GameMatcher.Timer);
        _timerCompletes = _context.GetGroup(GameMatcher.TimerComplete);
    }

    public void Execute()
    {
        foreach (var e in _timers.GetEntities())
        {
            if (e.timer.time > 0)
            {
                e.ReplaceTimer(e.timer.time - Time.deltaTime);
            }
            else
            {
                e.isTimerComplete = true;
            }
        }
    }

    public void Cleanup()
    {
        foreach (var e in _timerCompletes.GetEntities())
        {
            e.RemoveTimer();
            e.isTimerComplete = false;
        }
    }
}