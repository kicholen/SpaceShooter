using Entitas;
using System;

public class Tween {
	public static int TWEEN_ATTRIBUTES_LIMIT = 2;

	public Action<Entity> OnComplete;

	float time;
	float duration;
	int valuesCount;
	float[] startValues = new float[TWEEN_ATTRIBUTES_LIMIT];
	float[] targetValues = new float[TWEEN_ATTRIBUTES_LIMIT];
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

	public Tween From(float firstValue, float secondValue) {
		valuesCount = 2;
		this.startValues[0] = firstValue;
		this.startValues[1] = secondValue;
		return this;
	}

	public Tween From(float value) {
		valuesCount = 1;
		this.startValues[0] = value;
		return this;
	}

	public Tween To(float firstValue, float secondValue) {
		this.targetValues[0] = firstValue;
		this.targetValues[1] = secondValue;
		return this;
	}
	
	public Tween To(float value) {
		this.targetValues[0] = value;
		return this;
	}

	public Tween SetEndCallback(Action<Entity> callback) {
		OnComplete = callback; 
		return this;
	}

	public void Update(float deltaTime) {
		time += deltaTime;

		if (time > duration) {
			for (int i = 0; i < valuesCount; i++) {
				accessor.SetValues(target, tweenType, targetValues);
			}
		}
		else {
			float t = linear(time / duration);
			for (int i = 0; i < valuesCount; i++) {
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