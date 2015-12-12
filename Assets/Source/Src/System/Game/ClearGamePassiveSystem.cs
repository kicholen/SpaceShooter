using Entitas;

public class ClearGamePassiveSystem {

	protected Pool _pool;
	Group _resources;
	Group _enemySpawners;
	Group _bonusSpawners;
	Group _homeMissileSpawners;
	Group _waveSpawners;
	Group _shakes;
	Group _gameStats;
	Group _grids;
	Group _input;

	public void Init(Pool pool) {
		_pool = pool;
		_resources = pool.GetGroup(Matcher.Resource);
		_enemySpawners = pool.GetGroup(Matcher.EnemySpawner);
		_homeMissileSpawners = pool.GetGroup(Matcher.HomeMissileSpawner);
		_waveSpawners = pool.GetGroup(Matcher.WaveSpawner);
		_shakes = pool.GetGroup(Matcher.Shake);
		_gameStats = pool.GetGroup(Matcher.GameStats);
		_grids = pool.GetGroup(Matcher.Grid);
		_input = pool.GetGroup(Matcher.Input);
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
	
	protected void clearCameraShakes() {
		foreach (Entity e in _shakes.GetEntities()) {
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

	protected void clearGrids() {
		foreach (Entity e in _grids.GetEntities()) {
			e.isDestroyEntity = true;
		}
	}

	protected void setInput(bool enable) {
		foreach (Entity e in _input.GetEntities()) {
			e.input.isInputBlocked = !enable;
		}
	}
}
