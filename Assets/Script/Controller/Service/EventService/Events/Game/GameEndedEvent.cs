public class GameEndedEvent : GameEvent {
	
	public bool lost;
	
	public GameEndedEvent(bool lost) {
		this.lost = lost;
	}
	
}