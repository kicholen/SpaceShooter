using Entitas;
using System.Collections.Generic;
using UnityEngine;

public class ShieldCollisionComponent : IComponent {
    public float time;
    public float duration;
    public Queue<Vector2> collisionsPosition;
}