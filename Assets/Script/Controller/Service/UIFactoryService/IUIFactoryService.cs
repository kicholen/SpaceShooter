using UnityEngine;
using UnityEngine.Events;

public interface IUIFactoryService
{
	GameObject CreatePrefab(string path);
	void AddButton(Transform transform, string path, UnityAction action);
}

