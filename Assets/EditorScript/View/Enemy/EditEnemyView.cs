using System;
using Entitas;
using UnityEngine;

public class EditEnemyView : View, IView {
    IViewService viewService;
    IEnemyService enemyService;

    EnemyModelComponent component;
    Entity enemyEntity;
    Entity dummyEntity;
    EnemyModelCmpActionExecutor executor;

    public EditEnemyView(Pool pool, IViewService viewService, IEnemyService enemyService) : base("EditorView/Enemy/EditEnemyView") {
        this.pool = pool;
        this.viewService = viewService;
        this.enemyService = enemyService;
    }

    public override void Init() {
        base.Init();
        addSaveBackListeners();
    }

    public void SetData(EnemyModelComponent component) {
        this.component = component;
        executor = new EnemyModelCmpActionExecutor(component);
        createEnemy();
        createDummy();
        createHud();
    }

    void createHud() {
        new EnemyLeftPanelHud(getChild("LeftPanel/Viewport/Content"), eventService, executor, enemyEntity);
    }

    void createEnemy() {
        enemyEntity = pool.CreateEntity()
            .AddPosition(new Vector2())
            .AddResource(component.resource == null ? ResourceWithColliders.Enemy : component.resource);
    }

    void createDummy() {
        dummyEntity = pool.CreateEntity()
            .AddPosition(new Vector2(0.0f, 2.0f))
            .AddPlayer("")
            .AddCollision(CollisionTypes.Player, 10)
            //.AddHealth(shipModel.health)
            .AddResource(ResourceWithColliders.Player);
    }

    void addSaveBackListeners() {
        uiFactoryService.AddButton(getChild("SaveButton").gameObject, () => saveAndBack());
        uiFactoryService.AddButton(getChild("BackButton").gameObject, () => goToLandingView());
    }

    void saveAndBack() {
        saveEnemy(goToLandingView);
    }

    void saveEnemy(Action onSaveSuccess) {
        enemyService.UpdateEnemy(component, goToLandingView);
    }

    void goToLandingView() {
        enemyEntity.IsDestroyEntity(true);
        viewService.SetView(ViewTypes.EDITOR_ENEMIES);
    }
}