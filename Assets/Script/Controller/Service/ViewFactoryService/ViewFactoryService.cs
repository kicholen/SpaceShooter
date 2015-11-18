using Entitas;public class ViewFactoryService : IViewFactoryService {

	Controller controller;
	Pool pool;
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
			case ViewTypes.GAME:
				view = new GameView(controller.Services.Pool, uiFactoryService, controller.Services.GameService, eventService);
			break;
			case ViewTypes.INIT:
				view = new InitView(controller.Services.Pool, uiFactoryService, eventService);
			break;
			case ViewTypes.LANDING:
				view = new LandingView(controller.Services.Pool, uiFactoryService, eventService, controller.Services.ViewService);
			break;
			case ViewTypes.LEVEL:
				view = new LevelView(controller.Services.Pool, uiFactoryService, eventService, controller.Services.LoadService);
			break;
			case ViewTypes.LOAD:
				view = new LoadView(controller.Services.Pool, uiFactoryService, eventService);
			break;
			case ViewTypes.SHIP:
				view = new ShipView(controller.Services.Pool, uiFactoryService, eventService, controller.Services.ViewService);
			break;
		}

		return view;
	}
}