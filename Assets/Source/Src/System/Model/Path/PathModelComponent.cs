using Entitas;
using UnityEngine;
using System.Collections.Generic;

public class PathModelComponent : IComponent {
    public string id;
    public List<Vector2> points;
}