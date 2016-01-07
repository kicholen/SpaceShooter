using System.Collections.Generic;
using Entitas;

public class EditorServices : IServices {
	EditorController controller;
	Pool pool;
	List<Updateable> updateables = new List<Updateable>();
	ILoadService loadService;
	EventService eventService;
	IGameService gameService;
	IViewService viewService;
	IViewFactoryService viewFactoryService;
	IUIFactoryService uiFactoryService;
	
	public IController Controller { get { return controller; } }
	public Pool Pool { get { return pool; } }
	public List<Updateable> Updateables { get { return updateables; } }
	public ILoadService LoadService { get { return loadService;	} }
	public EventService EventService { get { return eventService; } }
	public IGameService GameService { get { return gameService; } }
	public IViewService ViewService { get { return viewService; } }
	public IViewFactoryService ViewFactoryService { get { return viewFactoryService; } }
	public IUIFactoryService UIFactoryService { get { return uiFactoryService; } }
	
	public EditorServices(EditorController controller) {
		this.controller = controller;
		pool = Pools.pool;
		eventService = new EventService();
		
		uiFactoryService = new UIFactoryService();
		viewFactoryService = new ViewFactoryService();
		
		viewService = new ViewService(eventService, uiFactoryService, viewFactoryService);
		
		loadService = new LoadService(eventService);
		gameService = new GameService(pool, viewService);

		pool.CreateEntity()
			.AddMaterialReference(controller.GetComponent<MaterialStorage>());
	}

	public void Init() {
		viewFactoryService.Init(this);
	}
	
	public void Update () {
		for (int i = 0; i < updateables.Count; i++) {
			updateables[i].Update();
		}
	}
}
