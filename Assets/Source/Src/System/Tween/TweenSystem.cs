using Entitas;

public class TweenSystem : IExecuteSystem, ISetPool {
	Group _group;
	Group _time;

	public void SetPool(Pool pool) {
		_time = pool.GetGroup(Matcher.Time);
		_group = pool.GetGroup(Matcher.Tween);
	}
	
	public void Execute() {
		float deltaTime = _time.GetSingleEntity().time.deltaTime;

		foreach (Entity e in _group.GetEntities()) {
			TweenComponent tween = e.tween;
			if (!tween.hasCompleted) {
				tween.time += deltaTime;
				tween.current = ease(tween.ease, tween.time, tween.from, tween.to, tween.duration);
				
				if (tween.time > tween.duration) {
					if (tween.onUpdate != null) {
						tween.onUpdate(e, tween.to);
					}
					tween.onComplete(e);
				}
				else if (tween.onUpdate != null) {
					tween.onUpdate(e, tween.current);
				}
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
