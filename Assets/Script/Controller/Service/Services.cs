using System.Collections.Generic;

public class Services : IServices {

	ILoadService loadService;
	IGameService gameService;
	EventService eventService;

	List<IUpdateable> updateables = new List<IUpdateable>();

	public ILoadService LoadService { get { return loadService;	} }
	public EventService EventService { get { return eventService; } }
	public IGameService GameService { get { return gameService; } }

	public Services() {
		loadService = new LoadService();
		updateables.Add(loadService);
		gameService = new GameService();
		updateables.Add(gameService);
		eventService = new EventService();
	}
	
	public void Update () {
		for (int i = 0; i < updateables.Count; i++) {
			updateables[i].Update();
		}
	}
}
