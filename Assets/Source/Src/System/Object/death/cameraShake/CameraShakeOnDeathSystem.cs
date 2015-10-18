using Entitas;
using System.Collections.Generic;
using UnityEngine;

public class CameraShakeOnDeathSystem : IReactiveSystem, ISetPool {
	public TriggerOnEvent trigger { get { return Matcher.AllOf(Matcher.CameraShakeOnDeath, Matcher.CollisionDeath).OnEntityAdded(); } }
	
	Pool _pool;
	Group _group;
	
	public void SetPool(Pool pool) {
		_pool = pool;
		//_group = pool.GetGroup(Matcher.BonusModel);
	}
	
	public void Execute(List<Entity> entities) {
		foreach (Entity e in entities) {
			CameraShakeOnDeathComponent component = e.cameraShakeOnDeath;
			if (component.type == 1) {
				shakeCamera();
			}
			else {
				throw new UnityException("CameraShakeOnDeathSystem: unknown type");
			}
		}
	}
	
	void shakeCamera() {
		_pool.CreateEntity()
			.AddCameraShake(new CameraShakeProperties());
	}
}