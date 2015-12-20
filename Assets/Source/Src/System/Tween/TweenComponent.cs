using Entitas;
using System;
using System.Collections.Generic;

public class TweenComponent : IComponent {
	static Dictionary<Type, Type> registeredAccessor;
	public bool isInGame;
	public Dictionary<Type, Tween> tweens;

	public void AddTween(IComponent target, int ease, int tweenType, float duration, float[] startValues, float[] endValues) {
		Tween tween = new Tween();
		tween.Setup(target, GetAccessor(target), ease, tweenType, duration);
		tween.SetValues(startValues, endValues);

		tweens.Add(target.GetType(), tween);
	}

	public void RemoveTween(Tween tween) {
		tweens.Remove(tween.GetType());
	}

	ITweenAccessor GetAccessor(IComponent component) {
		return (ITweenAccessor)Activator.CreateInstance(registeredAccessor[component.GetType()]);
	}

	public static void RegisterAccessor(Type componentType, Type accessorType) {
		if (registeredAccessor == null) {
			registeredAccessor = new Dictionary<Type, Type>();
		}
		registeredAccessor.Add(componentType, accessorType);
	}
}