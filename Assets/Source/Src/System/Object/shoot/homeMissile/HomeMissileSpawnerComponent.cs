using Entitas;
using UnityEngine;

public class HomeMissileSpawnerComponent : IComponent {
	public float time;
	public float spawnDelay;
	public int damage;
	public string resource;
	public float velocity;
	public Vector2 startVelocity;
	public float followDelay;
    public float selfDestructionDelay;
    public int ownerCollisionType;
}