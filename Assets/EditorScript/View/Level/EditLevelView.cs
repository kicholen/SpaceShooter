﻿using System;

public class EditLevelView : View, IView {
    ILevelService levelService;
    IViewService viewService;

    LevelModelComponent component;

    LevelActionExecutor executor;
    ViewModifier modifier;
    EditableElementsFactory factory;

    LeftPanelHud leftPanelHud;
    RightPanelHud rightPanelHud;

    CameraController cameraController;

    public EditLevelView(ILevelService levelService, IViewService viewService) : base("EditorView/Level/EditLevelView") {
        this.levelService = levelService;
        this.viewService = viewService;
    }

    public void SetData(LevelModelComponent component) {
        this.component = component;
        createModifiers(component);
        attachScripts();
        createHud();
        fillViewByComponent(component);
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
        factory = new EditableElementsFactory();
        modifier = go.AddComponent<ViewModifier>();
        modifier.SetExecutor(executor);
        modifier.SetFactory(factory);
    }

    void attachScripts() {
        cameraController = go.AddComponent<CameraController>();
    }

    void createHud() {
        leftPanelHud = new LeftPanelHud(getChild("LeftPanel"), eventService, executor, save, goToLevelsView);
        rightPanelHud = new RightPanelHud(getChild("RightPanel"), eventService, onSelectedTypeChange);
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
}