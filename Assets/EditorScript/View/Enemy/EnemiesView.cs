using System;
using System.Collections.Generic;
using Entitas;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;

public class EnemiesView : View, IView {
    IViewService viewService;
    IEnemyService enemyService;

    Transform content;

    public EnemiesView(Pool pool, IViewService viewService, IEnemyService enemyService) : base("EditorView/Enemy/EnemiesView") {
        this.pool = pool;
        this.viewService = viewService;
        this.enemyService = enemyService;
    }

    public override void Init() {
        base.Init();
        content = getChild("Panel/Viewport/Content");
        enemyService.LoadEnemyIds(onEnemiesLoaded);
        addBackAndNewListeners();
    }

    void onEnemiesLoaded(Dictionary<long, string> enemyIds) {
        List<KeyValuePair<long, string>> list = enemyIds.ToList();
        list.Sort((first, second) => first.Key.CompareTo(second.Key));
        foreach (KeyValuePair<long, string> pair in list)
            addButton(pair);
    }

    void addButton(KeyValuePair<long, string> pair) {
        GameObject go = uiFactoryService.CreatePrefab("EditorView/Button");
        uiFactoryService.AddText(go.transform, "Text", pair.Value);
        uiFactoryService.AddButton(go, onEnemyClicked);
        go.name = pair.Key.ToString();
        go.transform.SetParent(content.transform);
    }

    void onEnemyClicked() {
        string levelId = EventSystem.current.currentSelectedGameObject.name;
        enemyService.LoadEnemyById(Convert.ToInt64(levelId), onEnemyLoaded);
    }

    void addBackAndNewListeners() {
        uiFactoryService.AddButton(getChild("NewButton").gameObject, () => newAndEditEnemy());
        uiFactoryService.AddButton(getChild("BackButton").gameObject, () => goToLandingView());
    }

    void newAndEditEnemy() {
        enemyService.CreateNewEnemy(onEnemyLoaded);
    }

    void onEnemyLoaded(EnemyModelComponent component) {
        (viewService.SetView(ViewTypes.EDITOR_EDIT_ENEMY) as EditEnemyView).SetData(component);
    }

    void goToLandingView() {
        viewService.SetView(ViewTypes.EDITOR_LANDING);
    }
}