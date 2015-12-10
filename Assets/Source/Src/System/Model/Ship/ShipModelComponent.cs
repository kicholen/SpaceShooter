using Entitas;

public class ShipModelComponent : IComponent {
	public string name;

	public float maxVelocity;
	public int health;

	public bool hasLaser;
	public float laserDamage;

	public bool hasMissile;
	public float missileVelocity;
	public float missileSpawnDelay;
	public float missileDamage;

	public bool hasSecondaryMissiles;
	public float secondaryMissileVelocity;
	public float secondaryMissileSpawnDelay;
	public float secondaryMissileDamage;

	public bool hasHomeMissile;
	public float homeMissileVelocity;
	public float homeMissileSpawnDelay;
	public float homeMissileDamage;

	public bool hasMagnetField;
	public float magnetRadius;
}