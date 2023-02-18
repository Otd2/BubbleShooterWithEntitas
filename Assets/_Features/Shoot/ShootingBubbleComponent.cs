using Entitas;
using Entitas.CodeGeneration.Attributes;

[Game, Event(EventTarget.Self)]
public class ShootingBubbleComponent : IComponent
{
    public int shootIndex;
}