using System;
using UnityEngine;

public interface ICarView
{
    public event Action<Collider2D> OnObjectTriggerEnter;

    public void SetNewColor(Color color);
    public void UpdatePosition(Vector2 position);
    public void EnableView();
    public void DisableView();
}