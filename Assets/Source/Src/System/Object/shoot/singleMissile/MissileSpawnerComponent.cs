using Entitas;

public class MissileSpawnerComponent : IComponent {
	public float time;
	public int damage;
	public float spawnDelay;
	public string resource;
	public float velocityX;
	public float velocityY;
	public int collisionType;
}