using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System;

public class LevelsView : View, IView {
    IPathService pathService;
    ILevelService levelService;
    IViewService viewService;
    IEnemyService enemyService;

    Transform content;

    public LevelsView(ILevelService levelService, IViewService viewService, IPathService pathService, IEnemyService enemyService) : base("EditorView/Level/LevelsView") {
        this.levelService = levelService;
        this.viewService = viewService;
        this.pathService = pathService;
        this.enemyService = enemyService;
    }

    public override void Init() {
        base.Init();
        content = getChild("Panel/Viewport/Content");
        levelService.LoadLevelIds(onLevelIdsLoaded);
        uiFactoryService.AddButton(getChild("NewButton").gameObject, () => levelService.CreateNewLevel(onLevelLoaded));
        uiFactoryService.AddButton(getChild("BackButton").gameObject, () => viewService.SetView(ViewTypes.EDITOR_LANDING));
    }

    void onLevelIdsLoaded(Dictionary<long, string> levelIds) {
        foreach (KeyValuePair<long, string> pair in levelIds)
            addButton(pair);
    }

    void addButton(KeyValuePair<long, string> pair) {
        GameObject go = uiFactoryService.CreatePrefab("EditorView/Button");
        uiFactoryService.AddText(go.transform, "Text", pair.Value);
        uiFactoryService.AddButton(go, onLevelClicked);
        go.name = pair.Key.ToString();
        go.transform.SetParent(content.transform);
    }

    void onLevelClicked() {
        string levelId = EventSystem.current.currentSelectedGameObject.name;
        levelService.LoadLevelById(Convert.ToInt64(levelId), onLevelLoaded);
    }

    void onLevelLoaded(LevelModelComponent component) {
        pathService.LoadPaths(() => {
            enemyService.LoadEnemies(() => {
                (viewService.SetView(ViewTypes.EDITOR_EDIT_LEVEL) as EditLevelView).SetData(component);
            });
        });
    }
}