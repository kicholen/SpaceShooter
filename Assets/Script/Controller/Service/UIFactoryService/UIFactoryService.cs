using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

public class UIFactoryService : IUIFactoryService
{
	const string UI_PATH = "Prefab/UI/";
	const string SPRITE_PATH = "Sprite/";
    
    Vector2 dimensions = new Vector2(-1.0f, -1.0f);

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

    public Sprite CreateSprite(string path)
    {
        Sprite sprite = null;
        try
        {
            sprite = Resources.Load<Sprite>(SPRITE_PATH + path);
        }
        catch (Exception)
        {
            Debug.Log("Cannot instantiate sprite " + path);
        }

        return sprite;
    }

    public void AddButton(Transform transform, string path, UnityAction action) {
		GameObject go = transform.FindChild(path).gameObject;
		Button button = go.GetComponent<Button>();
		if (button == null) {
			button = go.AddComponent<Button>();
		}
		button.onClick.AddListener(action);
	}

	public void AddButton(GameObject go, UnityAction action) {
		Button button = go.GetComponent<Button>();
		if (button == null) {
			button = go.AddComponent<Button>();
		}
		button.onClick.AddListener(action);
	}

	public void AddText(Transform transform, string path, string lockit) {
		GameObject go = transform.FindChild(path).gameObject;
		Text text = go.GetComponent<Text>();
		if (text == null) {
			text = go.AddComponent<Text>();
		}
		text.text = lockit;
	}

    public Vector2 Dimensions {
		get {
			if (dimensions.x < 0.0f) {
				Camera camera = Camera.main;
				float size = camera.orthographicSize * 2.0f;
				float aspect = camera.aspect;
				if (aspect < 1.0f) {
					dimensions.Set(size * aspect, size);
				}
				else {
					dimensions.Set(size, size * aspect);
				}
			}
			return dimensions;
		}
	}
}

