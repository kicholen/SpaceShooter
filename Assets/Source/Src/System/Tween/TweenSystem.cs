using Entitas;

public class TweenSystem : IExecuteSystem, ISetPool {
	Pool _pool;
	Group _group;
	Group _time;

	public void SetPool(Pool pool) {
		_pool = pool;
		_time = pool.GetGroup(Matcher.Time);
		_group = pool.GetGroup(Matcher.AllOf(Matcher.TweenPosition, Matcher.Position));
	}
	
	public void Execute() {
		float deltaTime = _time.GetSingleEntity().time.deltaTime;

		foreach (Entity e in _group.GetEntities()) {
			tweenPosition(e, deltaTime);
		}
	}

	void tweenPosition(Entity e, float deltaTime) {
		TweenPositionComponent tween = e.tweenPosition;
		tween.time += deltaTime;
		
		if (tween.time > tween.duration) {
			e.position.pos.Set(tween.toVector.x, tween.toVector.y);
			if (tween.onUpdate != null) {
				tween.onUpdate(e);
			}
			e.AddCallOnFrameEnd((ent) => {
				ent.RemoveTweenPosition();
				tween.onComplete(ent);
			});
		}
		else {
			float x = ease(tween.ease, tween.time, tween.fromVector.x, tween.toVector.x, tween.duration);
			float y = ease(tween.ease, tween.time, tween.fromVector.y, tween.toVector.y, tween.duration);
			e.position.pos.Set(x, y);
			if (tween.onUpdate != null) {
				tween.onUpdate(e);
			}
		}
	}

	float ease(int type, float time, float from, float to, float duration) {
		float value = 0.0f;
		switch(type) {
			case EaseTypes.Linear:
				value = easeLinear(time, from, to, duration);
			break;
		}
		return value;
	}

	float easeLinear(float time, float from, float to, float duration) {
		return (to - from) * time / duration + from;
	}
}
