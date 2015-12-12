using Entitas;
using UnityEngine;

public class GhostSystem : IExecuteSystem, ISetPool {
	Pool _pool;
	Group _group;
	Group _time;

	const string COLLIDERS_FOLDER_NAME = "Collider/";

	public void SetPool(Pool pool) {
		_pool = pool;
		_time = pool.GetGroup(Matcher.Time);
		_group = pool.GetGroup(Matcher.AllOf(Matcher.Ghost, Matcher.Position, Matcher.Velocity, Matcher.Resource));
	}
	
	public void Execute() {
		float deltaTime = _time.GetSingleEntity().time.gameDeltaTime;
		foreach (Entity e in _group.GetEntities()) {
			GhostComponent ghost = e.ghost;

			ghost.currentTime -= deltaTime;

			if (ghost.currentTime < 0.0f) {
				ghost.currentTime = ghost.spawnTime;
				ResourceComponent resource = e.resource;
				PositionComponent position = e.position;

				_pool.CreateEntity()
					.AddResource(getResourcePathWithoutColliders(resource.name))
					.AddSortOrder(SortTypes.Effect)
					.AddAlpha(ghost.duration, ghost.duration)
					.AddPosition(new Vector2(position.pos.x, position.pos.y))
					.AddDestroyEntityDelayed(ghost.duration);
			}
		}
	}

	string getResourcePathWithoutColliders(string resource) {
		return resource.StartsWith(COLLIDERS_FOLDER_NAME, System.StringComparison.Ordinal) ? resource.Replace (COLLIDERS_FOLDER_NAME, "") : resource;
	}
}