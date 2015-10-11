using Entitas;
using UnityEngine;

public class HomeMissileSpawnerComponent : IComponent {
	public GameObject target;
	public float time;
	public float spawnDelay;
	public string resource;
	public float velocity;
	public int collisionType;
}