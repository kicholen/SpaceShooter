using Entitas;
using System;
using UnityEngine;

public class TweenPositionComponent : IComponent {
	public float time;
	public float duration;
	public int ease;
	public Vector2 fromVector;
	public Vector2 toVector;
	public bool isInGame;
	public Action<Entity> onComplete;
	public Action<Entity> onUpdate;
}