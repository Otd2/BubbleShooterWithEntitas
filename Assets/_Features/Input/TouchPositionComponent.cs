using System.Collections;
using System.Collections.Generic;
using Entitas;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;

[Input, Unique]
public class TouchPositionComponent : IComponent
{
    public Vector2 touchPos;
}
