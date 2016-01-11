using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System;

public class LevelsView : View, IView {

    ILevelService levelService;
    IViewService viewService;
    Transform content;

    public LevelsView(ILevelService levelService, IViewService viewService) : base("EditorView/Level/LevelsView") {
        this.levelService = levelService;
        this.viewService = viewService;
    }

    public override void Init() {
        base.Init();
        content = getChild("Panel/Viewport/Content");
        levelService.LoadLevelIds(onLevelIdsLoaded);
        uiFactoryService.AddButton(getChild("NewButton").gameObject, () => levelService.CreateNewLevel(onLevelLoaded));
        uiFactoryService.AddButton(getChild("BackButton").gameObject, () => viewService.SetView(ViewTypes.EDITOR_LANDING));
    }

    void onLevelIdsLoaded(List<string> levelIds) {
        foreach (string id in levelIds) {
            addButton(id);
        }
    }

    void addButton(string id) {
        GameObject go = uiFactoryService.CreatePrefab("EditorView/Button");
        uiFactoryService.AddText(go.transform, "Text", id);
        uiFactoryService.AddButton(go, onLevelClicked);
        go.name = id;
        go.transform.SetParent(content.transform);
    }

    void onLevelClicked() {
        string levelId = EventSystem.current.currentSelectedGameObject.name;
        levelService.LoadLevelById(Convert.ToInt64(levelId), onLevelLoaded);
    }

    void onLevelLoaded(LevelModelComponent component) {
        //(viewService.SetView(ViewTypes.EDITOR_EDIT_PATH) as EditPathView).SetData(component);
    }
}