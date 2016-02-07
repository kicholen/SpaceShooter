using Entitas;
using UnityEngine;

public class MissileSpawnerComponent : IComponent {
	public float time;
	public int damage;
	public float spawnDelay;
	public string resource;
	public Vector2 velocity;
	public int collisionType;
}