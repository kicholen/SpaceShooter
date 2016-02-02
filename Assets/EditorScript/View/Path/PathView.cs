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

    void onPathIdsLoaded(Dictionary<long, string> pathIds) {
        foreach (KeyValuePair<long, string> pair in pathIds)
            addButton(pair);
    }

    void addButton(KeyValuePair<long, string> pair) {
        GameObject go = uiFactoryService.CreatePrefab("EditorView/Button");
        uiFactoryService.AddText(go.transform, "Text", pair.Value);
        uiFactoryService.AddButton(go, onPathClicked);
        go.name = pair.Key.ToString();
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