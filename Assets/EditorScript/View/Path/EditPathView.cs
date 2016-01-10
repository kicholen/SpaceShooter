using Entitas;

public class EditPathView : View, IView {

    IPathService pathService;
    IViewService viewService;

    PathModelComponent component;

    EditorGrid gridScript;
    CameraController cameraScript;
    PathCreator pathCreatorScript;

    public EditPathView(IPathService pathService, IViewService viewService) : base("EditorView/Path/EditPathView") {
        this.pathService = pathService;
        this.viewService = viewService;
    }

    public override void Init() {
        base.Init();
        addListeners();
        attachScripts();
    }

    public void SetData(PathModelComponent component) {
        this.component = component;
        pathCreatorScript.Init(component, pool.GetGroup(Matcher.MaterialReference).GetSingleEntity().materialReference.storage.Default);
    }

    void addListeners() {
        uiFactoryService.AddButton(getChild("SaveButton").gameObject, save);
        uiFactoryService.AddButton(getChild("BackButton").gameObject, goToPathView);
        uiFactoryService.AddButton(getChild("InfoButton").gameObject, showInfoBox);
    }

    void save() {
        pathCreatorScript.Save();
        pathService.UpdatePath(component, goToPathView);
    }

    void goToPathView() {
        viewService.SetView(ViewTypes.EDITOR_PATH);
    }

    void showInfoBox() {
        eventService.Dispatch<InfoBoxShowEvent>(new InfoBoxShowEvent("'z' - undo\n 'y' - redo\n 'w''s''a''d' & '+'/'-' \n select node - 'delete'"));
    }

    void attachScripts() {
        gridScript = go.AddComponent<EditorGrid>();
        cameraScript = go.AddComponent<CameraController>();
        pathCreatorScript = go.AddComponent<PathCreator>();
    }

}