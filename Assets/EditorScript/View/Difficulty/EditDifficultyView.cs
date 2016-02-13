using System;
using Entitas;

public class EditDifficultyView : View, IView
{
    IViewService viewService;
    IDifficultyService difficultyService;

    DifficultyModelComponent component;

    public EditDifficultyView(Pool pool, IViewService viewService, IDifficultyService difficultyService) : base("EditorView/Difficulty/DifficultyView") {
        this.pool = pool;
        this.viewService = viewService;
        this.difficultyService = difficultyService;
    }

    public override void Init() {
        base.Init();
        addSaveBackListeners();
    }

    public void SetData(DifficultyModelComponent component) {
        this.component = component;
        createHud();
    }

    void createHud() {
        new DifficultyPanelHud(getChild("Viewport/Content"), component);
    }

    void addSaveBackListeners() {
        uiFactoryService.AddButton(getChild("SaveButton").gameObject, saveAndBack);
        uiFactoryService.AddButton(getChild("BackButton").gameObject, goToLandingView);
    }

    void saveAndBack() {
        saveEnemy(goToLandingView);
    }

    void saveEnemy(Action onSaveSuccess) {
        difficultyService.UpdateDifficulty(component, goToLandingView);
    }

    void goToLandingView() {
        viewService.SetView(ViewTypes.EDITOR_DIFFICULTIES);
    }
}