using Entitas;
using UnityEngine;
using System.Collections.Generic;

public class GameStatsSystem : IMultiReactiveSystem, IInitializeSystem, ISetPool {
	public TriggerOnEvent[] triggers { 
		get {
			TriggerOnEvent[] tri = new TriggerOnEvent[2];
			tri[0] = Matcher.AllOf(Matcher.Enemy, Matcher.CollisionDeath).OnEntityAdded();
			tri[1] = Matcher.AllOf(Matcher.Bonus, Matcher.CollisionDeath).OnEntityAdded();
			return tri;
		}
	}
	Pool _pool;
	Group _group;
	
	public void SetPool(Pool pool) {
		_pool = pool;
		_group = pool.GetGroup(Matcher.GameStats);
	}

	public void Initialize() {
		_pool.CreateEntity()
			.AddGameStats(0, 0, 0);
	}

	public void Execute(List<Entity> entities) {
		GameStatsComponent gameStats = _group.GetSingleEntity().gameStats;
		foreach (Entity e in entities) {
			if (e.hasBonus) {
				updateBonusStat(gameStats, e.bonus);
			}
			else if (e.hasEnemy) {
				gameStats.shipsDestroyed += 1;
			}
			else {
				throw new UnityException("GameStatsSystem: Unrecognized stat");
			}
		}
	}

	void updateBonusStat(GameStatsComponent gameStats, BonusComponent bonus) {
		if (bonus.type == BonusTypes.Star) {
			gameStats.starsPicked += 1;
		}
		else {
			gameStats.bonusesPicked += 1;
		}
	}
}
