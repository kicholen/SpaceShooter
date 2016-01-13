using UnityEngine;

public class EditableElementsFactory {

    public void CreateWaveElement(WaveModel model) {
        GameObject go = Object.Instantiate(Resources.Load<GameObject>("Prefab/UI/EditorView/Level/WaveElement"));
        go.AddComponent<EditableBehaviour>().SetWaveModel(model);
    }

    public void CreateEnemyElement(EnemyModel model) {
        GameObject go = Object.Instantiate(Resources.Load<GameObject>("Prefab/UI/EditorView/Level/WaveElement"));
        go.AddComponent<EditableBehaviour>().SetEnemyModel(model);
    }
}
