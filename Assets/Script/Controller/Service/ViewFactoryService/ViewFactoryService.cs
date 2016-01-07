public class ViewFactoryService : IViewFactoryService {

	IServices services;

	public void Init(IServices services) {
		this.services = services;
	}

	public IView CreateView(ViewTypes type) {
		IView view = null;
		switch (type) {
			case ViewTypes.GAME:
				view = new GameView(services.Updateables, services.Pool, services.UIFactoryService, services.GameService, services.EventService);
			break;
			case ViewTypes.INIT:
				view = new InitView(services.Pool, services.UIFactoryService, services.EventService);
			break;
			case ViewTypes.LANDING:
				view = new LandingView(services.Pool, services.UIFactoryService, services.EventService, services.ViewService);
			break;
			case ViewTypes.LEVEL:
				view = new LevelView(services.Pool, services.UIFactoryService, services.EventService, services.LoadService, services.ViewService, services.GameService);
			break;
			case ViewTypes.LOAD:
				view = new LoadView(services.Pool, services.UIFactoryService, services.EventService);
			break;
			case ViewTypes.SHIP:
				view = new ShipView(services.Pool, services.UIFactoryService, services.EventService, services.ViewService);
			break;
		}

		return view;
	}
}