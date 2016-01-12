using UnityEngine;

public class EditableBehaviour : MonoBehaviour {
    WaveModel waveModel;
    EnemyModel enemyModel;

    public void SetWaveModel(WaveModel waveModel) {
        this.waveModel = waveModel;
        SetSpawnBarrier(waveModel.spawnBarrier);
    }

    public void SetSpawnBarrier(float value) {
        waveModel.spawnBarrier = value;
        transform.position = new Vector3(0.0f, value, 0.0f);
    }
}