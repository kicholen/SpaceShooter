using System.Collections.Generic;
using System;

public class LoadService : ILoadService {

	IViewService viewService;
	EventService eventService;
	IGameService gameService;

	Queue<Action> phases;
	int totalPhases;
	int currentPhase;

	bool firstGame = true;

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

		totalPhases = phases.Count;
		nextPhase();
	}

	public void ExecutePlayGame(int level) {
		phases = new Queue<Action>();
		phases.Enqueue(() => addViewListener());
		phases.Enqueue(() => viewService.SetView(ViewTypes.LOAD));
		phases.Enqueue(() => { gameService.InitGame(level); nextPhase(); });
		if (firstGame) {
			phases.Enqueue(() => { gameService.InitPool(Resource.Explosion, 4); nextPhase(); });
			phases.Enqueue(() => { gameService.InitPool(Resource.MissileEnemy, 20); nextPhase(); });
			phases.Enqueue(() => { gameService.InitPool(Resource.Missile, 20); nextPhase(); });
			phases.Enqueue(() => { gameService.InitPool(Resource.Enemy, 10); nextPhase(); });
			phases.Enqueue(() => { gameService.InitPool(Resource.Star, 10); nextPhase(); });
			phases.Enqueue(() => { gameService.InitPool(Resource.Bonus, 3); nextPhase(); });
			firstGame = false;
		}
		phases.Enqueue(() => addViewListener());
		phases.Enqueue(() => viewService.SetView(ViewTypes.GAME));
		phases.Enqueue(() => { gameService.PlayGame(); nextPhase(); });

		totalPhases = phases.Count;
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
		currentPhase++;
		if (phases.Count > 0) {
			eventService.Dispatch<LoadProgressEvent>(new LoadProgressEvent((float)currentPhase/(float)totalPhases));
			phases.Dequeue().Invoke();
		}
		else {
			totalPhases = 0;
			currentPhase = 0;
		}
	}
}
