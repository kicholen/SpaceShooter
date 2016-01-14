using UnityEngine;

public class EditableElementsFactory {

    public GameObject CreateWaveElement(WaveModel model) {
        GameObject go = Object.Instantiate(Resources.Load<GameObject>("Prefab/UI/EditorView/Level/WaveElement"));
        go.AddComponent<EditableBehaviour>().SetWaveModel(model);
        return go;
    }

    public GameObject CreateEnemyElement(EnemyModel model) {
        GameObject go = Object.Instantiate(Resources.Load<GameObject>("Prefab/UI/EditorView/Level/WaveElement"));
        go.AddComponent<EditableBehaviour>().SetEnemyModel(model);
        return go;
    }
}
