using Entitas;
using System;
using UnityEngine;
using UnityEngine.UI;

public class EditLevelView : View, IView {
    public static IPathService pathService;
    public static IEnemyService enemyService;

    ILevelService levelService;
    IViewService viewService;

    LevelModelComponent component;

    LevelActionExecutor executor;
    ViewModifier modifier;
    EditableElementsFactory factory;

    LeftPanelHud leftPanelHud;
    RightBottomPanelHud rightBottomPanelHud;
    RightLevelSliderHud rightLevelSliderHud;

    public EditLevelView(Pool pool, ILevelService levelService, IViewService viewService, IPathService pathService, IEnemyService enemyService)
        : base("EditorView/Level/EditLevelView") {
        this.pool = pool;
        this.levelService = levelService;
        this.viewService = viewService;
        EditLevelView.pathService = pathService;
        EditLevelView.enemyService = enemyService;
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
        foreach (WaveSpawnModel model in component.waves) {
            factory.CreateWaveElement(model);
        }
        foreach (EnemySpawnModel model in component.enemies) {
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
        leftPanelHud = new LeftPanelHud(getChild("LeftPanel"), eventService, executor, delete, save, goToLevelsView, clone);
        leftPanelHud.setDebugToggles(changeDebugPathView, changeDebugTimeView, showDebugGrid);
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

    void showDebugGrid(bool show) {
        if (show)
            go.AddComponent<EditorGrid>();
        else
            UnityEngine.Object.Destroy(go.GetComponent<EditorGrid>());
    }

    void onSelectedTypeChange(SelectedType type) {
        modifier.SetSelectedType(type);
    }

    void save()
    {
        sortWavesAndEnemies();
        endGame();
        levelService.UpdateLevel(component, goToLevelsView);
    }

    void sortWavesAndEnemies()
    {
        executor.Execute(new SortWavesAndEnemiesAction());
    }

    void delete() {
        endGame();
        levelService.DeleteLevel(component.id, goToLevelsView);
    }

    void clone()
    {
        endGame();
        sortWavesAndEnemies();
        levelService.CreateNewLevel(newComponent => {
            newComponent.enemies = component.enemies;
            newComponent.enemyIndex = component.enemyIndex;
            newComponent.name = component.name + "(cloned)";
            newComponent.position = component.position;
            newComponent.size = component.size;
            newComponent.waveIndex = component.waveIndex;
            newComponent.waves = component.waves;
            levelService.UpdateLevel(newComponent, () => 
                eventService.Dispatch<InfoBoxShowEvent>(new InfoBoxShowEvent("Cloned to " + newComponent.name))
            );
        });
    }

    void goToLevelsView() {
        viewService.SetView(ViewTypes.EDITOR_LEVELS);
    }

    void startGame() {
        sortWavesAndEnemies();
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
        if (isGameInProgress()) {
            pool.DestroyEntity(pool.GetGroup(Matcher.LevelModel).GetSingleEntity());
            pool.CreateEntity()
                .IsEndGame(true);
        }
    }

    bool isGameInProgress() {
        return pool.GetGroup(Matcher.Player).count > 0;
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
        foreach (EnemySpawnModel enemy in component.enemies) {
            maxY = enemy.posY > maxY ? enemy.posY : maxY;
        }
        foreach (WaveSpawnModel wave in component.waves) {
            maxY = wave.spawnBarrier > maxY ? wave.spawnBarrier : maxY;
        }

        return maxY * GameConfig.CAMERA_SPEED;
    }

    GameObject getTimeElementGO() {
        return getChild("TimeElement").gameObject;
    }
}