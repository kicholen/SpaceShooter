using UnityEngine;
using System.Collections.Generic;
using System;

public class EditorGrid : MonoBehaviour {
	
	public Material LineMaterial;
	
	List<Line> lines;

	const int GRID_SIZE = 20;
	const float OPTIMAL_MAP_SIZE = 3.0f;
	const float EPSILON = 0.005f;

	void Start () {
		createLines();
		drawGrid();
		createReferencePoint();
	}
	
	void createLines () {
		lines = new List<Line>();

		// y axis
		for (int i = 0; i <= GRID_SIZE; i++) {
			Vector2 start = new Vector2(-(float)GRID_SIZE / 2.0f + (float)i, -(float)GRID_SIZE / 2.0f);
			Vector2 end = new Vector2(-(float)GRID_SIZE / 2.0f + (float)i, (float)GRID_SIZE / 2.0f);
			lines.Add(new Line(start, end));
		}

		// xaxis
		for (int i = 0; i <= GRID_SIZE; i++) {
			Vector2 start = new Vector2(-(float)GRID_SIZE / 2.0f, -(float)GRID_SIZE / 2.0f + (float)i);
			Vector2 end = new Vector2((float)GRID_SIZE / 2.0f, -(float)GRID_SIZE / 2.0f + (float)i);
			lines.Add(new Line(start, end));
		}
	}
	
	void drawGrid() {
		for (int i = 0; i < lines.Count; i++) {
			LineRenderer lineRenderer = createLineRenderer (Math.Abs(lines [i].start.x % 3) < EPSILON);
			lineRenderer.SetPosition(0, lines[i].start);
			lineRenderer.SetPosition(1, lines[i].end);
		}
	}

	void createReferencePoint() {
		GameObject go = Instantiate(Resources.Load<GameObject>("EditorPrefab/ReferencePoint"));
		go.transform.position = new Vector3(0.0f, 0.0f, 0.0f);
	}

	LineRenderer createLineRenderer(bool shouldExcel) {
		LineRenderer lineRenderer = new GameObject().AddComponent<LineRenderer>();
		lineRenderer.material = LineMaterial;
		lineRenderer.SetWidth(0.05f, 0.05f);
		Color color = shouldExcel?Color.blue:Color.black;
		lineRenderer.SetColors(color, color);
		lineRenderer.SetVertexCount(2);
		return lineRenderer;
	}
}

internal class Line {
	public Vector2 start;
	public Vector2 end;

	public Line(Vector2 start, Vector2 end) {
		this.start = start;
		this.end = end;
	}
}