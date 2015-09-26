using Entitas;
using UnityEngine;

public class CreatePlayerWeaponSystem : IInitializeSystem, ISetPool {
	Pool _pool;
	Group _players;
	
	public void SetPool(Pool pool) {
		_pool = pool;
		_players = _pool.GetGroup(Matcher.Player);
	}
	
	public void Initialize() {
		Debug.Log("CreatePlayerWeaponSystem");
		foreach (Entity e in _players.GetEntities()) {
			e.AddMissileSpawner(5.0f, 5.0f, true);
		}
	}
}
