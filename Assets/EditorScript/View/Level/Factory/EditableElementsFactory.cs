using UnityEngine;
using System;

public class EditableElementsFactory {
    bool shouldAddDebugPath = false;
    Material material;

    public EditableElementsFactory(Material material) {
        this.material = material;
    }

    public GameObject CreateWaveElement(WaveModel model) {
        GameObject go = UnityEngine.Object.Instantiate(Resources.Load<GameObject>("Prefab/UI/EditorView/Level/WaveElement"));
        EditableBehaviour editableBehaviour = go.AddComponent<EditableBehaviour>();
        editableBehaviour.SetOnSpawnBarrierChangedCallback(refreshNumeration);
        editableBehaviour.SetWaveModel(model);
        addDebugPathIfShould(go);
        return go;
    }

    public GameObject CreateEnemyElement(EnemyModel model) {
        GameObject go = UnityEngine.Object.Instantiate(Resources.Load<GameObject>("Prefab/UI/EditorView/Level/EnemyElement"));
        EditableBehaviour editableBehaviour = go.AddComponent<EditableBehaviour>();
        editableBehaviour.SetOnSpawnBarrierChangedCallback(refreshNumeration);
        editableBehaviour.SetEnemyModel(model);
        addDebugPathIfShould(go);
        return go;
    }

    void addDebugPathIfShould(GameObject go) {
        if (shouldAddDebugPath) {
            DebugPathBehaviour script = go.AddComponent<DebugPathBehaviour>();
            script.Init(material);
        }
    }

    public void refreshNumeration() {
        shouldAddDebugPath = true;
        EditableBehaviour[] behaviours = UnityEngine.Object.FindObjectsOfType<EditableBehaviour>();
        Array.Sort(behaviours, (x, y) => x.SpawnBarrier.CompareTo(y.SpawnBarrier));
        for (int i = 0; i < behaviours.Length; i++)
            behaviours[i].GetComponentInChildren<TextMesh>().text = i.ToString();
    }

    public void addDebugPathBehavioursIfNotExists() {
        shouldAddDebugPath = true;
        foreach (EditableBehaviour behaviour in UnityEngine.Object.FindObjectsOfType<EditableBehaviour>()) {
            DebugPathBehaviour script = behaviour.GetComponent<DebugPathBehaviour>();
            if (script == null) {
                script = behaviour.gameObject.AddComponent<DebugPathBehaviour>();
                script.Init(material);
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
