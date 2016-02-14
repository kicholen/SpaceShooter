using Entitas;

public class LandingView : View, IView {
	
	IViewService viewService;
	
	public LandingView(IViewService viewService) : base("View/LandingView") {
        this.viewService = viewService;
    }

    public override void Init() {
        base.Init();
        uiFactoryService.AddButton(go.transform, "PlayButton", onPlayClicked);
        uiFactoryService.AddButton(go.transform, "ShipButton", onShipClicked);
        uiFactoryService.AddButton(go.transform, "SettingsButton", onSettingsClicked);
    }

    void onPlayClicked() {
		viewService.SetView(ViewTypes.LEVEL);
    }

    void onShipClicked() {
        viewService.SetView(ViewTypes.SHIP);
    }

    void onSettingsClicked() {
        viewService.SetView(ViewTypes.SETTINGS);
    }
}