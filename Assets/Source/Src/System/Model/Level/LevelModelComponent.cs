using Entitas;
using UnityEngine;
using System.Collections.Generic;

public class LevelModelComponent : IComponent {
    public long id;
	public string name;
	public List<WaveModel> waves;
	public int waveIndex;
	public List<EnemyModel> enemies;
	public int enemyIndex;
	public Vector2 position;
	public Vector2 size;
}