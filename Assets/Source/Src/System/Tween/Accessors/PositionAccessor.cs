using Entitas;

public static class PositionAccessorType {
	public const int X = 1;
	public const int Y = 2;
	public const int XY = 3;
}

public class PositionAccessor : ITweenAccessor {

	public int GetValues(IComponent target, int tweenType, float[] returnValues) {
		PositionComponent targetPos = (PositionComponent)target;
		switch(tweenType) {
		case PositionAccessorType.X:
			returnValues[0] = targetPos.pos.x;
			return 1;
		case PositionAccessorType.Y:
			returnValues[0] = targetPos.pos.y;
			return 1;
		case PositionAccessorType.XY:
			returnValues[0] = targetPos.pos.x;
			returnValues[1] = targetPos.pos.y;
			return 2;
		default:
			return 0;
		}
	}

	public void SetValues(IComponent target, int tweenType, float[] newValues) {
		PositionComponent targetPos = (PositionComponent)target;
		switch(tweenType) {
		case PositionAccessorType.X:
			targetPos.pos.x = newValues[0];
			break;
		case PositionAccessorType.Y:
			targetPos.pos.y = newValues[0];
			break;
		case PositionAccessorType.XY:
			targetPos.pos.x = newValues[0];
			targetPos.pos.y = newValues[1];
			break;
		default:
			break;
		}
	}
}