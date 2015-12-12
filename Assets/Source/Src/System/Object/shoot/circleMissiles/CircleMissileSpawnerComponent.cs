using Entitas;

public class CircleMissileSpawnerComponent : IComponent {
	public int amount;
	public int damage;
	public float time;
	public float spawnDelay;
	public string resource;
	public float velocity;
	public int collisionType;
}