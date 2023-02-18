﻿using Entitas;
using Entitas.CodeGeneration.Attributes;

[Game, Event(EventTarget.Self)]
public class VisibleComponent : IComponent
{
    public bool isVisible;
}