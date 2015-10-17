using Entitas;

public class RelativePositionSystem : IExecuteSystem, ISetPool {
	Group _group;
	Group _time;
	
	public void SetPool(Pool pool) {
		_group = pool.GetGroup(Matcher.AllOf(Matcher.RelativePosition, Matcher.Child));
	}
	
	public void Execute() {
		foreach (Entity e in _group.GetEntities()) {
			RelativePositionComponent position = e.relativePosition;
			ChildComponent child = e.child;
			PositionComponent parentPosition = child.parent.position;

			e.ReplacePosition(position.x + parentPosition.x, position.y + parentPosition.y);
		}
	}
}
