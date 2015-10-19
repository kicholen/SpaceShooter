using Entitas;

public class CircleMissileRotatedSpawnerComponent : IComponent {
	public int amount;
	public int waves;
	public int angle;
	public int angleOffset;
	public float time;
	public float spawnDelay;
	public string resource;
	public float velocityX;
	public float velocityY;
	public float velocityOffsetX;
	public float velocityOffsetY;
	public int collisionType;
}