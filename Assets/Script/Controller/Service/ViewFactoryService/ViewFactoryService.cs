public class ViewFactoryService : IViewFactoryService {

	Controller controller;
	EventService eventService;
	IUIFactoryService uiFactoryService;

	public ViewFactoryService(Controller controller, EventService eventService, IUIFactoryService uiFactoryService) {
		this.controller = controller;
		this.eventService = eventService;
		this.uiFactoryService = uiFactoryService;
	}

	public IView CreateView(ViewTypes type) {
		IView view = null;
		switch (type) {
			case ViewTypes.INIT:
				view = new InitView(uiFactoryService, eventService);
			break;
			case ViewTypes.GAME:
				view = new GameView(uiFactoryService, controller.Services.GameService, eventService);
			break;
			case ViewTypes.LEVEL:
				view = new LevelView(controller.Services.Pool, uiFactoryService, eventService, controller.Services.LoadService);
			break;
			case ViewTypes.LOAD:
				view = new LoadView(uiFactoryService, eventService);
			break;
			case ViewTypes.LANDING:
				view = new LandingView(uiFactoryService, eventService, controller.Services.ViewService);
			break;
		}

		return view;
	}
}