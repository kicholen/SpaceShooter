using Entitas;
using UnityEngine;

public class CircleMissileSpawnerComponent : IComponent {
	public int amount;
	public float time;
	public float spawnDelay;
	public string resource;
	public float velocity;
	public int collisionType;
}