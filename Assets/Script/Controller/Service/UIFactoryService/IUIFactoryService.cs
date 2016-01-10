using UnityEngine;
using UnityEngine.Events;

public interface IUIFactoryService
{
	GameObject CreatePrefab(string path);
	void AddButton(Transform transform, string path, UnityAction action);
	void AddButton(GameObject go, UnityAction action);
	void AddText(Transform transform, string path, string lockit);
	Vector2 Dimensions { get; }
}

