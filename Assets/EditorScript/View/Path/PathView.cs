using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

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
        pathService.LoadPathById(pathId, onPathLoaded);
        //EditorController.instance.component = Utils.Deserialize<PathModelComponent>(sufix);
        //EditorController.instance.SetPathCreator();
        //EditorController.instance.menuController = this; changeView
        //listGO.SetActive(false);
    }

    void onPathLoaded(PathModelComponent component) {// todo
        component = Utils.Deserialize<PathModelComponent>("10");
        component.id = "42";
        pathService.UpdatePath(component, () => { });
        //viewService.SetView(ViewTypes.EDITOR_EDIT_PATH);
    }
}