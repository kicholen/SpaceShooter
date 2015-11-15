using System.Collections.Generic;
using Entitas;

public class Services : IServices {
	Controller controller;
	Pool pool;
	ILoadService loadService;
	IGameService gameService;
	EventService eventService;
	List<IUpdateable> updateables = new List<IUpdateable>();

	public Controller Controller { get { return controller; } }
	public Pool Pool { get { return pool; } }
	public List<IUpdateable> Updateables { get { return updateables; } }
	public ILoadService LoadService { get { return loadService;	} }
	public EventService EventService { get { return eventService; } }
	public IGameService GameService { get { return gameService; } }

	public Services(Controller controller) {
		this.controller = controller;
		pool = Pools.pool;
		loadService = new LoadService();
		updateables.Add(loadService);
		gameService = new GameService(pool, Controller);
		eventService = new EventService();
	}

	public void TestInit() {
		gameService.Init();
	}

	public void Update () {
		for (int i = 0; i < updateables.Count; i++) {
			updateables[i].Update();
		}
	}
}
