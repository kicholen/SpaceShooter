using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class PathCreator : MonoBehaviour {

	public Material LineMaterial;

	List<GameObject> gameObjects;
	GameObject dragging;
	LineRenderer lineRenderer;
	Vector3 currentPosition;

	Stack<ChangeAction> changes = new Stack<ChangeAction>();
	Stack<ChangeAction> backChanges = new Stack<ChangeAction>();

	void Start () {
		gameObject.tag = "PathCreator";
		gameObjects = new List<GameObject>();
		lineRenderer = new GameObject().AddComponent<LineRenderer>();
		lineRenderer.material = LineMaterial;
		lineRenderer.SetWidth(0.05f, 0.05f);
		lineRenderer.SetColors(Color.blue, Color.blue);
	}

	void Update () {
		if (EditorController.instance.blockTouch) {
			return;
		}
		if (Input.GetMouseButtonDown(0)) {
			RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
			if (hit.collider!=null) {
				dragging = hit.collider.gameObject;
				Debug.Log("hit");
			}
			else if (dragging != null) {
				ChangeAction moveAction = new ChangeAction(dragging, gameObjects.IndexOf(dragging), ChangeState.MOVE);
				changes.Push(moveAction);
				Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				dragging.transform.position = new Vector3(position.x, position.y, 0.0f);
				dragging = null;
			}
			else {
				currentPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				createNode();
			}
		}

		if (dragging != null && Input.GetKeyDown(KeyCode.Delete)) {
			GameObject go = dragging;
			go.SetActive(false);
			changes.Push(new ChangeAction(dragging, gameObjects.IndexOf(go), ChangeState.REMOVE));
			gameObjects.Remove(go);
			dragging = null;
			backChanges.Clear();
		}
		if (Input.GetKeyDown(KeyCode.O)) {
			saveNodes();
		}

		if (Input.GetKeyDown(KeyCode.Y)) {
			nextState();
		}
		else if (Input.GetKeyDown(KeyCode.Z)) {
			previousState();
		}

		drawLines();
	}

	void drawLines() {
		lineRenderer.SetVertexCount(gameObjects.Count);
		for (int i = 0; i < gameObjects.Count; i++) {
			lineRenderer.SetPosition(i, gameObjects[i].transform.position);
		}
	}

	void createNode() {
		GameObject go = Object.Instantiate(Resources.Load<GameObject>("EditorPrefab/point"));
		go.transform.position = currentPosition;
		gameObjects.Add(go);
		changes.Push(new ChangeAction(go, gameObjects.IndexOf(go), ChangeState.ADD));
		backChanges.Clear();
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
		Utils.SerializeComponent(component, EditorController.instance.sufix);
	}

	void nextState() {
		if (backChanges.Count == 0) {
			return;
		}
		ChangeAction action = backChanges.Pop();
		switch (action.state) {
		case ChangeState.REMOVE:
			gameObjects.RemoveAt(action.listPosition);
			action.go.SetActive(false);
			break;
		case ChangeState.ADD:
			if (gameObjects.Count < action.listPosition) {
				gameObjects.Insert(action.listPosition, action.go);
			}
			else {
				gameObjects.Add(action.go);
			}
			action.listPosition = gameObjects.IndexOf(action.go);
			action.go.SetActive(true);
			break;
		case ChangeState.MOVE:
			action.go.transform.position = new Vector3(action.previousPosition.x, action.previousPosition.y, action.previousPosition.z);
			//action.go.transform.position = new Vector3(action.previousPosition.x, action.previousPosition.y, action.previousPosition.z);
			break;
		}
		changes.Push(action);
	}

	void previousState() {
		if (changes.Count == 0) {
			return;
		}
		ChangeAction action = changes.Pop();
		switch (action.state) {
		case ChangeState.REMOVE:
			gameObjects.Insert(action.listPosition, action.go);
			action.go.SetActive(true);
			break;
		case ChangeState.ADD:
			gameObjects.RemoveAt(action.listPosition);
			action.go.SetActive(false);
			break;
		case ChangeState.MOVE:
			action.previousPosition = new Vector3(action.go.transform.position.x, action.go.transform.position.y, action.go.transform.position.z);
			action.go.transform.position = new Vector3(action.position.x, action.position.y, action.position.z);
			break;
		}
		backChanges.Push(action);
	}

	public void SetGameObjectsFromLoadedOne() {
		PathModelComponent component = EditorController.instance.component;
		if (component != null) {
			foreach (Vector2 position in component.points) {
				currentPosition = new Vector3(position.x, position.y, 0.0f);
				createNode();
			}
		}
	}
}

internal class ChangeAction {
	public GameObject go;
	public Vector3 position;
	public Vector3 previousPosition;
	public int state;
	public int listPosition;

	public ChangeAction(GameObject go, int listPosition, int state) {
		this.go = go;
		position = new Vector3(go.transform.position.x, go.transform.position.y, go.transform.position.z);
		this.listPosition = listPosition;
		this.state = state;
	}
}

