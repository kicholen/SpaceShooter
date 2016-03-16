using Entitas;
using UnityEngine;

public class BackgroundSystem : IExecuteSystem, IInitializeSystem, ISetPool {
	
	Pool _pool;
	Group _group;
	Group _star;

	bool shouldAddTrail = false;

	public void SetPool(Pool pool) {
		_pool = pool;
        _group = _pool.GetGroup(Matcher.AllOf(Matcher.Background, Matcher.GameObject));
		_star = pool.GetGroup(Matcher.BackgroundStar);
	}

	public void Initialize() {
		_pool.GetGroup(Matcher.EventService).GetSingleEntity().eventService.dispatcher.AddListener<SpeedBonusEvent>(onSpeedBonusChange);
	}

	public void Execute() {
		for (int i = 0; i < _group.count; i++) {
			Entity entity = _group.GetSingleEntity();
			BackgroundComponent component = entity.background;
            setScale(component, entity.gameObject);

            if (component.starsCount > _star.count) {
				PositionComponent position = entity.position;
				spawnStar(component, position);
			}
		}
	}

    void setScale(BackgroundComponent component, GameObjectComponent gameObject) {
        gameObject.gameObject.transform.localScale = new Vector2(15, 25);
    }

    void spawnStar(BackgroundComponent component, PositionComponent position) {
		Entity e = _pool.CreateEntity()
			.AddVelocity(new Vector2(0.0f, -Random.Range(2.0f, 3.0f)))
			.AddPosition(new Vector2(Random.Range(position.pos.x + component.dimension.x / 2.0f, position.pos.x - component.dimension.x / 2.0f), 
			                         Random.Range(position.pos.y + component.dimension.y / 2.0f + GameConfig.CAMERA_START_OFFSET, position.pos.y - component.dimension.y / 2.0f + GameConfig.CAMERA_START_OFFSET)))
			.AddResource(Resource.Particle)
			.IsBackgroundStar(true)
			.AddSortOrder(SortTypes.Background)
			.AddAlpha(6.0f, 6.0f);
		if (shouldAddTrail) {
			e.AddGhost(0.0f, 0.1f, 0.4f);
		}
	}

	void onSpeedBonusChange(SpeedBonusEvent speedEvent) {
		if (speedEvent.enabled && !shouldAddTrail) {
			shouldAddTrail = true;
			foreach (Entity e in _star.GetEntities()) {
				e.AddGhost(0.0f, 0.1f, 0.4f);
			}
		}
		else if (!speedEvent.enabled && shouldAddTrail) {
			shouldAddTrail = false;
			foreach (Entity e in _star.GetEntities()) {
				e.RemoveGhost();
			}
		}
	}
}