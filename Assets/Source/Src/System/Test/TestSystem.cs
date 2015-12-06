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
	Group _playerModel;

	public void SetPool(Pool pool) {
		_pool = pool;
		_playerGroup = pool.GetGroup(Matcher.Player);
		_timeGroup = pool.GetGroup(Matcher.Time);
		_pause = pool.GetGroup(Matcher.PauseGame);
		_settings = pool.GetGroup(Matcher.SettingsModel);
		_playerModel = pool.GetGroup(Matcher.PlayerModel);
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
	}
	
	void changeCamera() {
		Group group = _pool.GetGroup(Matcher.Camera);
		Entity e = group.GetSingleEntity();
		Vector2 playerPosition = _playerGroup.GetSingleEntity().position.pos;

		if (e.hasSmoothCamera) {
			e.AddRegularCamera(e.smoothCamera.offset)
				.RemoveVelocity();
			e.RemoveSmoothCamera();
		}
		else if (e.hasRegularCamera) {
			e.AddSmoothCamera(e.regularCamera.offset)
				.AddVelocity(new Vector2(0.0f, 2.0f))
				.ReplacePosition(new Vector2(playerPosition.x, playerPosition.y + e.regularCamera.offset.y));
			e.RemoveRegularCamera();
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
			player.AddHomeMissileSpawner(0.0f, 1.0f, Resource.MissilePrimary, 5.0f, CollisionTypes.Player);
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
		Utils.Serialize(_playerModel.GetSingleEntity().playerModel);

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