using System;
using UnityEngine;

public class EditableBehaviour : MonoBehaviour {
    public WaveSpawnModel waveModel;
    public EnemySpawnModel enemyModel;

    Action refreshNumeration;

    public float SpawnBarrier { get { return waveModel != null ? waveModel.spawnBarrier : enemyModel.spawnBarrier; } }

    public void SetWaveModel(WaveSpawnModel waveModel) {
        this.waveModel = waveModel;
        SetSpawnBarrier(waveModel.spawnBarrier);
    }

    public void SetEnemyModel(EnemySpawnModel enemyModel) {
        this.enemyModel = enemyModel;
        SetSpawnBarrier(enemyModel.spawnBarrier);
    }

    public void SetSpawnBarrier(float value) {
        if (waveModel != null)
            waveModel.spawnBarrier = value;
        else
            enemyModel.spawnBarrier = value;
        updatePosition();
    }

    public void SetOnSpawnBarrierChangedCallback(Action refreshNumeration)
    {
        this.refreshNumeration = refreshNumeration;
    }

    void Update()
    {
        if (hasSpawnBarrierChanged())
            updatePosition();
    }

    bool hasSpawnBarrierChanged()
    {
        return Math.Abs(transform.position.y - getSpawnBarrier()) > 0.01;
    }

    void updatePosition()
    {
        transform.position = new Vector3(Mathf.Sin(getSpawnBarrier()) + UnityEngine.Random.Range(-0.4f, 0.4f), getSpawnBarrier(), 0.0f);
        refreshNumeration();
    }

    float getSpawnBarrier()
    {
        return enemyModel != null ? enemyModel.spawnBarrier : waveModel.spawnBarrier;
    }
}