using Entitas;

public class MultipleMissileSpawnerComponent : IComponent { // todo change stupid field names
	public int amount;
	public int damage;
	public int currentAmount;
	public float timeDelay;
	public float delay;
	public float time;
	public float spawnDelay;
	public string resource;
	public float randomPositionOffsetX;
	public float velocityX;
	public float velocityY;
	public int collisionType;
}