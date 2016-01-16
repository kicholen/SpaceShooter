using UnityEngine;

public class DebugPathBehaviour : MonoBehaviour {
    Material material;

    LineRenderer lineRenderer;
    int currentPath = -1;

    public void Init(Material material) {
        this.material = material;
        lineRenderer = gameObject.AddComponent<LineRenderer>();
    }

    void Update() {
        updatePathIfChanged();
    }

    void OnDestroy() {
        Destroy(lineRenderer);
    }

    void updatePathIfChanged() {
        EditableBehaviour behaviour = GetComponent<EditableBehaviour>();
        int path = currentPath;
        float offsetY = 0.0f;

        if (behaviour.enemyModel == null) {
            path = behaviour.waveModel.path;
            offsetY = behaviour.waveModel.spawnBarrier;
        }
        else {
            path = behaviour.enemyModel.path;
            offsetY = behaviour.enemyModel.posY;
        }

        if (path != currentPath) {
            updateLineRenderer(path, offsetY);
        }
    }

    void updateLineRenderer(int path, float offsetY) {
        PathModelComponent component = EditLevelView.pathService.TryToGetPath(path.ToString());
        if (component != null) {
            if (lineRenderer == null) {
                lineRenderer = createLineRenderer();
            }
            lineRenderer.SetVertexCount(component.points.Count);
            for (int i = 0; i < component.points.Count; i++) {
                lineRenderer.SetPosition(i, new Vector2(component.points[i].x, component.points[i].y + offsetY));
            }
        }
    }

    LineRenderer createLineRenderer() {
        LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.material = material;
        lineRenderer.SetWidth(0.05f, 0.05f);
        lineRenderer.SetColors(Color.yellow, Color.yellow);

        return lineRenderer;
    }
}