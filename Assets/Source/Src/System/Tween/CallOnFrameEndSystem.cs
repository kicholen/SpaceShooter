using Entitas;
using System;

public class CallOnFrameEndSystem : IExecuteSystem, ISetPool {
	Group _group;
	Group _time;
	
	public void SetPool(Pool pool) {
		_group = pool.GetGroup(Matcher.CallOnFrameEnd);
	}
	
	public void Execute() {
		foreach (Entity e in _group.GetEntities()) {
			Action<Entity> action = e.callOnFrameEnd.callback;
			e.RemoveCallOnFrameEnd();
			action.Invoke(e);
		}
	}
}
