using System;
using UnityEngine;

public interface IInputHandler
{
    public event Action<Vector2> OnMove;
}
