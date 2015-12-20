using Entitas;
using System.Collections.Generic;
using System;

public class TweenSystem : IExecuteSystem, IInitializeSystem, ISetPool {
	Group _group;
	Group _time;

	public void Initialize() {
		TweenComponent.RegisterAccessor(typeof(PositionComponent), typeof(PositionAccessor));
	}

	public void SetPool(Pool pool) {
		_time = pool.GetGroup(Matcher.Time);
		_group = pool.GetGroup(Matcher.Tween);
	}
	
	public void Execute() {
		TimeComponent time = _time.GetSingleEntity().time;
		float gameDeltaTime = time.gameDeltaTime;
		float deltaTime = time.deltaTime;

		foreach (Entity e in _group.GetEntities()) {
			TweenComponent tweenComponent = e.tween;
			update(e, tweenComponent, tweenComponent.isInGame ? gameDeltaTime : deltaTime);
		}
	}

	void update(Entity e, TweenComponent tweenComponent, float deltaTime) {
		Dictionary<Type, Tween> tweens = tweenComponent.tweens;
		foreach (Tween tween in tweens.Values) {
			tween.Update(deltaTime);
			if (tween.HasEnded()) {
				onTweenEnded(e, tweenComponent, tween);
			}
		}
	}

	void onTweenEnded(Entity e, TweenComponent tweenComponent, Tween tween) {
		e.AddCallOnFrameEnd((ent) => {
			if (tween.OnComplete != null) {
				tween.OnComplete(ent);
			}
			tweenComponent.RemoveTween(tween);
		});
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
