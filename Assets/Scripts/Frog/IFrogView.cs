using log4net.Util;
using System;
using UnityEngine;

public interface IFrogView
{
    public event Action<Collider2D> OnTriggerEnterEvent;

    public void UpdatePosition(Vector2 position);
}
