using System;
using UnityEngine;
using UnityEngine.UI;

public class RightPanelHud : BaseGui {
    EventService eventService;

    public RightPanelHud(Transform content, EventService eventService, Action<SelectedType> onSelectedTypeChange) {
        go = content.gameObject;
        this.eventService = eventService;
        addListeners(onSelectedTypeChange);
    }

    void addListeners(Action<SelectedType> onSelectedTypeChange) {
        getChild("WaveButton").GetComponent<Button>().onClick.AddListener(() => onSelectedTypeChange(SelectedType.Wave));
        getChild("EnemyButton").GetComponent<Button>().onClick.AddListener(() => onSelectedTypeChange(SelectedType.Enemy));
    }
}
