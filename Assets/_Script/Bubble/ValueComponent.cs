using Entitas;
using Entitas.CodeGeneration.Attributes;

[Game, Event(EventTarget.Self)]
public class ValueComponent : IComponent
{
    public int Number;
}