using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

public class UIFactoryService : IUIFactoryService
{
	const string UI_PATH = "Prefab/UI/";

	public GameObject CreatePrefab(string path) {
		GameObject go = null;
		GameObject res = Resources.Load<GameObject>(UI_PATH + path);
		try {
			go = UnityEngine.Object.Instantiate(res);
		}
		catch (Exception) {
			Debug.Log("Cannot instantiate " + path);
		}

		return go;
	}

	public void AddButton(Transform transform, string path, UnityAction action) {
		GameObject go = transform.FindChild(path).gameObject;
		Button button = go.GetComponent<Button>();
		if (button == null) {
			button = go.AddComponent<Button>();
		}
		button.onClick.AddListener(action);
	}
}

