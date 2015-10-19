using Entitas;
using UnityEngine;

public class LaserComponent : IComponent {
	public float height;
	public Vector2 direction;
	public Entity source;
}