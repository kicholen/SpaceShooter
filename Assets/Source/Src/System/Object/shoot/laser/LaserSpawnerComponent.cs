using Entitas;
using UnityEngine;

public class LaserSpawnerComponent : IComponent {
	public float velocity;
	public float height;
	public float angle;
	public Vector2 direction; 
	public int collisionType;
	public Entity laser;
}