using System.Collections.Generic;
using System;

public abstract class Phase
{
	protected EventService eventService;

	protected Queue<Action> actions;
	int currentAction;

	public int Count { get { return actions.Count; } }

	public abstract void CreateActions();

	public void SetEventService(EventService eventService) {
		this.eventService = eventService;
	}

	public void Execute() {
		nextAction();
	}

	protected void addViewListener() {
		eventService.AddListener<ViewShownEvent>(onViewShown);
		nextAction();
	}
	
	protected void onViewShown(ViewShownEvent e) {
		eventService.RemoveListener<ViewShownEvent>(onViewShown);
		nextAction();
	}
	
	protected void nextAction() {
		currentAction++;
		if (actions.Count > 0) {
			eventService.Dispatch<PhaseProgressEvent>(new PhaseProgressEvent((float)currentAction/(float)Count));
			actions.Dequeue().Invoke();
		}
		else {
			eventService.Dispatch<PhaseFinishedEvent>(new PhaseFinishedEvent(this));
		}
	}
}

