public class EditLevelView : View, IView {

    ILevelService levelService;
    IViewService viewService;

    LevelModelComponent component;

    LevelActionExecutor executor;
    ViewModifier modifier;
    LeftPanelHud leftPanelHud;
    RightPanelHud rightPanelHud;
    CameraController cameraController;

    public EditLevelView(ILevelService levelService, IViewService viewService) : base("EditorView/Level/EditLevelView") {
        this.levelService = levelService;
        this.viewService = viewService;
    }

    public void SetData(LevelModelComponent component) {
        this.component = component;
        executor = new LevelActionExecutor(component);
        modifier = go.AddComponent<ViewModifier>();
        modifier.SetExecutor(executor);
        cameraController = go.AddComponent<CameraController>();
        leftPanelHud = new LeftPanelHud(getChild("LeftPanel"), eventService, executor, save, goToLevelsView);
        rightPanelHud = new RightPanelHud(getChild("RightPanel"), eventService, onSelectedTypeChange);
    }

    void onSelectedTypeChange(SelectedType type) {
        modifier.SetSelectedType(type);
    }

    void save() {
        levelService.UpdateLevel(component, goToLevelsView);
    }

    void goToLevelsView() {
        viewService.SetView(ViewTypes.EDITOR_LEVELS);
    }
}
