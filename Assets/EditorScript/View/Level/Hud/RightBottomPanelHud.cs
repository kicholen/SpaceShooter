using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class RightBottomPanelHud : BaseGui {
    Transform content;
    EventService eventService;
    WaveActionExecutor waveExecutor;

    public RightBottomPanelHud(Transform content, EventService eventService) {
        go = content.gameObject;
        this.eventService = eventService;
        prepareHud();
        addListeners();
    }

    void prepareHud() {
        content = getChild("Viewport/Content");
        hideView();
    }

    void hideView() {
        go.SetActive(false);
    }

    void showView() {
        go.SetActive(true);
    }

    void addListeners() {
        eventService.AddListener<NoActiveModelEvent>((gameEvent) => hideView());
        eventService.AddListener<ActiveWaveModelChangeEvent>((gameEvent) => updateViewWithWaveModel(gameEvent.model));
        eventService.AddListener<ActiveEnemyModelChangeEvent>((gameEvent) => updateViewWithEnemyModel(gameEvent.model));
    }

    void updateViewWithWaveModel(WaveModel waveModel) {
        showView();
        waveExecutor = new WaveActionExecutor(waveModel);
        removeChildren(content.gameObject);
        createChangeWaveCountField().transform.SetParent(content, false);
        createChangeWaveSpawnOffsetField().transform.SetParent(content, false);
        createChangeSpeedField().transform.SetParent(content, false);
        createChangeHealthField().transform.SetParent(content, false);
        createChangePathField().transform.SetParent(content, false);
        createChangeGridField().transform.SetParent(content, false);
        createChangeDamageField().transform.SetParent(content, false);
    }

    void updateViewWithEnemyModel(EnemyModel enemyModel) {
        showView();
        removeChildren(content.gameObject);
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
        return createInputElement("path", waveExecutor.getPath().ToString(), (value) => {
            waveExecutor.Execute(new ChangeWavePathAction(value));
        });
    }

    GameObject createChangeGridField() {
        return createInputElement("grid", waveExecutor.getGrid().ToString(), (value) => {
            waveExecutor.Execute(new ChangeWaveGridAction(value));
        });
    }

    GameObject createChangeDamageField() {
        return createInputElement("damage", waveExecutor.getDamage().ToString(), (value) => {
            waveExecutor.Execute(new ChangeWaveDamageAction(value));
        });
    }

    GameObject createInputElement(string infoText, string defaultText, UnityAction<string> onValueChange) {
        GameObject gameObject = UnityEngine.Object.Instantiate(Resources.Load<GameObject>("Prefab/UI/EditorView/Level/InputElement"));
        gameObject.GetComponentInChildren<Text>().text = infoText;
        InputField input = gameObject.GetComponentInChildren<InputField>();
        input.onValueChange.AddListener(onValueChange);
        input.text = defaultText;

        return gameObject;
    }
}