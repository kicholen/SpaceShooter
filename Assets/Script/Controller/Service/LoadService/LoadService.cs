using System.Collections.Generic;
using System;

public class LoadService : ILoadService {

	IViewService viewService;
	EventService eventService;
	IGameService gameService;

	Queue<Action> phases;

	public LoadService(IViewService viewService, EventService eventService, IGameService gameService) {
		this.viewService = viewService;
		this.eventService = eventService;
		this.gameService = gameService;
	}

	public void ExecuteInit() {
		phases = new Queue<Action>();
		phases.Enqueue(() => addViewListener());
		phases.Enqueue(() => viewService.SetView(ViewTypes.INIT));
		phases.Enqueue(() => { gameService.Init(); nextPhase(); });
		phases.Enqueue(() => addViewListener());
		phases.Enqueue(() => viewService.SetView(ViewTypes.LANDING));
		nextPhase();
	}

	public void ExecutePlayGame(int level = 1) {
		phases = new Queue<Action>();
		phases.Enqueue(() => addViewListener());
		phases.Enqueue(() => viewService.SetView(ViewTypes.LOAD));
		phases.Enqueue(() => { gameService.InitGame(level); nextPhase(); });
		phases.Enqueue(() => addViewListener());
		phases.Enqueue(() => viewService.SetView(ViewTypes.GAME));
		phases.Enqueue(() => { gameService.PlayGame(); nextPhase(); });
		nextPhase();
	}

	void addViewListener() {
		eventService.AddListener<ViewShownEvent>(onViewShown);
		nextPhase();
	}

	void onViewShown(ViewShownEvent e) {
		eventService.RemoveListener<ViewShownEvent>(onViewShown);
		nextPhase();
	}

	void nextPhase() {
		if (phases.Count > 0) {
			phases.Dequeue().Invoke();
		}
	}
}
