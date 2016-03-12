using Entitas;

public class ClearGamePassiveSystem {

	protected Pool _pool;
	Group resources;
	Group enemySpawners;
	Group bonusSpawners;
	Group homeMissileSpawners;
	Group waveSpawners;
    Group missileSpawners;
    Group shakes;
	Group gameStats;
	Group grids;
	Group input;
    Group shipBonus;

    public void Init(Pool pool) {
		_pool = pool;
		resources = pool.GetGroup(Matcher.Resource);
		enemySpawners = pool.GetGroup(Matcher.EnemySpawner);
		homeMissileSpawners = pool.GetGroup(Matcher.HomeMissileSpawner);
		waveSpawners = pool.GetGroup(Matcher.WaveSpawner);
        missileSpawners = pool.GetGroup(Matcher.MissileSpawner);
        shakes = pool.GetGroup(Matcher.Shake);
		gameStats = pool.GetGroup(Matcher.GameStats);
		grids = pool.GetGroup(Matcher.Grid);
		input = pool.GetGroup(Matcher.Input);
		shipBonus = pool.GetGroup(Matcher.ShipBonus);
    }
	
	protected void clearEnemySpawners() {
		foreach (Entity e in enemySpawners.GetEntities()) {
			_pool.DestroyEntity(e);
		}
	}
	
	protected void clearHomeMissileSpawners() {
		foreach (Entity e in homeMissileSpawners.GetEntities()) {
			if (!e.hasChild) {
				e.isDestroyEntity = true;
			}
		}
	}
	
	protected void clearWaveSpawners() {
		foreach (Entity e in waveSpawners.GetEntities()) {
			e.isDestroyEntity = true;
		}
	}

    protected void clearMissileSpawners() {
        foreach (Entity e in missileSpawners.GetEntities()) {
            if (!e.hasChild) {
                e.isDestroyEntity = true;
            }
        }
    }

    protected void clearGameObjects() {
		foreach (Entity e in resources.GetEntities()) {
			if (!e.hasChild) {
				e.isDestroyEntity = true;
			}
		}
	}
	
	protected void clearCameraShakes() {
		foreach (Entity e in shakes.GetEntities()) {
            e.RemoveShake();
		}
	}
	
	protected void clearGameStats() {
		foreach (Entity e in gameStats.GetEntities()) {
			e.ReplaceGameStats(0, 0, 0, 0);
		}
	}

	protected void clearGrids() {
		foreach (Entity e in grids.GetEntities()) {
			e.isDestroyEntity = true;
		}
	}

	protected void setInput(bool enable) {
		foreach (Entity e in input.GetEntities()) {
			e.input.isInputBlocked = !enable;
		}
    }

    protected void clearShipBonus()
    {
        foreach (Entity e in shipBonus.GetEntities())
        {
            e.isDestroyEntity = true;
        }
    }
}
