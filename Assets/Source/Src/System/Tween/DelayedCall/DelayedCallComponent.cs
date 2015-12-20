using Entitas;
using System;
using UnityEngine;

public class DelayedCallComponent : IComponent {
	public float duration;
	public Action<Entity> onComplete;
}