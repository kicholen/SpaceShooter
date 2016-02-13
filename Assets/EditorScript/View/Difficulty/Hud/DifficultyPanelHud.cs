using UnityEngine;

public class DifficultyPanelHud : EditorViewUpdaterBase
{
    Transform content;
    DifficultyModelComponent component;

    DifficultyActionExecutor executor;

    public DifficultyPanelHud(Transform content, DifficultyModelComponent component) {
        this.content = content;
        this.component = component;
        executor = new DifficultyActionExecutor(component);
        setData();
    }

    void setData() {
        createChangeHpBoostPercentField().transform.SetParent(content, false);
        createChangeDmgBoostPercentField().transform.SetParent(content, false);
        createChangeMissileSpeedBoostPercentField().transform.SetParent(content, false);
    }

    GameObject createChangeHpBoostPercentField() {
        return createInputElement("hpBoostPercent", component.hpBoostPercent.ToString(), (value) => {
            executor.Execute(new ChangeDifficultyHpBoostAction(value));
        });
    }

    GameObject createChangeDmgBoostPercentField() {
        return createInputElement("dmgBoostPercent", component.dmgBoostPercent.ToString(), (value) => {
            executor.Execute(new ChangeDifficultyDmgBoostAction(value));
        });
    }

    GameObject createChangeMissileSpeedBoostPercentField() {
        return createInputElement("missileSpeedBoostPercent", component.missileSpeedBoostPercent.ToString(), (value) => {
            executor.Execute(new ChangeDifficultyMissileSpeedBoostPercent(value));
        });
    }
}
 