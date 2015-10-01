using Entitas;
using UnityEngine;
using System.Collections.Generic;
using System.Xml;
using System;

public class SnapPositionSystem : IExecuteSystem, ISetPool {
	Group _group;
	
	public void SetPool(Pool pool) {
		_group = pool.GetGroup(Matcher.AllOf(Matcher.SnapPosition, Matcher.Position));
	}
	
	public void Execute() {
		Debug.Log("SnapPositionSystem");

		foreach (Entity e in _group.GetEntities()) {
			PositionComponent position = e.position;
			SnapPositionComponent snapPosition = e.snapPosition;
			
			if (position.x < snapPosition.x) {
				position.x = snapPosition.x;
			}
			else if (position.x > (snapPosition.x + snapPosition.width)) {
				position.x = snapPosition.x + snapPosition.width;
			}
			
			if (position.y > (snapPosition.height + snapPosition.y)) {
				position.y = snapPosition.height + snapPosition.y;
			}
			else if (position.y < snapPosition.y) {
				position.y = snapPosition.y;
			}
		}
	}
}