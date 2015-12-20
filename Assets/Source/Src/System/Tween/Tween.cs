using Entitas;
using System;

public class Tween {
	public static int TWEEN_ATTRIBUTES_LIMIT = 2;

	public Action<Entity> OnComplete;

	float time;
	float duration;
	float[] startValues;
	float[] targetValues;
	float[] bufferValues = new float[TWEEN_ATTRIBUTES_LIMIT];
	int ease;
	int tweenType;
	IComponent target;
	ITweenAccessor accessor;

	public void Setup(IComponent target, ITweenAccessor accessor, int ease, int tweenType, float duration) {
		this.target = target;
		this.accessor = accessor;
		this.ease = ease;
		this.tweenType = tweenType;
		this.duration = duration;
	}

	public void SetValues(float[] startValues, float[] targetValues) {
		this.startValues = startValues;
		this.targetValues = targetValues;
	}

	public void Update(float deltaTime) {
		time += deltaTime;

		if (time > duration) {
			for (int i = 0; i < TWEEN_ATTRIBUTES_LIMIT; i++) {
				accessor.SetValues(target, tweenType, targetValues);
			}
		}
		else {
			float t = linear(time / duration);
			for (int i = 0; i < TWEEN_ATTRIBUTES_LIMIT; i++) {
				bufferValues[i] = startValues[i] + t * (targetValues[i] - startValues[i]);
			}
			accessor.SetValues(target, tweenType, bufferValues);
		}
	}

	public bool HasEnded() {
		return time > duration;
	}

	void reset() {
		target = null;
		time = 0.0f;
	}
	
	float linear(float t) {
		return t;
	}
}