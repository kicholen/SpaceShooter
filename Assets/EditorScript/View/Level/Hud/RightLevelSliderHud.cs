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
    }

    void addOnValueChangeListener() {
        Slider slider = go.GetComponent<Slider>();
        slider.onValueChanged.AddListener(onValueChange);
    }

    void onValueChange(float value) {
        if (isGameInProgress())
            movePlayer(value);
        else
            camera.transform.position = new Vector3(0.0f, getPositionYBasedOnSlider(value), camera.transform.position.z);
    }

    bool isGameInProgress() {
        return pool.GetGroup(Matcher.Player).count > 0;
    }

    void movePlayer(float value) {
        pool.GetGroup(Matcher.Camera).GetSingleEntity().position.pos.y = getPositionYBasedOnSlider(value);
    }

    float getPositionYBasedOnSlider(float value) {
        return value * (component.size.y - component.position.y);
    }
}