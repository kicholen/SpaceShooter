using Entitas;
using System;

public class TweenComponent : IComponent {
	public float time;
	public float duration;
	public int ease;
	public float from;
	public float to;
	public float current;
	public bool hasCompleted;
	public Action<Entity, float> onUpdate;
	public Action<Entity> onComplete;
}