using UnityEngine;

public class EditorController : MonoBehaviour {

	public bool blockTouch = true;
	public PathModelComponent component;
	public string sufix;
	PathCreator pathCreator;

	static EditorController controller;
	public static EditorController instance { 
		get {
			if (controller == null) {
				controller = new GameObject().AddComponent<EditorController>();
				controller.init();
			}
			return controller;
		}
	}

	public void init() {
		blockTouch = true;
	}

	public void SetPathCreator() {
		pathCreator = GameObject.FindGameObjectsWithTag("PathCreator")[0].GetComponent<PathCreator>();
		pathCreator.SetGameObjectsFromLoadedOne();
	}
}