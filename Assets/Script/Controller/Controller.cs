using UnityEngine;
using System.Collections.Generic;

public class Controller : MonoBehaviour {

	IServices services;
	public IServices Services {
		get {
			return services;
		}
	}

	void Start () {
		/*LevelModelComponent component = new LevelModelComponent();
		component.name = "999";
		component.waves = new List<WaveModel>();
		WaveModel model = new WaveModel();
		model.spawnBarrier = 2.0f;
		model.count = 3;
		model.spawnOffset = 0.2f;
		model.speed = 5.0f;
		model.type = 1;
		model.health = 200;
		model.path = 6;
		component.waves.Add(model);
		model = new WaveModel();
		model.spawnBarrier = 2.0f;
		model.count = 3;
		model.spawnOffset = 0.2f;
		model.speed = 5.0f;
		model.type = 1;
		model.health = 200;
		model.path = 7;
		component.waves.Add(model);
		component.waveIndex = 0;

		component.enemies = new List<EnemyModel>();
		EnemyModel enemyModel = new EnemyModel();
		enemyModel.spawnBarrier = 2.0f;
		enemyModel.speed = 5.0f;
		enemyModel.type = 1;
		enemyModel.health = 200;
		enemyModel.path = 7;
		component.enemies.Add(enemyModel);
		enemyModel = new EnemyModel();
		enemyModel.spawnBarrier = 2.0f;
		enemyModel.speed = 5.0f;
		enemyModel.type = 1;
		enemyModel.health = 200;
		enemyModel.path = 7;
		component.enemies.Add(enemyModel);

		component.enemyIndex = 0;
		component.position = new Vector2(-0.5f, 2.0f);
		component.size = new Vector2(1, 100);
		Utils.Serialize(component, "999");*/
		services = new Services(this);
		services.LoadService.ExecuteInit();
	}

	void Update () {
		services.Update();
	}
}
