using Entitas;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyControllerSystem : IReactiveSystem, ISetPool {
	public TriggerOnEvent trigger { get { return Matcher.DifficultyController.OnEntityAdded(); } }
	
	Pool _pool;
	Group _group;
	Group _player;
	
	public void SetPool(Pool pool) {
		_pool = pool;
		_group = pool.GetGroup(Matcher.BonusModel);
		_player = pool.GetGroup(Matcher.Player);
	}
	
	public void Execute(List<Entity> entities) {
		foreach (Entity e in entities) {
			DifficultyControllerComponent difficulty = e.difficultyController;

			switch(difficulty.difficultyType) {
				case DifficultyTypes.Easy:

					break;
				case DifficultyTypes.Normal:

					break;
				case DifficultyTypes.Hard:

					break;
			}
		}
	}
	
	void activateBonus(Entity e, BonusModelComponent bonus) {
		switch(bonus.type) {
		case 1:
			_pool.CreateEntity()
				.AddSpeedBonus(10.0f, 10.0f, 2.0f);
			break;
		default:
			throw new UnityException("Unknown bonus type: " + bonus.type);
			break;
		}
	}
}