using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LeftPanelHud : BaseGui {
    Transform content;
    EventService eventService;
    LevelActionExecutor executor;

    LeftPanelViewState state;
    bool isGameInProgress;

    public LeftPanelHud(Transform content, EventService eventService, LevelActionExecutor executor, Action onDelete, Action onSave, Action onBack) {
        go = content.gameObject;
        this.eventService = eventService;
        this.executor = executor;
        state = LeftPanelViewState.Hidden;
        setData(onDelete, onSave, onBack);
    }

    public void setDebugToggles(Action<bool> onPathViewChanged, Action<bool> onTimeViewChanged, Action<bool> onGridViewChanged) {
        createToggleElement("showPaths", (value) => onPathViewChanged(value)).transform.SetParent(content, false);
        createToggleElement("showTime", (value) => onTimeViewChanged(value)).transform.SetParent(content, false);
        createToggleElement("showGrid", (value) => onGridViewChanged(value)).transform.SetParent(content, false);
    }

    public void setStartEndGameCallbacks(Action startGame, Action endGame) {
        getChild("ExtendPanel/StopStartGameButton").GetComponent<Button>().onClick.AddListener(() => {
            if (isGameInProgress) {
                endGame();
                isGameInProgress = false;
            }
            else {
                startGame();
                isGameInProgress = true;
            }
        });
        getChild("ExtendPanel/BackButton").GetComponent<Button>().onClick.AddListener(() => {
            if (isGameInProgress)
                endGame();
        });
    }

    void setData(Action onDelete, Action onSave, Action onBack) {
        content = getChild("ExtendPanel/Viewport/Content");
        addListeners(onDelete, onSave, onBack);
        fillContent();
        updateView();
    }

    void addListeners(Action onDelete, Action onSave, Action onBack) {
        getChild("ExtendButton").GetComponent<Button>().onClick.AddListener(changeStateAndUpdateView);
        getChild("ExtendPanel/HideButton").GetComponent<Button>().onClick.AddListener(changeStateAndUpdateView);
        getChild("ExtendPanel/SaveButton").GetComponent<Button>().onClick.AddListener(() => onSave());
        getChild("ExtendPanel/BackButton").GetComponent<Button>().onClick.AddListener(() => onBack());
        getChild("ExtendPanel/DeleteButton").GetComponent<Button>().onClick.AddListener(() => onDelete());
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
        input.onValueChanged.AddListener(onValueChange);
        input.text = defaultText == null ? "" : defaultText;

        return gameObject;
    }

    GameObject createToggleElement(string text, UnityAction<bool> onValueChange) {
        GameObject gameObject = UnityEngine.Object.Instantiate(Resources.Load<GameObject>("Prefab/UI/EditorView/Level/ToggleElement"));
        gameObject.GetComponentInChildren<Text>().text = text;
        Toggle toggle = gameObject.GetComponentInChildren<Toggle>();
        toggle.isOn = false;
        toggle.onValueChanged.AddListener(onValueChange);
        
        return gameObject;
    }
}

internal enum LeftPanelViewState {
    Shown,
    Hidden
}