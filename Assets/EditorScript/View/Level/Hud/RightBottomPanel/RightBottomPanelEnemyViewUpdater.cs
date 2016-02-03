using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RightBottomPanelEnemyViewUpdater : RightBottomPanelViewUpdaterBase {
    EnemyActionExecutor enemyExecutor;

    public void Update(Transform content, EnemyModel enemyModel) {
        enemyExecutor = new EnemyActionExecutor(enemyModel);
        createChangePositionXField().transform.SetParent(content, false);
        createChangePositionYField().transform.SetParent(content, false);
        createChangeSpeedField().transform.SetParent(content, false);
        createChangeHealthField().transform.SetParent(content, false);
        createChangePathField().transform.SetParent(content, false);
        createChangeDamageField().transform.SetParent(content, false);
        createChangeTypeField().transform.SetParent(content, false);
    }

    GameObject createChangePositionXField() {
        return createInputElement("posX", enemyExecutor.GetPosX().ToString(), (value) => {
            enemyExecutor.Execute(new ChangeEnemyPosXAction(value));
        });
    }

    GameObject createChangePositionYField() {
        return createInputElement("posY", enemyExecutor.GetPosY().ToString(), (value) => {
            enemyExecutor.Execute(new ChangeEnemyPosYAction(value));
        });
    }

    GameObject createChangeSpeedField() {
        return createInputElement("speed", enemyExecutor.getSpeed().ToString(), (value) => {
            enemyExecutor.Execute(new ChangeEnemySpeedAction(value));
        });
    }

    GameObject createChangeHealthField() {
        return createInputElement("health", enemyExecutor.getHealth().ToString(), (value) => {
            enemyExecutor.Execute(new ChangeEnemyHealthAction(value));
        });
    }

    GameObject createChangePathField() {
        List<string> options = EditLevelView.pathService.GetPathNames();
        options.Add("0");
        return createDropdownElement("path", enemyExecutor.getPath().ToString(), options, (value) => {
            Debug.Log(value);
            enemyExecutor.Execute(new ChangeEnemyPathAction(value));
        });
    }

    GameObject createChangeDamageField() {
        return createInputElement("damage", enemyExecutor.getDamage().ToString(), (value) => {
            enemyExecutor.Execute(new ChangeEnemyDamageAction(value));
        });
    }

    GameObject createChangeTypeField() {
        List<string> types = typeof(EnemyTypes).GetFields(System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public)
            .Select(fieldInfo => ((int)fieldInfo.GetRawConstantValue()).ToString()).ToList<string>();
        return createDropdownElement("type", enemyExecutor.getType().ToString(), types, (value) => {
            enemyExecutor.Execute(new ChangeEnemyTypeAction(value));
        });
    }
}
