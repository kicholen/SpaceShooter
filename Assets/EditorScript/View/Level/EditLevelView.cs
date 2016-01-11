public class EditLevelView : View, IView {

    ILevelService levelService;
    IViewService viewService;

    LevelModelComponent component;

    LevelModifierExecutor executor;
    LeftPanelHud leftPanelHud;

    public EditLevelView(ILevelService levelService, IViewService viewService) : base("EditorView/Level/EditLevelView") {
        this.levelService = levelService;
        this.viewService = viewService;
    }

    public override void Init() {
        base.Init();
        executor = new LevelModifierExecutor(component);
        leftPanelHud = new LeftPanelHud(getChild("LeftPanel"), executor, save, goToLevelsView);
    }

    public void SetData(LevelModelComponent component) {
        this.component = component;
    }

    void save() {
        levelService.UpdateLevel(component, goToLevelsView);
    }

    void goToLevelsView() {
        viewService.SetView(ViewTypes.EDITOR_LEVELS);
    }
}
