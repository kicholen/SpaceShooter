public class LoadService : ILoadService {

	IViewService viewService;
	EventService eventService;
	IGameService gameService;

	int phasesInProgress;
	public bool IsInProgress { get { return phasesInProgress > 0; } }

	public LoadService(EventService eventService) {
		this.eventService = eventService;
		addEventListeners();
	}

	public void PrepareAndExecute(IPhase phase) {
		phasesInProgress++;
		phase.SetEventService(eventService);
		phase.CreateActions();
		phase.Execute();
	}

	void addEventListeners() {
		eventService.AddListener<PhaseFinishedEvent>(onPhaseFinished);
	}

	void onPhaseFinished(PhaseFinishedEvent phaseEvent) {
		phasesInProgress--;
	}
}
