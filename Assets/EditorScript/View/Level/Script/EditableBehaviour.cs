using UnityEngine;

public class EditableBehaviour : MonoBehaviour {
    public WaveModel waveModel;
    public EnemyModel enemyModel;

    public float SpawnBarrier { get { return waveModel != null ? waveModel.spawnBarrier : enemyModel.spawnBarrier; } }

    public void SetWaveModel(WaveModel waveModel) {
        this.waveModel = waveModel;
        SetSpawnBarrier(waveModel.spawnBarrier);
    }

    public void SetEnemyModel(EnemyModel enemyModel) {
        this.enemyModel = enemyModel;
        SetSpawnBarrier(enemyModel.spawnBarrier);
    }

    public void SetSpawnBarrier(float value) {
        if (waveModel != null) {
            waveModel.spawnBarrier = value;
        }
        else {
            enemyModel.spawnBarrier = value;
        }
        transform.position = new Vector3(0.0f, value, 0.0f);
    }
}