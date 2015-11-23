using Entitas;
using UnityEngine;

public class RotateSystem : IExecuteSystem, ISetPool {
	
	Group _group;
	Group _time;
	
	public void SetPool(Pool pool) {
		_group = pool.GetGroup(Matcher.AllOf(Matcher.Rotate, Matcher.GameObject));
		_time = pool.GetGroup(Matcher.Time);
	}
	
	public void Execute() {
		float deltaTime = _time.GetSingleEntity().time.gameDeltaTime;
		
		foreach (Entity e in _group.GetEntities()) {
			RotateComponent component = e.rotate;
			component.angle += component.rotateSpeed * deltaTime;
			Debug.Log(component.angle);
			e.gameObject.gameObject.transform.rotation = Quaternion.AngleAxis(component.angle, Vector3.forward);
		}
	}
}