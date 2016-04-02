using Entitas;
using System;

public class CallOnFrameEndSystem : IExecuteSystem, ISetPool {
	Group group;
	
	public void SetPool(Pool pool) {
		group = pool.GetGroup(Matcher.CallOnFrameEnd);
	}
	
	public void Execute() {
		foreach (Entity e in group.GetEntities()) {
			Action<Entity> action = e.callOnFrameEnd.callback;
			e.RemoveCallOnFrameEnd();
			action.Invoke(e);
		}
	}
}
