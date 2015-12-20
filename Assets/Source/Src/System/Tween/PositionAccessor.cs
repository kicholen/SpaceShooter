using UnityEngine;
using Entitas;

public class PositionAccessor : ITweenAccessor {
	public const int X = 1;
	public const int Y = 2;
	public const int XY = 3;

	public int GetValues(IComponent target, int tweenType, float[] returnValues) {
		PositionComponent targetPos = (PositionComponent)target;
		switch(tweenType) {
		case X:
			returnValues[0] = targetPos.pos.x;
			return 1;
		case Y:
			returnValues[0] = targetPos.pos.y;
			return 1;
		case XY:
			returnValues[0] = targetPos.pos.x;
			returnValues[1] = targetPos.pos.y;
			return 2;
		default:
			Debug.Log("Unexpected tweenType in PositionAccessor: " + tweenType);
			return 0;
		}
	}

	public void SetValues(IComponent target, int tweenType, float[] newValues) {
		PositionComponent targetPos = (PositionComponent)target;
		switch(tweenType) {
		case X:
			targetPos.pos.x = newValues[0];
			break;
		case Y:
			targetPos.pos.y = newValues[0];
			break;
		case XY:
			targetPos.pos.x = newValues[0];
			targetPos.pos.y = newValues[1];
			break;
		default:
			Debug.Log("Unexpected tweenType in PositionAccessor: " + tweenType);
			break;
		}
	}
}