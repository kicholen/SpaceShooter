using System.Collections.Generic;
using Entitas;

public class Services : IServices {
	Controller controller;
	Pool pool;
	List<IUpdateable> updateables = new List<IUpdateable>();
	ILoadService loadService;
	EventService eventService;
	IGameService gameService;
	IViewService viewService;
	IViewFactoryService viewFactoryService;
	IUIFactoryService uiFactoryService;

	public Controller Controller { get { return controller; } }
	public Pool Pool { get { return pool; } }
	public List<IUpdateable> Updateables { get { return updateables; } }
	public ILoadService LoadService { get { return loadService;	} }
	public EventService EventService { get { return eventService; } }
	public IGameService GameService { get { return gameService; } }
	public IViewService ViewService { get { return viewService; } }
	public IViewFactoryService ViewFactoryService { get { return viewFactoryService; } }
	public IUIFactoryService UIFactoryService { get { return uiFactoryService; } }

	public Services(Controller controller) {
		this.controller = controller;
		pool = Pools.pool;
		gameService = new GameService(pool, Controller);
		eventService = new EventService();

		uiFactoryService = new UIFactoryService();
		viewFactoryService = new ViewFactoryService(controller, eventService, uiFactoryService);
		
		viewService = new ViewService(eventService, uiFactoryService, viewFactoryService);

		loadService = new LoadService(viewService, eventService, gameService);

		pool.CreateEntity()
			.AddMaterialReference(controller.GetComponent<MaterialStorage>());
	}

	public void Update () {
		for (int i = 0; i < updateables.Count; i++) {
			updateables[i].Update();
		}
	}
}
