using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RightBottomPanelEnemyViewUpdater : EditorViewUpdaterBase {
    EnemyActionExecutor enemyExecutor;

    public void Update(Transform content, EnemyModel enemyModel) {
        enemyExecutor = new EnemyActionExecutor(enemyModel);
        createChangeSpawnBarrierField().transform.SetParent(content, false);
        createChangePositionXField().transform.SetParent(content, false);
        createChangePositionYField().transform.SetParent(content, false);
        createChangeSpeedField().transform.SetParent(content, false);
        createChangeHealthField().transform.SetParent(content, false);
        createChangePathField().transform.SetParent(content, false);
        createChangeDamageField().transform.SetParent(content, false);
        createChangeTypeField().transform.SetParent(content, false);
    }

    GameObject createChangeSpawnBarrierField() {
        return createInputElement("spawnBarrier", enemyExecutor.GetSpawnBarrier().ToString(), (value) => {
            enemyExecutor.Execute(new ChangeEnemySpawnBarrierAction(value));
        });
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
            enemyExecutor.Execute(new ChangeEnemyPathAction(options[Convert.ToInt16(value)]));
        });
    }

    GameObject createChangeDamageField() {
        return createInputElement("damage", enemyExecutor.getDamage().ToString(), (value) => {
            enemyExecutor.Execute(new ChangeEnemyDamageAction(value));
        });
    }

    GameObject createChangeTypeField() {
        List<string> types = EditLevelView.enemyService.GetEnemyNames();
        types.Insert(0, "0");
        types.Insert(0, "500");
        types.Insert(0, "501");
        types.Insert(0, "502");
        types.Insert(0, "1000");
        types.Insert(0, "1001");

        return createDropdownElement("type", enemyExecutor.getType().ToString(), types, (value) => {
            enemyExecutor.Execute(new ChangeEnemyTypeAction(types[Convert.ToInt16(value)]));
        });
    }
}
