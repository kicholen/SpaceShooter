using System;
using Entitas;

public class EditBonusView : View, IView
{
    IViewService viewService;
    IBonusService bonusService;

    BonusModelComponent component;

    public EditBonusView(Pool pool, IViewService viewService, IBonusService bonusService) : base("EditorView/Bonus/BonusView") {
        this.pool = pool;
        this.viewService = viewService;
        this.bonusService = bonusService;
    }

    public override void Init() {
        base.Init();
        addSaveBackListeners();
    }

    public void SetData(BonusModelComponent component) {
        this.component = component;
        createHud();
    }

    void createHud() {
        new BonusPanelHud(getChild("Viewport/Content"), component);
    }

    void addSaveBackListeners() {
        uiFactoryService.AddButton(getChild("SaveButton").gameObject, saveAndBack);
        uiFactoryService.AddButton(getChild("BackButton").gameObject, goToLandingView);
    }

    void saveAndBack() {
        saveEnemy(goToLandingView);
    }

    void saveEnemy(Action onSaveSuccess) {
        bonusService.UpdateBonus(component, goToLandingView);
    }

    void goToLandingView() {
        viewService.SetView(ViewTypes.EDITOR_BONUSES);
    }
}