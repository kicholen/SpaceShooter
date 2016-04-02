using Entitas;
using UnityEngine;

public class RelativePositionSystem : IExecuteSystem, ISetPool {
	Group group;
	
	public void SetPool(Pool pool) {
		group = pool.GetGroup(Matcher.AllOf(Matcher.RelativePosition, Matcher.Child));
	}
	
	public void Execute() {
		foreach (Entity e in group.GetEntities()) {
			RelativePositionComponent relativePosition = e.relativePosition;
			Vector2 parentPosition = e.child.parent.position.pos;

			e.position.pos.Set(relativePosition.x + parentPosition.x, relativePosition.y + parentPosition.y);
		}
	}
}
