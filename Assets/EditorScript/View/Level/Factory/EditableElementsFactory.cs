using UnityEngine;

public class EditableElementsFactory {
    bool shouldAddDebugPath = false;

    public GameObject CreateWaveElement(WaveModel model) {
        GameObject go = Object.Instantiate(Resources.Load<GameObject>("Prefab/UI/EditorView/Level/WaveElement"));
        go.AddComponent<EditableBehaviour>().SetWaveModel(model);
        addDebugPathIfShould(go);
        return go;
    }

    public GameObject CreateEnemyElement(EnemyModel model) {
        GameObject go = Object.Instantiate(Resources.Load<GameObject>("Prefab/UI/EditorView/Level/WaveElement"));
        go.AddComponent<EditableBehaviour>().SetEnemyModel(model);
        addDebugPathIfShould(go);
        return go;
    }

    void addDebugPathIfShould(GameObject go) {
        if (shouldAddDebugPath) {
            go.AddComponent<DebugPathBehaviour>();
        }
    }

    public void addDebugPathBehavioursIfNotExists() {
        shouldAddDebugPath = true;
        foreach (EditableBehaviour behaviour in UnityEngine.Object.FindObjectsOfType<EditableBehaviour>()) {
            DebugPathBehaviour debugPath = behaviour.GetComponent<DebugPathBehaviour>();
            if (debugPath == null) {
                behaviour.gameObject.AddComponent<DebugPathBehaviour>();
            }
        }
    }

    public void removeDebugPathBehaviours() {
        shouldAddDebugPath = false;
        foreach (EditableBehaviour behaviour in UnityEngine.Object.FindObjectsOfType<EditableBehaviour>()) {
            DebugPathBehaviour debugPath = behaviour.GetComponent<DebugPathBehaviour>();
            if (debugPath != null) {
                UnityEngine.Object.Destroy(debugPath);
            }
        }
    }
}
