using Entitas;
using System;
using UnityEngine;
using UnityEngine.UI;

public class EditLevelView : View, IView {
    public static IPathService pathService;

    ILevelService levelService;
    IViewService viewService;

    LevelModelComponent component;

    LevelActionExecutor executor;
    ViewModifier modifier;
    EditableElementsFactory factory;

    LeftPanelHud leftPanelHud;
    RightBottomPanelHud rightBottomPanelHud;
    RightLevelSliderHud rightLevelSliderHud;

    public EditLevelView(Pool pool, ILevelService levelService, IViewService viewService, IPathService pathService) : base("EditorView/Level/EditLevelView") {
        this.pool = pool;
        this.levelService = levelService;
        this.viewService = viewService;
        EditLevelView.pathService = pathService;
    }

    public void SetData(LevelModelComponent component) {
        this.component = component;
        attachScripts();
        createModifiers(component);
        fillViewByComponent(component);
        createHud();
    }

    public override void Destroy() {
        base.Destroy();
        rightBottomPanelHud.Destroy();
        rightLevelSliderHud.Destroy();
        foreach (EditableBehaviour script in UnityEngine.Object.FindObjectsOfType<EditableBehaviour>()) {
            UnityEngine.Object.Destroy(script.gameObject);
        }
    }

    void fillViewByComponent(LevelModelComponent component) {
        foreach (WaveModel model in component.waves) {
            factory.CreateWaveElement(model);
        }
        foreach (EnemyModel model in component.enemies) {
            factory.CreateEnemyElement(model);
        }
        factory.refreshNumeration();
    }

    void createModifiers(LevelModelComponent component) {
        executor = new LevelActionExecutor(component);
        factory = new EditableElementsFactory(pool.GetGroup(Matcher.MaterialReference).GetSingleEntity().materialReference.storage.Default);
        createViewModifier();
    }

    void createViewModifier() {
        modifier = go.AddComponent<ViewModifier>();
        modifier.SetExecutor(executor);
        modifier.SetFactory(factory);
        modifier.SetEventService(eventService);
        modifier.SetPool(pool);
    }

    void attachScripts() {
        go.AddComponent<CameraController>();
    }

    void createHud() {
        leftPanelHud = new LeftPanelHud(getChild("LeftPanel"), eventService, executor, save, goToLevelsView);
        leftPanelHud.setDebugToggles(changeDebugPathView, changeDebugTimeView);
        leftPanelHud.setStartEndGameCallbacks(startGame, endGame);
        new RightPanelHud(getChild("RightPanel"), onSelectedTypeChange);
        rightBottomPanelHud = new RightBottomPanelHud(getChild("RightBottomPanel"), eventService);
        rightLevelSliderHud = new RightLevelSliderHud(getChild("LevelSlider"), pool, component);
    }

    void changeDebugPathView(bool show) {
        if (show)
            factory.addDebugPathBehavioursIfNotExists();
        else
            factory.removeDebugPathBehaviours();
    }

    void changeDebugTimeView(bool show) {
        if (show)
            showTimeElement(calculateLevelTime());
        else
            hideTimeElement();
    }

    void onSelectedTypeChange(SelectedType type) {
        modifier.SetSelectedType(type);
    }

    void save() {
        endGame();
        levelService.UpdateLevel(component, goToLevelsView);
    }

    void goToLevelsView() {
        viewService.SetView(ViewTypes.EDITOR_LEVELS);
    }

    void startGame() {
        eventService.Dispatch<NoActiveModelEvent>(new NoActiveModelEvent());
        pool.GetGroup(Matcher.CurrentShip).GetSingleEntity().currentShip.model.health = int.MaxValue;
        go.AddComponent<HighlightCurrentNodeBehaviour>();
        pool.CreateEntity()
            .AddComponent(ComponentIds.LevelModel, component);

        pool.CreateEntity()
            .AddStartGame(Convert.ToInt16(component.id));
    }

    void endGame() {
        UnityEngine.Object.Destroy(go.GetComponent<HighlightCurrentNodeBehaviour>());
        //pool.DestroyEntity(pool.GetGroup(Matcher.LevelModel).GetSingleEntity());
        pool.CreateEntity()
            .IsEndGame(true);
    }

    void hideTimeElement() {
        getTimeElementGO().SetActive(false);
    }

    void showTimeElement(float time) {
        GameObject timeElement = getTimeElementGO();
        timeElement.SetActive(true);
        timeElement.GetComponent<Text>().text = "~" + time + "s";
    }

    float calculateLevelTime() {
        float maxY = 0.0f;
        foreach (EnemyModel enemy in component.enemies) {
            maxY = enemy.posY > maxY ? enemy.posY : maxY;
        }
        foreach (WaveModel wave in component.waves) {
            maxY = wave.spawnBarrier > maxY ? wave.spawnBarrier : maxY;
        }

        return maxY * Config.CAMERA_SPEED;
    }

    GameObject getTimeElementGO() {
        return getChild("TimeElement").gameObject;
    }
}