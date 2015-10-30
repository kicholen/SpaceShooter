using Entitas;

public class RelativePositionSystem : IExecuteSystem, ISetPool {
	Group _group;
	Group _time;
	
	public void SetPool(Pool pool) {
		_group = pool.GetGroup(Matcher.AllOf(Matcher.RelativePosition, Matcher.Child));
	}
	
	public void Execute() {
		foreach (Entity e in _group.GetEntities()) {
			RelativePositionComponent relativePosition = e.relativePosition;
			PositionComponent position = e.position;
			PositionComponent parentPosition = e.child.parent.position;

			position.x = relativePosition.x + parentPosition.x;
			position.y = relativePosition.y + parentPosition.y;
		}
	}
}
