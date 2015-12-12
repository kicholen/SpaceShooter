using Entitas;

public class HomeMissileSpawnerComponent : IComponent {
	public float time;
	public float spawnDelay;
	public int damage;
	public string resource;
	public float velocity;
	public int ownerCollisionType;
}