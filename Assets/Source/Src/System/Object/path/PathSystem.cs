using Entitas;
using System.Collections.Generic;
using UnityEngine;

public class PathSystem : IExecuteSystem, ISetPool {

	Pool _pool;
	Group _time;
	Group _group;
	
	const float MIN_DISTANCE = 0.8f;
	const float STEERING = 12.0f;
	
	public void SetPool(Pool pool) {
		_pool = pool;
		_time = _pool.GetGroup(Matcher.Time);
		_group = pool.GetGroup(Matcher.AllOf(Matcher.Path, Matcher.GameObject, Matcher.Velocity, Matcher.VelocityLimit, Matcher.Position));
	}
	
	public void Execute() {
		float deltaTime = _time.GetSingleEntity().time.gameDeltaTime;
		foreach (Entity e in _group.GetEntities()) {
			PathComponent path = e.path;
			List<Vector2> points = path.path.points;
			if (path.node != points.Count) {
				PositionComponent position = e.position;
				Vector2 desiredPosition = points[path.node] + new Vector2(0.0f, path.startY + (path.duration * 1.0f));
				if (path.node == 0) {
					position.pos.Set(desiredPosition.x, desiredPosition.y);
					path.node += 1;
				}
				else {
					Vector2 currentPosition = position.pos;
					path.duration += deltaTime;
					float speed = e.velocityLimit.maxVelocity;

					VelocityComponent velocity = e.velocity;
					Vector2 steering = new Vector2((desiredPosition.x - currentPosition.x), (desiredPosition.y - currentPosition.y));
					steering.Normalize();
					steering *= speed / STEERING;
					velocity.vel += steering;
					velocity.vel.Normalize();
					velocity.vel *= speed;
					//drawDebugCircle(e.gameObject.gameObject, points, path.startY + (path.duration * 1.0f));
					
					if (distance(currentPosition, desiredPosition) <= MIN_DISTANCE) {
						path.node = path.node + 1;
					}
				}
			}
			else {
				e.RemovePath();
			}
		}
	}

	float distance(Vector2 current, Vector2 desired) {
		return Mathf.Sqrt((current.x - desired.x) * (current.x - desired.x) + (current.y - desired.y) * (current.y - desired.y));
	}

	//usage: drawDebugCircle(e.gameObject.gameObject, points, path.startY + (path.duration * 1.0f));
	void drawDebugCircle(GameObject go, List<Vector2> points, float startY) {
		Group _materials = _pool.GetGroup(Matcher.MaterialReference);

		float ThetaScale = 0.01f;
		int Size;
		float Theta = 0f;
		LineRenderer[] lineDrawers = go.GetComponentsInChildren<LineRenderer>();
		if (lineDrawers.Length == 0) {
			for (int j = 0; j < points.Count; j++) {
				GameObject goChild = new GameObject();
				goChild.transform.SetParent(go.transform, false);
				LineRenderer lineDrawer = goChild.AddComponent<LineRenderer>();
				lineDrawer.material = _materials.GetSingleEntity().materialReference.storage.Default;
				lineDrawer.SetWidth(0.05f, 0.05f);
				lineDrawer.SetColors(Color.red, Color.red);
			}

			lineDrawers = go.GetComponentsInChildren<LineRenderer>();
		}

		for (int j = 0; j < points.Count; j++) {
			LineRenderer lineDrawer = lineDrawers[j];
			Theta = 0f;
			Size = (int)((1f / ThetaScale) + 1f);
			lineDrawer.SetVertexCount(Size);
			for(int i = 0; i < Size; i++) {
				Theta += (2.0f * Mathf.PI * ThetaScale);
				float x = MIN_DISTANCE * Mathf.Cos(Theta) + points[j].x;
				float y = MIN_DISTANCE * Mathf.Sin(Theta) + points[j].y + startY;
				lineDrawer.SetPosition(i, new Vector3(x, y, 0));
			}
		}
	}
}