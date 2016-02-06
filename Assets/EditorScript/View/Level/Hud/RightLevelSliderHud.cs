using Entitas;
using UnityEngine;
using UnityEngine.UI;

public class RightLevelSliderHud : BaseGui {
    Camera camera;
    Pool pool;
    LevelModelComponent component;

    public RightLevelSliderHud(Transform transform, Pool pool, LevelModelComponent component) {
        go = transform.gameObject;
        this.pool = pool;
        this.component = component;
        camera = Camera.main;
        addOnValueChangeListener();
        addSliderAutoUpdate();
    }

    void addSliderAutoUpdate() {
        SliderAutoUpdateBehaviour behaviour = go.AddComponent<SliderAutoUpdateBehaviour>();
        behaviour.setInProgressAction(isGameInProgress);
        behaviour.setEndPosition(getGameEndPosition());
    }

    void addOnValueChangeListener() {
        Slider slider = go.GetComponent<Slider>();
        slider.onValueChanged.AddListener(onValueChange);
    }

    void onValueChange(float value) {
        if (isGameInProgress())
            moveView(value);
        else
            camera.transform.position = new Vector3(0.0f, getPositionYBasedOnSlider(value), camera.transform.position.z);
    }

    bool isGameInProgress() {
        return pool.GetGroup(Matcher.Player).count > 0;
    }

    void moveView(float value) {
        float y = getPositionYBasedOnSlider(value);
        moveCamera(y);
        clearEnemies();
        clearWaveSpawners();
        setWaveAndEnemyIndexBasedOnPosition(y);
    }

    void clearEnemies() {
        foreach (Entity e in pool.GetGroup(Matcher.Enemy).GetEntities())
            e.IsDestroyEntity(true);
    }

    void clearWaveSpawners() {
        foreach (Entity e in pool.GetGroup(Matcher.WaveSpawner).GetEntities())
            e.IsDestroyEntity(true);
    }

    void setWaveAndEnemyIndexBasedOnPosition(float y) {
        LevelModelComponent level = pool.GetGroup(Matcher.EnemySpawner).GetSingleEntity().enemySpawner.model;//.pos.y = getPositionYBasedOnSlider(value);
        level.waveIndex = 0;
        level.enemyIndex = 0;
        foreach (WaveModel wave in level.waves)
            if (wave.spawnBarrier < y)
                level.waveIndex += 1;
        foreach (EnemyModel enemy in level.enemies)
            if (enemy.spawnBarrier < y)
                level.enemyIndex += 1;
    }

    void moveCamera(float y) {
        pool.GetGroup(Matcher.Camera).GetSingleEntity().position.pos.y = y;
    }

    float getPositionYBasedOnSlider(float value) {
        return value * getGameEndPosition();
    }

    float getGameEndPosition() {
        return component.size.y - component.position.y;
    }
}