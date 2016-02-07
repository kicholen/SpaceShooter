using Entitas;
using UnityEngine;

public class MultipleMissileSpawnerComponent : IComponent {
	public int amount;
	public int damage;
	public int currentAmount;
	public float timeDelay;
	public float delay;
	public float time;
	public float spawnDelay;
	public string resource;
	public float randomPositionOffsetX;
	public Vector2 startVelocity;
	public int collisionType;
}