using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System;

public class PathView : View, IView {

    IPathService pathService;
    IViewService viewService;
    Transform content;

    public PathView(IPathService pathService, IViewService viewService) : base("EditorView/Path/PathView") {
        this.pathService = pathService;
        this.viewService = viewService;
    }

    public override void Init() {
        base.Init();
        content = getChild("Panel/Viewport/Content");
        pathService.LoadPathIds(onPathIdsLoaded);
        uiFactoryService.AddButton(getChild("NewButton").gameObject, () => pathService.CreateNewPath(onPathLoaded));
        uiFactoryService.AddButton(getChild("BackButton").gameObject, () => viewService.SetView(ViewTypes.EDITOR_LANDING));
    }

    void onPathIdsLoaded(List<string> pathIds) {
        foreach (string id in pathIds) {
            addButton(id);
        }
    }

    void addButton(string id) {
        GameObject go = uiFactoryService.CreatePrefab("EditorView/Button");
        uiFactoryService.AddText(go.transform, "Text", id);
        uiFactoryService.AddButton(go, onPathClicked);
        go.name = id;
        go.transform.SetParent(content.transform);
    }

    void onPathClicked() {
        string pathId = EventSystem.current.currentSelectedGameObject.name;
        pathService.LoadPathById(Convert.ToInt64(pathId), onPathLoaded);
    }

    void onPathLoaded(PathModelComponent component) {
        (viewService.SetView(ViewTypes.EDITOR_EDIT_PATH) as EditPathView).SetData(component);
    }
}