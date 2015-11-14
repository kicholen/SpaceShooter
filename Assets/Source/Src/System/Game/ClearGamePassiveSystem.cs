using Entitas;

public class ClearGamePassiveSystem {

	protected Pool _pool;
	Group _resources;
	Group _enemySpawners;
	Group _bonusSpawners;
	Group _homeMissileSpawners;
	Group _waveSpawners;
	Group _bonuses;
	Group _cameraShakes;
	Group _gameStats;

	public void SetPool(Pool pool) {
		_resources = pool.GetGroup(Matcher.Resource);
		_enemySpawners = pool.GetGroup(Matcher.EnemySpawner);
		_homeMissileSpawners = pool.GetGroup(Matcher.HomeMissileSpawner);
		_waveSpawners = pool.GetGroup(Matcher.WaveSpawner);
		_cameraShakes = pool.GetGroup(Matcher.CameraShake);
		_bonuses = pool.GetGroup(Matcher.BonusModel);
		_gameStats = pool.GetGroup(Matcher.GameStats);
	}
	
	protected void clearEnemySpawners() {
		foreach (Entity e in _enemySpawners.GetEntities()) {
			_pool.DestroyEntity(e);
		}
	}
	
	protected void clearHomeMissileSpawners() {
		foreach (Entity e in _homeMissileSpawners.GetEntities()) {
			if (!e.hasChild) {
				e.isDestroyEntity = true;
			}
		}
	}
	
	protected void clearWaveSpawners() {
		foreach (Entity e in _waveSpawners.GetEntities()) {
			e.isDestroyEntity = true;
		}
	}
	
	protected void clearGameObjects() {
		foreach (Entity e in _resources.GetEntities()) {
			if (!e.hasChild) {
				e.isDestroyEntity = true;
			}
		}
	}
	
	protected void clearBonuses() {
		foreach (Entity e in _bonuses.GetEntities()) {
			if (!e.hasChild) {
				e.isDestroyEntity = true;
			}
		}
	}
	
	protected void clearCameraShakes() {
		foreach (Entity e in _cameraShakes.GetEntities()) {
			if (!e.hasChild) {
				e.isDestroyEntity = true;
			}
		}
	}
	
	protected void clearGameStats() {
		foreach (Entity e in _gameStats.GetEntities()) {
			e.ReplaceGameStats(0, 0, 0);
		}
	}
}
