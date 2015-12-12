using Entitas;

public class ShipModelComponent : IComponent {
	public string name;

	public float maxVelocity;
	public int health;

	public bool hasLaser;
	public int laserDamage;

	public bool hasMissile;
	public float missileVelocity;
	public float missileSpawnDelay;
	public int missileDamage;

	public bool hasSecondaryMissiles;
	public float secondaryMissileVelocity;
	public float secondaryMissileSpawnDelay;
	public int secondaryMissileDamage;

	public bool hasHomeMissile;
	public float homeMissileVelocity;
	public float homeMissileSpawnDelay;
	public int homeMissileDamage;

	public bool hasMagnetField;
	public float magnetRadius;
}