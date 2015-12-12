using Entitas;
using System.Collections.Generic;
using UnityEngine;

public class CameraShakeOnDeathSystem : IReactiveSystem, ISetPool {
	public TriggerOnEvent trigger { get { return Matcher.AllOf(Matcher.CameraShakeOnDeath, Matcher.CollisionDeath).OnEntityAdded(); } }
	
	Group _group;
	
	public void SetPool(Pool pool) {
		_group = pool.GetGroup(Matcher.Camera);
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
		Entity entity = _group.GetSingleEntity();
		if (!entity.hasShake) {
			entity.AddShake(new ShakeProperties());
		}
	}
}