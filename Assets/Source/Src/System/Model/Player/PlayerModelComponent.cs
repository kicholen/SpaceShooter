using Entitas;
using UnityEngine;

public class PlayerModelComponent : IComponent {
	public string name;

	public Vector2 velocityLimit;
	public int health;

	public bool hasLaser;
	public float laserDamage;

	public bool hasMissile;
	public float missileVelocity;
	public float missileSpawnDelay;
	public float missileDamage;

	public float hasSecondaryMissiles;
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