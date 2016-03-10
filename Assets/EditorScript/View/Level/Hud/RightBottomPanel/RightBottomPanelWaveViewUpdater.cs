using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RightBottomPanelWaveViewUpdater : EditorViewUpdaterBase {
    WaveActionExecutor waveExecutor;

    public void Update(Transform content, WaveModel waveModel) {
        waveExecutor = new WaveActionExecutor(waveModel);
        createChangeSpawnBarrierField().transform.SetParent(content, false);
        createChangeWaveCountField().transform.SetParent(content, false);
        createChangeWaveSpawnOffsetField().transform.SetParent(content, false);
        createChangeSpeedField().transform.SetParent(content, false);
        createChangeHealthField().transform.SetParent(content, false);
        createChangePathField().transform.SetParent(content, false);
        createChangeGridField().transform.SetParent(content, false);
        createChangeDamageField().transform.SetParent(content, false);
        createChangeTypeField().transform.SetParent(content, false);
    }

    GameObject createChangeSpawnBarrierField() {
        return createInputElement("spawnBarrier", waveExecutor.GetSpawnBarrier().ToString(), (value) => {
            waveExecutor.Execute(new ChangeWaveSpawnBarrierAction(value));
        });
    }

    GameObject createChangeWaveCountField() {
        return createInputElement("count", waveExecutor.GetCount().ToString(), (value) => {
            waveExecutor.Execute(new ChangeWaveCountAction(value));
        });
    }

    GameObject createChangeWaveSpawnOffsetField() {
        return createInputElement("spawnOffset", waveExecutor.GetSpawnOffset().ToString(), (value) => {
            waveExecutor.Execute(new ChangeWaveSpawnOffsetAction(value));
        });
    }

    GameObject createChangeSpeedField() {
        return createInputElement("speed", waveExecutor.getSpeed().ToString(), (value) => {
            waveExecutor.Execute(new ChangeWaveSpeedAction(value));
        });
    }

    GameObject createChangeHealthField() {
        return createInputElement("health", waveExecutor.getHealth().ToString(), (value) => {
            waveExecutor.Execute(new ChangeWaveHealthAction(value));
        });
    }

    GameObject createChangePathField() {
        List<string> options = EditLevelView.pathService.GetPathNames();
        options.Add("0");
        return createDropdownElement("path", waveExecutor.getPath().ToString(), options, (value) => {
            waveExecutor.Execute(new ChangeWavePathAction(options[Convert.ToInt16(value)]));
        });
    }

    GameObject createChangeGridField() {
        List<string> gridTypes = typeof(GridTypes).GetFields(System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public)
            .Select(fieldInfo => ((int)fieldInfo.GetRawConstantValue()).ToString()).ToList<string>();
        return createDropdownElement("grid", waveExecutor.getGrid().ToString(), gridTypes, (value) => {
            waveExecutor.Execute(new ChangeWaveGridAction(value));
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

        return createDropdownElement("type", waveExecutor.getType().ToString(), types, (value) => {
            waveExecutor.Execute(new ChangeWaveTypeAction(types[Convert.ToInt16(value)]));
        });
    }

    GameObject createChangeDamageField() {
        return createInputElement("damage", waveExecutor.getDamage().ToString(), (value) => {
            waveExecutor.Execute(new ChangeWaveDamageAction(value));
        });
    }
}
