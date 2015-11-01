using Entitas;

public class CircleMissileRotatedSpawnerComponent : IComponent {
	public int amount;
	public int waves;
	public int angle;
	public int angleOffset;
	public float time;
	public float spawnDelay;
	public string resource;
	public float velocity;
	public int collisionType;
}