using Entitas;
using UnityEngine;

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
	}
	
	void changeCamera() {
		Group group = _pool.GetGroup(Matcher.Camera);
		Entity e = group.GetSingleEntity();
		Entity player = _playerGroup.GetSingleEntity();

		if (e.hasSmoothCamera) {
			e.AddRegularCamera(e.smoothCamera.offset)
				.RemoveVelocity()
				.RemovePosition();
			e.RemoveSmoothCamera();
		}
		else if (e.hasRegularCamera) {
			e.AddSmoothCamera(e.regularCamera.offset)
				.AddVelocity(0.0f, 2.0f)
				.AddPosition(player.position.x, player.position.y + e.regularCamera.offset.y);
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
			player.AddHomeMissileSpawner(0.0f, 1.0f, Resource.Missile, 5.0f, CollisionTypes.Player);
		}
	}

	void laserWeapon() {
		Entity player = _playerGroup.GetSingleEntity();
		
		if (player.hasLaserSpawner) {
			player.RemoveLaserSpawner();
		}
		else {
			player.AddLaserSpawner(5.0f, 0.0f, 0.0f, new Vector2(), CollisionTypes.Player, null);
		}
	}

	void pauseOrUnpauseGame() {
		if (_pause.Count > 0) {
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
		Utils.SerializeComponent(_settings.GetSingleEntity().settingsModel);
		Utils.SerializeComponent(_playerModel.GetSingleEntity().playerModel);
	}
}