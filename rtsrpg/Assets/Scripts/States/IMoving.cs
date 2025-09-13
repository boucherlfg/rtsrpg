using UnityEngine;

namespace States
{
    public interface IMoving
    {
        Vector2? MovementPosition { get; set; }
        float Speed { get; }
    }
}