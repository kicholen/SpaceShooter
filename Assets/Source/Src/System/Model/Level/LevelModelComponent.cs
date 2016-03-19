using Entitas;
using UnityEngine;
using System.Collections.Generic;

public class LevelModelComponent : IComponent {
    public long id;
	public string name;
	public List<WaveSpawnModel> waves;
	public int waveIndex;
	public List<EnemySpawnModel> enemies;
	public int enemyIndex;
	public Vector2 position;
	public Vector2 size;
}