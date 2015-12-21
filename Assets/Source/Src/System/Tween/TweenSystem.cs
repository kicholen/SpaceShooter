using Entitas;
using System.Collections.Generic;
using System;

public class TweenSystem : IExecuteSystem, ISetPool {
	Group _group;
	Group _time;

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
		List<Tween> tweens = tweenComponent.tweens;
		for (int i = 0; i < tweens.Count; i++) {
			Tween tween = tweens[i];
			tween.Update(deltaTime);
			if (tween.HasEnded()) {
				onTweenEnded(e, tweenComponent, tween);
			}
		}
	}

	void onTweenEnded(Entity e, TweenComponent tweenComponent, Tween tween) {
		e.AddCallOnFrameEnd((ent) => {
			tweenComponent.RemoveTween(tween);
			if (tweenComponent.tweens.Count == 0)
				e.RemoveTween();
			if (tween.OnComplete != null)
				tween.OnComplete(ent);
		});
	}
}
