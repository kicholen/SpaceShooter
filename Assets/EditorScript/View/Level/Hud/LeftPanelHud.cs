﻿using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LeftPanelHud : BaseGui {
    Transform content;
    EventService eventService;
    LevelActionExecutor executor;

    LeftPanelViewState state;

    public LeftPanelHud(Transform content, EventService eventService, LevelActionExecutor executor, Action onSave, Action onBack) {
        go = content.gameObject;
        this.eventService = eventService;
        this.executor = executor;
        state = LeftPanelViewState.Hidden;
        setData(onSave, onBack);
    }

    public void setDebugToggles(Action<bool> onPathViewChanged) {
        createToggleElement("showPaths", (value) => onPathViewChanged(value)).transform.SetParent(content, false);
    }

    void setData(Action onSave, Action onBack) {
        content = getChild("ExtendPanel/Viewport/Content");
        addListeners(onSave, onBack);
        fillContent();
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

    void fillContent() {
        createNameInptField().transform.SetParent(content, false);
        createPositionElement().transform.SetParent(content, false);
        createSizeElement().transform.SetParent(content, false);
    }

    GameObject createPositionElement() {
        return new InputVectorElement("StartPosition", eventService, executor.getPosition(), (value) => {
            executor.Execute(new ChangeStartPositionAction(value, executor.getPosition().y));
        }, (value) => {
            executor.Execute(new ChangeStartPositionAction(executor.getPosition().x, value));
        }).Go;
    }

    GameObject createSizeElement() {
        return new InputVectorElement("LevelSize", eventService, executor.getSize(), (value) => {
            executor.Execute(new ChangeLevelDimensionAction(value, executor.getSize().y));
        }, (value) => {
            executor.Execute(new ChangeLevelDimensionAction(executor.getSize().x, value));
        }).Go;
    }

    GameObject createNameInptField() {
        return createInputElement(executor.getName(), (value) => {
            executor.Execute(new ChangeLevelNameAction(value));
        });
    }

    GameObject createInputElement(string defaultText, UnityAction<string> onValueChange) {
        GameObject gameObject = UnityEngine.Object.Instantiate(Resources.Load<GameObject>("Prefab/UI/EditorView/Level/InputElement"));
        gameObject.GetComponentInChildren<Text>().text = "LevelName";
        InputField input = gameObject.GetComponentInChildren<InputField>();
        input.onValueChange.AddListener(onValueChange);
        input.text = defaultText;

        return gameObject;
    }

    GameObject createToggleElement(string text, UnityAction<bool> onValueChange) {
        /*GameObject gameObject = UnityEngine.Object.Instantiate(Resources.Load<GameObject>("Prefab/UI/EditorView/Level/ToggleElement"));
        gameObject.GetComponentInChildren<Text>().text = text;
        Toggle toggle = gameObject.GetComponentInChildren<Toggle>();
        toggle.isOn = false;
        toggle.onValueChanged.AddListener(onValueChange);
        */
        return new GameObject();//gameObject;
    }
}

internal enum LeftPanelViewState {
    Shown,
    Hidden
}