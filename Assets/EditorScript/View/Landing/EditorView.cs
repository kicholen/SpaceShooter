using UnityEngine;
using UnityEngine.Events;

public class EditorView : View, IView {

    IViewService viewService;
    Transform content;

    public EditorView(IViewService viewService) : base("EditorView/Landing/LandingView") {
        this.viewService = viewService;
    }

    public override void Init() {
        base.Init();
        content = getChild("Panel/Viewport/Content");
        addButton("Edit Paths", () => viewService.SetView(ViewTypes.EDITOR_PATH));
        addButton("Edit Levels", () => viewService.SetView(ViewTypes.EDITOR_LEVELS));
        addButton("Edit Enemies", () => viewService.SetView(ViewTypes.EDITOR_ENEMIES));
        addButton("Edit Bonuses", () => viewService.SetView(ViewTypes.EDITOR_BONUSES));
        addButton("Edit Difficulties", () => viewService.SetView(ViewTypes.EDITOR_DIFFICULTIES));
        addButton("Edit Languages", () => viewService.SetView(ViewTypes.EDITOR_LANUGAGES));
    }

    void addButton(string text, UnityAction onClicked) {
        GameObject go = uiFactoryService.CreatePrefab("EditorView/Button");
        uiFactoryService.AddText(go.transform, "Text", text);
        uiFactoryService.AddButton(go, onClicked);
        go.name = text;
        go.transform.SetParent(content.transform);
    }
}