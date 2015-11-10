using Entitas;
using UnityEngine;

public class WaveSpawnerComponent : IComponent {
	public int count;
	public int type;
	public Vector2 position;
	public float timeOffset;
	public float time;
	public float speed;
	public int health;
	public int path;
}