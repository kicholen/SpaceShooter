using Entitas;

public class LandingView : View, IView {
	
	IViewService viewService;
	
	public LandingView(Pool pool, IUIFactoryService uiFactoryService, EventService eventService, IViewService viewService)
	: base(pool, uiFactoryService, eventService, "View/LandingView") {
		this.viewService = viewService;
		uiFactoryService.AddButton(go.transform, "PlayButton", onPlayClicked);
	}

	void onPlayClicked() {
		viewService.SetView(ViewTypes.LEVEL);
	}
}