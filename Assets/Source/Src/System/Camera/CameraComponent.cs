using Entitas;
using UnityEngine;

public class CameraComponent : IComponent {
	public Camera camera;
	public Entity follow; // todo move to followTargetComponent
}