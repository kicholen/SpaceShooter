using UnityEngine;

public static class Vector2Extension
{
	public static Vector2 Set(this Vector2 baseVector, Vector2 vector) {
		baseVector.x = vector.x;
		baseVector.y = vector.y;
		return baseVector;
	}
}