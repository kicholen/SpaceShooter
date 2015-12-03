using Entitas;

public class TweenSystem : IExecuteSystem, ISetPool {
	Group _group;
	Group _time;

	public void SetPool(Pool pool) {
		_time = pool.GetGroup(Matcher.Time);
		_group = pool.GetGroup(Matcher.AllOf(Matcher.TweenPosition, Matcher.Position));
	}
	
	public void Execute() {
		TimeComponent time = _time.GetSingleEntity().time;
		float deltaTime = time.deltaTime;
		float gameDeltaTime = time.gameDeltaTime;

		foreach (Entity e in _group.GetEntities()) {
			tweenPosition(e, deltaTime, gameDeltaTime);
		}
	}

	void tweenPosition(Entity e, float deltaTime, float gameDeltaTime) {
		TweenPositionComponent tween = e.tweenPosition;
		if (tween.isInGame) {
			tween.time += gameDeltaTime;
		}
		else {
			tween.time += deltaTime;
		}
		
		if (tween.time > tween.duration) {
			e.position.pos.Set(tween.toVector.x, tween.toVector.y);
			if (tween.onUpdate != null) {
				tween.onUpdate(e);
			}
			e.AddCallOnFrameEnd((ent) => {
				ent.RemoveTweenPosition();
				if (tween.onComplete != null) {
					tween.onComplete(ent);
				}
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
