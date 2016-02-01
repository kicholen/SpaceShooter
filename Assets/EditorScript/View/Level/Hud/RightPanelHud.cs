using System;
using UnityEngine;
using UnityEngine.UI;

public class RightPanelHud : BaseGui {
    public RightPanelHud(Transform content, Action<SelectedType> onSelectedTypeChange) {
        go = content.gameObject;
        addListeners(onSelectedTypeChange);
    }

    void addListeners(Action<SelectedType> onSelectedTypeChange) {
        getChild("WaveButton").GetComponent<Button>().onClick.AddListener(() => onSelectedTypeChange(SelectedType.Wave));
        getChild("EnemyButton").GetComponent<Button>().onClick.AddListener(() => onSelectedTypeChange(SelectedType.Enemy));
    }
}
