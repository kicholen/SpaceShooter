using Entitas;
using System;
using System.Collections.Generic;

public class TweenComponent : IComponent {
	public bool isInGame;
	public List<Tween> tweens;

	public Tween AddTween(IComponent target, Func<float, float> ease, int tweenType, float duration) {
		Tween tween = new Tween();
		tween.Setup(target, CreateAccessor(target), ease, tweenType, duration);
		tweens.Add(tween);
		return tween;
	}

	public void RemoveTween(Tween tween) {
		tweens.Remove(tween);
	}

    public void Clear()
    {
        tweens.Clear();
    }

    ITweenAccessor CreateAccessor(IComponent component) {
		switch(component.GetType().ToString()) {
		case "PositionComponent":
			return new PositionAccessor();
		case "GameObjectComponent":
			return new GameObjectAccessor();
        case "LaserSpawnerComponent":
            return new LaserSpawnerAccessor();
            default:
			throw new Exception("Component " + component.GetType().ToString() + " doesn't have accessor");
		}
	}
}