using Entitas;
using UnityEngine;
using System.IO;

public class TestSystem : IInitializeSystem, IExecuteSystem, ISetPool {
	const float EPSILON = 0.005f;

	Pool _pool;
	Group _playerGroup;
	Group _timeGroup;
	Group _pause;
	Group _settings;

	public void SetPool(Pool pool) {
		_pool = pool;
		_playerGroup = pool.GetGroup(Matcher.Player);
		_timeGroup = pool.GetGroup(Matcher.Time);
		_pause = pool.GetGroup(Matcher.PauseGame);
		_settings = pool.GetGroup(Matcher.SettingsModel);
	}

	public void Initialize() {
		_pool.CreateEntity()
			.isTest = true;
	}

	public void Execute() {
		if (Input.GetKeyDown(KeyCode.C)) {
			changeCamera();
		}
		if (Input.GetKeyDown(KeyCode.T)) {
			changeTime();
		}
		if (Input.GetKeyDown(KeyCode.H)) {
			homeMissile();
		}
		if (Input.GetKeyDown(KeyCode.L)) {
			laserWeapon();
		}
		if (Input.GetKeyDown(KeyCode.P)) {
			pauseOrUnpauseGame();
		}
		if (Input.GetKeyDown(KeyCode.M)) {
			playSound();
		}
		if (Input.GetKeyDown(KeyCode.D)) {
			changeDifficulty();
		}
		if (Input.GetKeyDown(KeyCode.S)) {
			serialize();
		}
		if (Input.GetKeyDown(KeyCode.R)) {
			restart();
		}
		if (Input.GetKeyDown(KeyCode.Z)) {
			clearPersistentFolder();
		}
		if (Input.GetKeyDown(KeyCode.E)) {
			endGame();
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            _pool.CreateEntity()
                .AddHealth(10000)
                .AddVelocityLimit(0.0f)
                .AddVelocity(new Vector2())
                .AddCollision(CollisionTypes.Enemy, 5000)
                .AddBonusOnDeath(BonusTypes.Star | BonusTypes.Speed)
                .AddExplosionOnDeath(1.0f, Resource.Explosion)
                .AddResource(ResourceWithColliders.Blockade)
                .IsMoveWithCamera(true)
                .AddMotherShip(0.0f, 1.0f, 3.0f, 0, 4, 1, 1000, 1, 3.0f)
                .AddPosition(new Vector2(0.0f, _playerGroup.GetSingleEntity().position.pos.y + 1.0f));
        }
    }
	
	void changeCamera() {
		Group group = _pool.GetGroup(Matcher.Camera);
		Entity e = group.GetSingleEntity();
		Vector2 playerPosition = _playerGroup.GetSingleEntity().position.pos;

		if (e.hasSmoothCamera) {
			e.AddDefaultCamera(e.smoothCamera.offset)
				.RemoveVelocity();
			e.RemoveSmoothCamera();
		}
		else if (e.hasDefaultCamera) {
			e.AddSmoothCamera(e.defaultCamera.offset)
				.AddVelocity(new Vector2(0.0f, 2.0f))
				.ReplacePosition(new Vector2(playerPosition.x, playerPosition.y + e.defaultCamera.offset.y));
			e.RemoveDefaultCamera();
		}
	}

	void changeTime() {
		TimeComponent component = _timeGroup.GetSingleEntity().time;
		component.modificator = Mathf.Abs(component.modificator - 1.0f) < EPSILON ? 0.2f : 1.0f;
	}

	void homeMissile() {
		Entity player = _playerGroup.GetSingleEntity();

		if (player.hasHomeMissileSpawner) {
			player.RemoveHomeMissileSpawner();
		}
		else {
            player.AddHomeMissileSpawner(0.0f, 0.2f, 10, ResourceWithColliders.MissileHoming, 7.0f, new Vector2(7.0f, -0.8f), 0.4f, 3.0f, CollisionTypes.Player);
		}
	}

	void laserWeapon() {

	}

	void pauseOrUnpauseGame() {
		if (_pause.count > 0) {
			_pause.GetSingleEntity()
				.isDestroyEntity = true;
		}
		else {
			_pool.CreateEntity()
				.isPauseGame = true;
		}
	}

	void playSound() {
		_pool.CreateEntity()
			.AddSound("Sound/test", 1.0f, null);
	}

	void changeDifficulty() {
		Entity e = _settings.GetSingleEntity();
		SettingsModelComponent model = e.settingsModel;
		int difficulty = model.difficulty + 1;
		if (difficulty == DifficultyTypes.Hard) {
			difficulty = DifficultyTypes.Easy;
		}
		e.ReplaceSettingsModel(difficulty, model.music, model.sound, model.language);
	}

	void serialize() {
		Utils.Serialize(_settings.GetSingleEntity().settingsModel);
		//Utils.Serialize(_playerModel.GetSingleEntity().playerModel);

		int i = 1;
		foreach (Entity e in _pool.GetGroup(Matcher.BonusModel).GetEntities()) {
			Utils.Serialize(e.bonusModel, i.ToString());
		}

		i = 1;
		foreach (Entity e in _pool.GetGroup(Matcher.PathModel).GetEntities()) {
			Utils.Serialize(e.pathModel, i.ToString());
		}
	}

	void restart() {
		_pool.CreateEntity()
			.AddStartGame(1);
	}

	void clearPersistentFolder() {
		if (File.Exists(Application.persistentDataPath + "/PlayerModelComponent.xml")) {
			File.Delete(Application.persistentDataPath + "/PlayerModelComponent.xml");
		}
		if (File.Exists(Application.persistentDataPath + "/SettingsModelComponent.xml")) {
			File.Delete(Application.persistentDataPath + "/SettingsModelComponent.xml");
		}
	}

	void endGame() {
		_pool.CreateEntity()
			.isEndGame = true;
	}
}