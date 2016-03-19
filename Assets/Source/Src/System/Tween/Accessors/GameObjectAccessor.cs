using UnityEngine;
using Entitas;
using UnityEngine.UI;
using System;

public static class GameObjectAccessorType {
	public const int LOCAL_X = 1;
	public const int LOCAL_Y = 2;
	public const int LOCAL_XY = 3;
	public const int LOCAL_SCALE_X = 4;
	public const int LOCAL_SCALE_Y = 5;
	public const int LOCAL_SCALE_XY = 6;
	public const int TEXT = 7;
}

public class GameObjectAccessor : ITweenAccessor {
	
	public int GetValues(IComponent target, int tweenType, float[] returnValues) {
		GameObject go = (target as GameObjectComponent).gameObject;
		switch(tweenType) {
		case GameObjectAccessorType.LOCAL_X:
			returnValues[0] = go.transform.localPosition.x;
			return 1;
		case GameObjectAccessorType.LOCAL_Y:
			returnValues[0] = go.transform.localPosition.y;
			return 1;
		case GameObjectAccessorType.LOCAL_XY:
			returnValues[0] = go.transform.localPosition.x;
			returnValues[1] = go.transform.localPosition.y;
			return 2;
        case GameObjectAccessorType.LOCAL_SCALE_X:
            returnValues[0] = go.transform.localScale.x;
            return 1;
        case GameObjectAccessorType.LOCAL_SCALE_Y:
            returnValues[0] = go.transform.localScale.y;
            return 1;
        case GameObjectAccessorType.LOCAL_SCALE_XY:
            returnValues[0] = go.transform.localScale.x;
            returnValues[1] = go.transform.localScale.y;
            return 2;
        case GameObjectAccessorType.TEXT:
            returnValues[0] = Convert.ToInt32(go.GetComponent<Text>().text);
            return 1;
            default:
			return 0;
		}
	}
	
	public void SetValues(IComponent target, int tweenType, float[] newValues) {
		GameObject go = (target as GameObjectComponent).gameObject;
		switch(tweenType) {
		case GameObjectAccessorType.LOCAL_X:
			go.transform.localPosition = new Vector2(newValues[0], go.transform.localPosition.y);
			break;
		case GameObjectAccessorType.LOCAL_Y:
			go.transform.localPosition = new Vector2(go.transform.localPosition.x, newValues[0]);
			break;
		case GameObjectAccessorType.LOCAL_XY:
			go.transform.localPosition = new Vector2(newValues[0], newValues[1]);
			break;
        case GameObjectAccessorType.LOCAL_SCALE_X:
            go.transform.localScale = new Vector2(newValues[0], go.transform.localScale.y);
            break;
        case GameObjectAccessorType.LOCAL_SCALE_Y:
            go.transform.localScale = new Vector2(go.transform.localScale.x, newValues[0]);
            break;
        case GameObjectAccessorType.LOCAL_SCALE_XY:
            go.transform.localScale = new Vector2(newValues[0], newValues[1]);
            break;
        case GameObjectAccessorType.TEXT:
            go.GetComponent<Text>().text = newValues[0].ToString("0");
            break;
        default:
			break;
		}
	}
}