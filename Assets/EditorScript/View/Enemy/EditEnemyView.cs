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

    EnemyRightPanelHud rightPanelHud;

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

    public override void Destroy() {
        base.Destroy();
        rightPanelHud.Destroy();
    }

    void createHud() {
        new EnemyLeftPanelHud(getChild("LeftPanel/Viewport/Content"), eventService, executor, enemyEntity, component);
        rightPanelHud = new EnemyRightPanelHud(getChild("RightPanel/Viewport/Content"), eventService, enemyEntity, component);
    }

    void createEnemy() {
        enemyEntity = pool.CreateEntity()
            .AddPosition(new Vector2(0.0f, 2.0f));
        component.resource = component.resource == null ? ResourceWithColliders.Enemy : component.resource;
        enemyEntity.AddResource(component.resource);
    }

    void createDummy() {
        dummyEntity = pool.CreateEntity()
            .AddPosition(new Vector2(-2.0f, -2.0f))
            .AddPlayer("")
            .AddCollision(CollisionTypes.Player, 10)
            .AddResource(ResourceWithColliders.Player);
        Vector2 currentPosition = dummyEntity.position.pos;
        Vector2 offset = new Vector2(3.0f, 0.0f);
        dummyEntity.AddTween(true, new System.Collections.Generic.List<Tween>());
        dummyEntity.tween.AddTween(dummyEntity.position, EaseTypes.bounceOut, PositionAccessorType.XY, 2.0f)
                .From(currentPosition.x, currentPosition.y)
                .To(currentPosition.x + offset.x, currentPosition.y + offset.y)
                .PingPong();
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
        dummyEntity.IsDestroyEntity(true);
        viewService.SetView(ViewTypes.EDITOR_ENEMIES);
    }
}