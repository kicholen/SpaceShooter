using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Linq;

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

    public override void Destroy() {
        base.Destroy();
        removeListeners();
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
        eventService.AddListener<NoActiveModelEvent>(onNoActiveModel);
        eventService.AddListener<ActiveWaveModelChangeEvent>(onWaveModelChange);
        eventService.AddListener<ActiveEnemyModelChangeEvent>(onEnemyModelChange);
    }

    void onNoActiveModel(NoActiveModelEvent gameEvent) {
        hideView();
    }
    void onWaveModelChange(ActiveWaveModelChangeEvent gameEvent) {
        updateViewWithWaveModel(gameEvent.model);
    }

    void onEnemyModelChange(ActiveEnemyModelChangeEvent gameEvent) {
        updateViewWithEnemyModel(gameEvent.model);
    }

    void removeListeners() {
        eventService.RemoveListener<NoActiveModelEvent>(onNoActiveModel);
        eventService.RemoveListener<ActiveWaveModelChangeEvent>(onWaveModelChange);
        eventService.RemoveListener<ActiveEnemyModelChangeEvent>(onEnemyModelChange);
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
        return createDropdownElement("path", waveExecutor.getPath().ToString(), EditLevelView.pathService.GetPathNames(), (value) => {
            waveExecutor.Execute(new ChangeWavePathAction(value));
        });
    }

    GameObject createChangeGridField() {
        List<string> gridTypes = typeof(GridTypes).GetFields(System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public)
            .Select(fieldInfo => ((int)fieldInfo.GetRawConstantValue()).ToString()).ToList<string>();
        return createDropdownElement("grid", waveExecutor.getGrid().ToString(), gridTypes, (value) => {
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

    GameObject createDropdownElement(string infoText, string defaultText, List<string> names, UnityAction<string> onValueChange) {
        GameObject gameObject = UnityEngine.Object.Instantiate(Resources.Load<GameObject>("Prefab/UI/EditorView/Level/DropdownElement"));
        gameObject.GetComponentInChildren<Dropdown>().value = System.Convert.ToInt32(defaultText);
        gameObject.GetComponentInChildren<Dropdown>().options = names.Select(name => new Dropdown.OptionData(name)).ToList<Dropdown.OptionData>();
        gameObject.transform.FindChild("Label").GetComponent<Text>().text = infoText;
        gameObject.GetComponentInChildren<Dropdown>().onValueChanged.AddListener(value => onValueChange(value.ToString()));

        return gameObject;
    }
}