using UnityEngine;

public class EditableBehaviour : MonoBehaviour {
    public WaveModel waveModel;
    public EnemyModel enemyModel;

    public void SetWaveModel(WaveModel waveModel) {
        this.waveModel = waveModel;
        SetSpawnBarrier(waveModel.spawnBarrier);
    }

    public void SetEnemyModel(EnemyModel enemyModel) {
        this.enemyModel = enemyModel;
        SetSpawnBarrier(enemyModel.spawnBarrier);
    }

    public void SetSpawnBarrier(float value) {
        waveModel.spawnBarrier = value;
        transform.position = new Vector3(0.0f, value, 0.0f);
    }
}