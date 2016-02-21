using Entitas;
using UnityEngine;

public class LaserSpawnerComponent : IComponent {
	public float velocity;
	public float height;
	public float maxHeight;
    public float angle;
	public Vector2 direction; 
	public int collisionType;
	public int damage;
    public string resource;
    public Entity laser;
}