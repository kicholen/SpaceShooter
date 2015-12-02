using Entitas;
using UnityEngine;

public class BackgroundSystem : IExecuteSystem, ISetPool {
	
	Pool _pool;
	Group _group;
	Group _star;
	
	public void SetPool(Pool pool) {
		_pool = pool;
		_group = _pool.GetGroup(Matcher.Background);
		_star = pool.GetGroup(Matcher.BackgroundStar);
	}
	
	public void Execute() {
		for (int i = 0; i < _group.count; i++) {
			Entity entity = _group.GetSingleEntity();
			BackgroundComponent component = entity.background;

			if (component.starsCount > _star.count) {
				PositionComponent position = entity.position;
				spawnStar(component, position);
			}
		}
	}

	void spawnStar(BackgroundComponent component, PositionComponent position) {
		_pool.CreateEntity()
			.AddVelocity(new Vector2(0.0f, -Random.Range(2.0f, 3.0f)))
			.AddPosition(new Vector2(Random.Range(position.pos.x + component.dimension.x / 2.0f, position.pos.x - component.dimension.x / 2.0f), 
			                         Random.Range(position.pos.y + component.dimension.y / 2.0f + Config.CAMERA_START_OFFSET, position.pos.y - component.dimension.y / 2.0f + Config.CAMERA_START_OFFSET)))
			.AddResource(Resource.Particle)
			.IsBackgroundStar(true)
			.AddAlpha(6.0f, 6.0f);
	}
}