using Entitas;
using System;

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
    }

    void createModifiers(LevelModelComponent component) {
        executor = new LevelActionExecutor(component);
        factory = new EditableElementsFactory(pool.GetGroup(Matcher.MaterialReference).GetSingleEntity().materialReference.storage.Default);
        modifier = go.AddComponent<ViewModifier>();
        modifier.SetExecutor(executor);
        modifier.SetFactory(factory);
        modifier.SetEventService(eventService);
    }

    void attachScripts() {
        go.AddComponent<CameraController>();
    }

    void createHud() {
        leftPanelHud = new LeftPanelHud(getChild("LeftPanel"), eventService, executor, save, goToLevelsView);
        leftPanelHud.setDebugToggles(changeDebugPathView);
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

    void onSelectedTypeChange(SelectedType type) {
        modifier.SetSelectedType(type);
    }

    void save() {
        levelService.UpdateLevel(component, goToLevelsView);
    }

    void goToLevelsView() {
        viewService.SetView(ViewTypes.EDITOR_LEVELS);
    }

    void startGame() {
        pool.GetGroup(Matcher.CurrentShip).GetSingleEntity().currentShip.model.health = int.MaxValue;
        pool.CreateEntity()
            .AddComponent(ComponentIds.LevelModel, component);

        pool.CreateEntity()
            .AddStartGame(Convert.ToInt16(component.id));
    }

    void endGame() {
        pool.DestroyEntity(pool.GetGroup(Matcher.LevelModel).GetSingleEntity());
        pool.CreateEntity()
            .IsEndGame(true);
    }
}