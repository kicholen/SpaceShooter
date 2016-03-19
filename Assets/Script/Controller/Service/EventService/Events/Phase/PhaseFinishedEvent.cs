public class PhaseFinishedEvent : GameEvent {
	
	public Phase phase;
	
	public PhaseFinishedEvent(Phase phase) {
		this.phase = phase;
	}
}