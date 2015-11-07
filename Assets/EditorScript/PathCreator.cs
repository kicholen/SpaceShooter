using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class PathCreator : MonoBehaviour {

	List<GameObject> gameObjects;
	GameObject dragging;
	Color lineColor;

	void Start () {
		gameObjects = new List<GameObject>();
		lineColor = Color.red;
	}
	
	void Update () {
		if (Input.GetMouseButtonDown(0)) {
			RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
			if (hit.collider!=null) {
				dragging = hit.collider.gameObject;
				Debug.Log("hit");
			}
			else if (dragging != null) {
				Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				dragging.transform.position = new Vector3(position.x, position.y, 0.0f);
				dragging = null;
			}
			else {
				createNode();
			}
		}

		if (Input.GetKeyDown(KeyCode.S)) {
			saveNodes();
		}
	}

	void OnDrawGizmos() {
		Gizmos.color = lineColor;
		for (int i = 0; i < gameObjects.Count - 1; i++) {
			Gizmos.DrawLine(gameObjects[i].transform.position, gameObjects[i + 1].transform.position);
		}
	}

	void createNode() {
		GameObject go = Object.Instantiate(Resources.Load<GameObject>("EditorPrefab/point"));
		Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		go.transform.position = new Vector3(position.x, position.y, 0.0f);
		gameObjects.Add(go);
	}
		
	void onObjectClicked() {
		dragging = EventSystem.current.currentSelectedGameObject;
	}

	void saveNodes() {
		PathModelComponent component = new PathModelComponent();
		component.points = new List<Vector2>();
		for (int i = 0; i < gameObjects.Count; i++) {
			component.points.Add(gameObjects[i].transform.position);
		}
		Utils.SerializeComponent(component);
	}
}
