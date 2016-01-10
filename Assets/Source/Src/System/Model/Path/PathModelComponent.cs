using Entitas;
using UnityEngine;
using System.Collections.Generic;

public class PathModelComponent : IComponent {
    public long id;
    public string name;
    public List<Vector2> points;
}