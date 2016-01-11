using System;
using UnityEngine;
using UnityEngine.UI;

public class LeftPanelHud : BaseGui {

    Transform content;
    LevelModifierExecutor executor;
    LeftPanelViewState state;

    public LeftPanelHud(Transform content, LevelModifierExecutor executor, Action onSave, Action onBack) {
        go = content.gameObject;
        this.executor = executor;
        state = LeftPanelViewState.Hidden;
        setData(onSave, onBack);
    }

    void setData(Action onSave, Action onBack) {
        content = getChild("ExtendPanel/Viewport/Content");
        addListeners(onSave, onBack);
        updateView();
    }

    void addListeners(Action onSave, Action onBack) {
        getChild("ExtendButton").GetComponent<Button>().onClick.AddListener(changeStateAndUpdateView);
        getChild("ExtendPanel/HideButton").GetComponent<Button>().onClick.AddListener(changeStateAndUpdateView);
        getChild("ExtendPanel/SaveButton").GetComponent<Button>().onClick.AddListener(() => onSave());
        getChild("ExtendPanel/BackButton").GetComponent<Button>().onClick.AddListener(() => onBack());
    }

    void changeStateAndUpdateView() {
        state = state == LeftPanelViewState.Hidden ? LeftPanelViewState.Shown : LeftPanelViewState.Hidden;
        updateView();
    }

    void updateView() {
        bool shouldShowExtend = state == LeftPanelViewState.Shown;
        getChild("ExtendPanel").gameObject.SetActive(shouldShowExtend);
        getChild("ExtendButton").gameObject.SetActive(!shouldShowExtend);
    }
}

internal enum LeftPanelViewState {
    Shown,
    Hidden
}